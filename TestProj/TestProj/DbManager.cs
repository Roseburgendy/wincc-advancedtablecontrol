using CCHMIRUNTIME;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestProj.Models;

namespace TestProj
{
    public static class DbManager
    {
        private static OleDbConnection _conn;

        /// <summary>
        /// 获取与 OLE DB Provider 连接
        /// </summary>
        public static OleDbConnection GetConnection()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
                return _conn;

            HMIRuntime hmirt = new HMIRuntime();
            string catalog = hmirt.Tags["@DatasourceNameRT"].Read().ToString();
            string pcName = hmirt.Tags["@LocalMachineName"].Read().ToString();

            if (ConfigManager.Config == null)
            {
                string dtname = hmirt.Tags["@DatasourceNameRT"].Read().ToString();
                ConfigManager.Init(dtname);
            }
            string connStr = $"Provider=WinCCOLEDBProvider.1;" +
                             $"Catalog={catalog};" +
                             $"Data Source={pcName}\\WINCC";

            _conn = new OleDbConnection(connStr);
            _conn.Open();
            return _conn;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public static void CloseConnection()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
            {
                _conn.Close();
                _conn = null;
            }
        }

        /// <summary>
        /// 获取所有归档变量（通过 SQL Server 直连）
        /// </summary>
        public static List<(long ValueID, string ValueName, string DataType)> FetchAllTags()
        {
            var list = new List<(long, string, string)>();
            try
            {
                HMIRuntime hmirt = new HMIRuntime();
                string pcName = hmirt.Tags["@LocalMachineName"].Read().ToString();
                string catalog = hmirt.Tags["@DatasourceNameRT"].Read().ToString();

                string sqlConnStr = $"Server={pcName}\\WINCC;" +
                                    $"Database={catalog};" +
                                    "Integrated Security=SSPI;";

                using (var sqlConn = new SqlConnection(sqlConnStr))
                {
                    sqlConn.Open();
                    var cmd = new SqlCommand(
                        "SELECT ValueID, ValueName, VarType FROM dbo.Archive " +
                        "WHERE ValueName IS NOT NULL " +
                        "ORDER BY ValueName", sqlConn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                        list.Add((reader.GetInt64(0),
                                  reader.GetString(1),
                                  MapVarType(reader.GetInt32(2))));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取变量列表失败: {ex.Message}", "错误");
            }
            return list;
        }
        /// <summary>
        /// 查询过程值归档数据
        /// </summary>
        /// <param name="valueIds">ValueID 列表</param>
        /// <param name="start">开始时间（UTC）</param>
        /// <param name="end">结束时间（UTC）</param>
        /// <param name="timeStepSeconds">0=原始点，>0=按秒聚合均值</param>
        public static DataTable FetchData(
            List<long> valueIds,
            DateTime start,
            DateTime end,
            int timeStepSeconds)
        {
            var result = new DataTable();
            if (valueIds == null || valueIds.Count == 0)
                return result;

            const int batchSize = 20;
            var batches = valueIds
                .Select((id, idx) => new { id, idx })
                .GroupBy(x => x.idx / batchSize)
                .Select(g => g.Select(x => x.id).ToList())
                .ToList();

            try
            {
                var conn = GetConnection();

                foreach (var batch in batches)
                {
                    string query = BuildQuery(batch, start, end, timeStepSeconds);
                    var cmd = new OleDbCommand(query, conn);
                    var adapter = new OleDbDataAdapter(cmd);
                    var batchTable = new DataTable();
                    adapter.Fill(batchTable);

                    if (result.Columns.Count == 0)
                        result = batchTable;
                    else
                        foreach (DataRow row in batchTable.Rows)
                            result.ImportRow(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据失败: {ex.Message}", "错误");
            }

            return result;
        }

        /// <summary>
        /// 查询从 start 开始，每个变量最多取 count 条原始数据点。
        /// 采用渐进式窗口策略：先查小窗口，数据不足再逐步扩大，
        /// 避免一次性拉取全量归档数据导致的性能问题。
        /// </summary>
        public static DataTable FetchDataByCount(List<long> valueIds, DateTime start, int count)
        {
            if (valueIds == null || valueIds.Count == 0 || count <= 0)
                return new DataTable();

            DateTime utcNow = DateTime.UtcNow;
            // 如果 start 在未来，则以 start 为基准向后延伸
            if (utcNow <= start) utcNow = start.AddDays(365);

            // 渐进窗口：依次尝试 1h → 6h → 1d → 7d → 30d → 365d
            var windows = new[]
            {
                TimeSpan.FromHours(1),
                TimeSpan.FromHours(6),
                TimeSpan.FromDays(1),
                TimeSpan.FromDays(7),
                TimeSpan.FromDays(30),
                TimeSpan.FromDays(365)
            };

            foreach (var window in windows)
            {
                DateTime end        = start + window;
                bool isLastWindow   = end >= utcNow;
                if (isLastWindow) end = utcNow;

                var data = FetchData(valueIds, start, end, 0);

                if (data.Rows.Count > 0)
                {
                    // 检查是否至少有一个变量已积累到 count 条
                    bool satisfied = !data.Columns.Contains("ValueID") || data
                        .AsEnumerable()
                        .GroupBy(r => Convert.ToInt64(r["ValueID"]))
                        .Any(g => g.Count() >= count);

                    if (satisfied || isLastWindow)
                        return TrimByCount(data, count);
                }
                else if (isLastWindow)
                {
                    return data; // 到最大窗口仍无数据，直接返回空表
                }
                // 数据不足且窗口未到上限，继续扩大
            }

            return new DataTable();
        }

        /// <summary>
        /// 将 DataTable 按 ValueID 分组，每组保留时间最早的 count 条
        /// </summary>
        private static DataTable TrimByCount(DataTable data, int count)
        {
            var limited     = data.Clone();
            var perTagCount = new Dictionary<long, int>();
            bool hasVidCol  = data.Columns.Contains("ValueID");

            foreach (DataRow row in data.AsEnumerable().OrderBy(r => r["Timestamp"]))
            {
                long vid = hasVidCol ? Convert.ToInt64(row["ValueID"]) : 0;
                perTagCount.TryGetValue(vid, out int c);
                if (c < count)
                {
                    limited.ImportRow(row);
                    perTagCount[vid] = c + 1;
                }
            }
            return limited;
        }

        private static string BuildQuery(List<long> valueIds, DateTime start, DateTime end, int timeStepSeconds)
        {
            // ValueID 查询不加引号
            string tagList = valueIds.Count == 1
                ? valueIds[0].ToString()
                : $"({string.Join(";", valueIds)})";

            string timeBegin = start.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string timeEnd   = end.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string timeStep = timeStepSeconds > 0
                ? $",'TIMESTEP={timeStepSeconds},1'"
                : "";

            return $"TAG:R,{tagList},'{timeBegin}','{timeEnd}'{timeStep}";
        }
        

        /// <summary>
        /// 映射变量类型
        /// </summary>
        public static string MapVarType(int varType)
        {
            switch (varType)
            {
                case 19: return "Float";
                case 20: return "Double";
                case 6: return "Int16";
                case 7: return "Int32";
                case 8: return "UInt16";
                case 9: return "UInt32";
                case 11: return "Bool";
                default: return $"Unknown({varType})";
            }
        }
    }
}