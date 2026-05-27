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
    /// 使用 HMIRuntime.Logging.DataLogs.Restore / Remove COM API。
    /// </summary>
    public static class BackupManager
    {
        // 本地记录已连接的备份文件夹路径（用于"断开备份"列表展示）
        private static readonly HashSet<string> _connectedFolders =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>当前已连接的备份文件夹路径集合（只读）。</summary>
        public static IReadOnlyCollection<string> ConnectedFolders => _connectedFolders;

        /// <summary>
        /// 连接备份归档。
        /// </summary>
        /// <param name="sourcePath">
        ///   备份文件夹路径，例如 <c>E:\db1\</c>。
        ///   WinCC 会将该文件夹下与时间段匹配的归档文件全部连接。
        ///   若 <paramref name="timeFrom"/> 和 <paramref name="timeTo"/> 均为 null，
        ///   则连接文件夹下所有归档文件。
        /// </param>
        /// <param name="timeFrom">查询开始时间（UTC）；null 表示不限。</param>
        /// <param name="timeTo">查询结束时间（UTC）；null 表示不限。</param>
        /// <param name="archiveType">
        ///   归档类型：1=快速归档，2=慢速归档，3=快速+慢速（默认）。
        /// </param>
        /// <returns>
        ///   0 = 成功；1 = 归档已连接或路径下无备份（视为正常）；其余值 = 错误。
        /// </returns>
        // 归档类型常量（与 HMILoggingType 枚举值对应）
        // 1=快速归档，2=慢速归档，3=快速+慢速
        private const HMILoggingType ArchiveTypeBoth = (HMILoggingType)3;

        public static int ConnectBackup(
            string    sourcePath,
            DateTime? timeFrom   = null,
            DateTime? timeTo     = null,
            HMILoggingType archiveType = ArchiveTypeBoth)
        {
            var hmi = new HMIRuntime();
            int result;

            if (timeFrom.HasValue && timeTo.HasValue)
            {
                // Restore 时间参数为字符串（BSTR），格式 yyyy-MM-dd HH:mm:ss，UTC 时间
                string fromStr = timeFrom.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string toStr   = timeTo.Value.ToString("yyyy-MM-dd HH:mm:ss");
                result = (int)hmi.Logging.DataLogs.Restore(
                    sourcePath,
                    fromStr,
                    toStr,
                    -1,           // TimeOut = -1：等待连接完成
                    1);
            }
            else
            {
                // 时间参数传空字符串 → 格式无效 → WinCC 连接路径下所有归档文件
                // （文档说明：TimeFrom/TimeTo 格式错误时连接路径下全部归档）
                result = (int)hmi.Logging.DataLogs.Restore(
                    sourcePath,
                    "",
                    "",
                    -1,
                    1);
            }

            // 0 = 正常，1 = 已连接或无备份，均视为可以记录
            if (result == 0 || result == 1)
                _connectedFolders.Add(sourcePath);

            return result;
        }

        /// <summary>
        /// 断开已连接的备份归档。
        /// </summary>
        /// <param name="sourcePath">备份文件夹路径（与连接时相同）。</param>
        /// <param name="archiveType">归档类型，默认快速+慢速。</param>
        /// <returns>0 = 成功；其余 = 错误。</returns>
        public static int DisconnectBackup(
            string sourcePath,
            HMILoggingType archiveType = ArchiveTypeBoth)
        {
            var hmi = new HMIRuntime();
            // WinCC VBS API：HMIRuntime.Logging.DataLogs.Remove SourcePath, Type
            // Type 参数在 COM 接口中为 string，传整数值的字符串形式
            int result = (int)hmi.Logging.Remove(sourcePath, ((int)archiveType).ToString());
            if (result == 0)
                _connectedFolders.Remove(sourcePath);
            return result;
        }


        /// <summary>
        /// 扫描指定文件夹，返回识别到的 WinCC 备份归档文件列表（显示用）。
        /// 每项为 (显示名称, 解析出的时间范围)。
        /// </summary>
        public static List<BackupFileInfo> GetBackupFilesInFolder(string folderPath)
        {
            var result = new List<BackupFileInfo>();
            if (!Directory.Exists(folderPath)) return result;

            // WinCC 备份归档以 SQL Server 数据文件对存放，主文件后缀为 .ldf
            var files = Directory.GetFiles(folderPath, "*.ldf", SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
                files = Directory.GetFiles(folderPath, "*.mdf", SearchOption.TopDirectoryOnly);

            foreach (var f in files.OrderBy(x => x))
            {
                string name  = Path.GetFileNameWithoutExtension(f);
                var    range = ParseBackupTimeRange(name);
                result.Add(new BackupFileInfo
                {
                    Name      = name,
                    TimeRange = range
                });
            }
            return result;
        }

        // 从 WinCC 备份归档文件名中解析时间范围
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
            // WinCC 备份文件名中的时间为 UTC，解析后标记 Kind=Utc
            if (!DateTime.TryParseExact(s, "yyyyMMddHHmm",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out dt))
                return false;
            dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            return true;
        }


        /// <summary>
        /// 根据查询时间范围，自动调用 Restore 连接对应的备份归档。
        /// 已记录连接的文件夹不会重复连接。
        /// </summary>
        /// <param name="backupFolder">备份文件夹路径。</param>
        /// <param name="queryStart">查询开始时间（UTC）。</param>
        /// <param name="queryEnd">查询结束时间（UTC）。</param>
        /// <returns>Restore 返回值（0/1=正常，其余=错误）；-999=跳过（已连接）。</returns>
        public static int AutoConnectForTimeRange(
            string   backupFolder,
            DateTime queryStart,
            DateTime queryEnd)
        {
            if (string.IsNullOrWhiteSpace(backupFolder) || !Directory.Exists(backupFolder))
                return -1;

            // 若该文件夹已连接过，不重复连接
            if (_connectedFolders.Contains(backupFolder))
                return -999;

            return ConnectBackup(backupFolder, queryStart, queryEnd);
        }
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
                // 文件名时间为 UTC，显示时转为本地时间方便用户识别
                DateTime localStart = TimeRange.Value.Start.ToLocalTime();
                DateTime localEnd   = TimeRange.Value.End.ToLocalTime();
                return $"{Name}  [{localStart:yyyy-MM-dd HH:mm} ~ {localEnd:yyyy-MM-dd HH:mm}]";
            }
            return Name;
        }
    }
}
