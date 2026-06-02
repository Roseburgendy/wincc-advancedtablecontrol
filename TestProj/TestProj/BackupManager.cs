using CCHMIRUNTIME;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestProj
{
    /// <summary>
    /// 管理 WinCC 备份归档的连接与断开
    /// </summary>
    public static class BackupManager
    {
        // 归档类型常量：1=快速归档，2=慢速归档，3=快速+慢速
        private const HMILoggingType ArchiveTypeBoth = (HMILoggingType)1;

        // 已连接归档列表（含时间范围，断开时作为 Remove 的参数）
        private static readonly List<ConnectedArchiveEntry> _connectedArchives =
            new List<ConnectedArchiveEntry>();

        /// <summary>当前已连接的备份归档列表（只读）。</summary>
        public static IReadOnlyList<ConnectedArchiveEntry> ConnectedArchives =>
            _connectedArchives.AsReadOnly();

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
                // connect the backup 
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
            string sourcePath, 
            DateTime? timeFrom, 
            DateTime? timeTo)
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

        /// <summary>
        /// 断开已连接的备份归档。
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

        #region  自动链接备份

        /// <summary>
        /// 判断 [queryStart, queryEnd] 是否已被已连接归档完全覆盖。
        /// 采用区间合并算法：把与查询段有交叉的条目按起始时间排序后逐段合并，
        /// 检查合并结果能否无缺口地覆盖整个查询范围。
        /// </summary>
        public static bool NeedsConnection(string backupFolder, DateTime queryStart, DateTime queryEnd)
        {
            if (string.IsNullOrWhiteSpace(backupFolder) || !Directory.Exists(backupFolder))
                return false;

            // 取出所有与查询时段有交叉的已连接条目
            var relevant = _connectedArchives
                .Where(e =>
                    (!e.TimeFrom.HasValue || e.TimeFrom.Value < queryEnd) &&
                    (!e.TimeTo.HasValue   || e.TimeTo.Value   > queryStart))
                .ToList();

            if (relevant.Count == 0) return true;

            // 无时间范围的条目 = 当时以空时间段连接了整个文件夹，视为完全覆盖
            if (relevant.Any(e => !e.TimeFrom.HasValue || !e.TimeTo.HasValue))
                return false;

            // 区间合并：按 TimeFrom 升序排列，依次向右扩展已覆盖边界
            var sorted  = relevant.OrderBy(e => e.TimeFrom.Value).ToList();
            DateTime covered = queryStart;

            foreach (var e in sorted)
            {
                if (e.TimeFrom.Value > covered)
                    return true;            // 存在缺口 → 未完全覆盖

                if (e.TimeTo.Value > covered)
                    covered = e.TimeTo.Value;
            }

            return covered < queryEnd;      // true = 右侧还有未覆盖部分
        }

        #endregion

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
                string name  = Path.GetFileNameWithoutExtension(f); // get file name
                var    range = ParseBackupTimeRange(name); // get time range
                result.Add(new BackupFileInfo { Name = name, TimeRange = range }); // add to file log
            }
            return result;
        }

        #region utility
        /// <summary>
        /// 返回文件夹内与时间段重叠的备份文件；timeFrom/timeTo 均为 null 时返回全部。
        /// </summary>
        public static List<BackupFileInfo> GetMatchingFiles(
            string folder, DateTime? timeFrom, DateTime? timeTo)
        {
            var all = GetBackupFilesInFolder(folder);
            // no time range specified → return all
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

        #endregion
    }

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
