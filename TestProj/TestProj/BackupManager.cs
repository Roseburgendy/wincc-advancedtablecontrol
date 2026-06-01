using CCHMIRUNTIME;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestProj
{
    /// <summary>
    /// 管理 WinCC 备份归档的连接与断开。
    /// 连接：HMIRuntime.Logging.DataLogs.Restore SourcePath, TimeFrom, TimeTo, TimeOut, Type
    /// 断开：HMIRuntime.Logging.DataLogs.Remove              TimeFrom, TimeTo, TimeOut, Type
    ///        （注意：Remove 无 SourcePath，以时间范围定位归档）
    /// </summary>
    public static class BackupManager
    {
        // 归档类型常量：1=快速归档，2=慢速归档，3=快速+慢速
        private const HMILoggingType ArchiveTypeBoth = (HMILoggingType)3;

        // 已连接归档列表（含时间范围，断开时作为 Remove 的参数）
        private static readonly List<ConnectedArchiveEntry> _connectedArchives =
            new List<ConnectedArchiveEntry>();

        /// <summary>当前已连接的备份归档列表（只读）。</summary>
        public static IReadOnlyList<ConnectedArchiveEntry> ConnectedArchives =>
            _connectedArchives.AsReadOnly();

        // ── 连接 ────────────────────────────────────────────────────────────

        /// <summary>
        /// 连接备份归档。
        /// </summary>
        /// <param name="sourcePath">备份文件夹路径，例如 <c>E:\db1\</c>。</param>
        /// <param name="timeFrom">查询开始时间（UTC）；null 表示连接所有归档。</param>
        /// <param name="timeTo">查询结束时间（UTC）；null 表示连接所有归档。</param>
        /// <param name="archiveType">归档类型，默认快速+慢速。</param>
        /// <returns>0 = 成功；1 = 已连接或无备份（视为正常）；其余 = 错误。</returns>
        public static int ConnectBackup(
            string         sourcePath,
            DateTime?      timeFrom    = null,
            DateTime?      timeTo      = null,
            HMILoggingType archiveType = ArchiveTypeBoth)
        {
            var hmi = new HMIRuntime();
            int result;

            if (timeFrom.HasValue && timeTo.HasValue)
            {
                string fromStr = timeFrom.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string toStr   = timeTo.Value.ToString("yyyy-MM-dd HH:mm:ss");
                result = (int)hmi.Logging.DataLogs.Restore(
                    sourcePath, fromStr, toStr, -1, archiveType);
            }
            else
            {
                // 空字符串 = 时间格式无效 → WinCC 连接文件夹下所有归档
                result = (int)hmi.Logging.DataLogs.Restore(
                    sourcePath, "", "", -1, archiveType);
            }

            if (result == 0 || result == 1)
                RecordConnected(sourcePath, timeFrom, timeTo);

            return result;
        }

        /// <summary>
        /// 连接成功后，扫描文件夹记录每个被连接的归档条目，供断开时使用。
        /// </summary>
        private static void RecordConnected(
            string sourcePath, DateTime? timeFrom, DateTime? timeTo)
        {
            var files = GetMatchingFiles(sourcePath, timeFrom, timeTo);

            if (files.Count > 0)
            {
                foreach (var f in files)
                {
                    // 跳过已记录的重复项
                    if (_connectedArchives.Any(e => e.DisplayName == f.ToString()))
                        continue;

                    _connectedArchives.Add(new ConnectedArchiveEntry
                    {
                        DisplayName = f.ToString(),
                        TimeFrom    = f.TimeRange?.Start,   // UTC
                        TimeTo      = f.TimeRange?.End      // UTC
                    });
                }
            }
            else
            {
                // 文件夹内无可识别文件名 → 以文件夹路径 + 时间范围记录一条通用条目
                string display = timeFrom.HasValue
                    ? $"{sourcePath}  [{timeFrom.Value.ToLocalTime():yyyy-MM-dd HH:mm} ~ {timeTo.Value.ToLocalTime():yyyy-MM-dd HH:mm}]"
                    : sourcePath;

                if (!_connectedArchives.Any(e => e.DisplayName == display))
                {
                    _connectedArchives.Add(new ConnectedArchiveEntry
                    {
                        DisplayName = display,
                        TimeFrom    = timeFrom,
                        TimeTo      = timeTo
                    });
                }
            }
        }

        // ── 断开 ────────────────────────────────────────────────────────────

        /// <summary>
        /// 断开已连接的备份归档。
        /// WinCC Remove 以时间范围定位归档，无 SourcePath 参数。
        /// </summary>
        /// <param name="entry">要断开的归档条目（来自 <see cref="ConnectedArchives"/>）。</param>
        /// <param name="archiveType">归档类型，默认快速+慢速。</param>
        /// <returns>0 = 成功；其余 = 错误。</returns>
        public static int DisconnectBackup(
            ConnectedArchiveEntry entry,
            HMILoggingType        archiveType = ArchiveTypeBoth)
        {
            var hmi = new HMIRuntime();
            int result;

            if (entry.TimeFrom.HasValue && entry.TimeTo.HasValue)
            {
                string fromStr = entry.TimeFrom.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string toStr   = entry.TimeTo.Value.ToString("yyyy-MM-dd HH:mm:ss");
                result = (int)hmi.Logging.DataLogs.Remove(fromStr, toStr, -1, archiveType);
            }
            else
            {
                // 无时间范围（连接所有时）→ 空字符串让 WinCC 匹配所有已连接归档
                result = (int)hmi.Logging.DataLogs.Remove("", "", -1, archiveType);
            }

            if (result == 0)
                _connectedArchives.Remove(entry);

            return result;
        }

        // ── 自动连接 ────────────────────────────────────────────────────────

        /// <summary>
        /// 根据查询时间范围自动连接备份归档。
        /// 若已有时间范围覆盖查询区间的归档，跳过不重复连接。
        /// </summary>
        public static int AutoConnectForTimeRange(
            string   backupFolder,
            DateTime queryStart,
            DateTime queryEnd)
        {
            if (string.IsNullOrWhiteSpace(backupFolder) || !Directory.Exists(backupFolder))
                return -1;

            // 检查是否已有覆盖该查询范围的已连接归档
            bool alreadyCovered = _connectedArchives.Any(e =>
                (!e.TimeFrom.HasValue || e.TimeFrom.Value < queryEnd) &&
                (!e.TimeTo.HasValue   || e.TimeTo.Value   > queryStart));

            if (alreadyCovered) return -999;

            return ConnectBackup(backupFolder, queryStart, queryEnd);
        }

        // ── 文件扫描工具 ─────────────────────────────────────────────────────

        /// <summary>
        /// 扫描指定文件夹，返回识别到的 WinCC 备份归档文件列表。
        /// </summary>
        public static List<BackupFileInfo> GetBackupFilesInFolder(string folderPath)
        {
            var result = new List<BackupFileInfo>();
            if (!Directory.Exists(folderPath)) return result;

            var files = Directory.GetFiles(folderPath, "*.ldf", SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
                files = Directory.GetFiles(folderPath, "*.mdf", SearchOption.TopDirectoryOnly);

            foreach (var f in files.OrderBy(x => x))
            {
                string name  = Path.GetFileNameWithoutExtension(f);
                var    range = ParseBackupTimeRange(name);
                result.Add(new BackupFileInfo { Name = name, TimeRange = range });
            }
            return result;
        }

        /// <summary>
        /// 返回文件夹内与时间段重叠的备份文件；timeFrom/timeTo 均为 null 时返回全部。
        /// </summary>
        public static List<BackupFileInfo> GetMatchingFiles(
            string folder, DateTime? timeFrom, DateTime? timeTo)
        {
            var all = GetBackupFilesInFolder(folder);
            if (!timeFrom.HasValue || !timeTo.HasValue) return all;

            return all.Where(f =>
                f.TimeRange.HasValue &&
                f.TimeRange.Value.Start < timeTo.Value &&
                f.TimeRange.Value.End   > timeFrom.Value
            ).ToList();
        }

        /// <summary>从 WinCC 备份归档文件名中解析时间范围（UTC）。</summary>
        public static (DateTime Start, DateTime End)? ParseBackupTimeRange(string archiveName)
        {
            var match = Regex.Match(archiveName, @"_TLG_F_(\d{12})_(\d{12})$");
            if (!match.Success) return null;

            if (TryParseBackupDateTime(match.Groups[1].Value, out DateTime start) &&
                TryParseBackupDateTime(match.Groups[2].Value, out DateTime end))
                return (start, end);

            return null;
        }

        private static bool TryParseBackupDateTime(string s, out DateTime dt)
        {
            dt = default;
            if (s.Length != 12) return false;
            if (!DateTime.TryParseExact(s, "yyyyMMddHHmm",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out dt))
                return false;
            dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            return true;
        }
    }

    // ── 数据类 ───────────────────────────────────────────────────────────────

    /// <summary>已连接的备份归档条目，保存断开时所需的时间范围。</summary>
    public class ConnectedArchiveEntry
    {
        /// <summary>显示名称（文件名 + 本地时间范围，或文件夹路径）。</summary>
        public string    DisplayName { get; set; }

        /// <summary>连接时的开始时间（UTC）；null 表示连接了整个文件夹。</summary>
        public DateTime? TimeFrom    { get; set; }

        /// <summary>连接时的结束时间（UTC）；null 表示连接了整个文件夹。</summary>
        public DateTime? TimeTo      { get; set; }
    }

    /// <summary>备份文件信息（用于对话框列表展示）。</summary>
    public class BackupFileInfo
    {
        public string Name      { get; set; }
        public (DateTime Start, DateTime End)? TimeRange { get; set; }

        public override string ToString()
        {
            if (TimeRange.HasValue)
            {
                DateTime localStart = TimeRange.Value.Start.ToLocalTime();
                DateTime localEnd   = TimeRange.Value.End.ToLocalTime();
                return $"{Name}  [{localStart:yyyy-MM-dd HH:mm} ~ {localEnd:yyyy-MM-dd HH:mm}]";
            }
            return Name;
        }
    }
}
