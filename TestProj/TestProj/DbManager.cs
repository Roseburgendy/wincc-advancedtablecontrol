using CCHMIRUNTIME;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        /// <param name="tagNames">变量全名列表，格式 "归档名\变量名"</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="mode">
        /// 0 = 显示从开始时间起某个定义时间范围（例如，从开始时间起 30 分钟）内的表格值
        /// 1 = 显示某个定义时间间隔内的表格值.
        /// 2 = 显示从开始时间起的某个定义数量的值，例如从开始时间起的 100 个值
        /// </param>
        /// <param name="timeStepSeconds">mode=0 时的聚合粒度（秒）</param>
        /// <param name="aggregationType">
        /// 聚合类型，仅 mode=0 有效：
        /// 5=无插补均值, 261=有插补均值,
        /// 3=无插补最小值, 4=无插补最大值
        /// </param>
        /// <param name="maxCount">mode=2 时的最大返回条数</param>
        /// <returns>原始 DataTable，列为 TagName / RealValue / TimeStamp</returns>
        public static DataTable FetchData(
            List<string> tagNames,
            DateTime start,
            DateTime end,
            int mode = 1,
            int timeStepSeconds = 3600,
            int aggregationType = 5,
            int maxCount = 100)
        {
            var result = new DataTable();

            if (tagNames == null || tagNames.Count == 0)
                return result;

            // OLE DB 单次查询最多 20 个变量，分批处理
            const int batchSize = 20;
            var batches = tagNames
                .Select((tag, idx) => new { tag, idx })
                .GroupBy(x => x.idx / batchSize)
                .Select(g => g.Select(x => x.tag).ToList())
                .ToList();

            try
            {
                var conn = GetConnection();

                foreach (var batch in batches)
                {
                    string query = BuildQuery(batch, start, end, mode,
                                              timeStepSeconds, aggregationType,
                                              maxCount);
                    var cmd = new OleDbCommand(query, conn);
                    var adapter = new OleDbDataAdapter(cmd);
                    var batchTable = new DataTable();
                    adapter.Fill(batchTable);

                    // 第一批直接用，后续批次追加行
                    if (result.Columns.Count == 0)
                    {
                        result = batchTable;
                    }
                    else
                    {
                        foreach (DataRow row in batchTable.Rows)
                            result.ImportRow(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据失败: {ex.Message}", "错误");
            }

            return result;
        }

        /// <summary>
        /// 构建 TAG:R 查询字符串
        /// </summary>
        private static string BuildQuery(
            List<string> tagNames,
            DateTime start,
            DateTime end,
            int mode,
            int timeStepSeconds,
            int aggregationType,
            int maxCount)
        {
            // 变量列表格式：('归档名\变量名';'归档名\变量名')
            string tagList = tagNames.Count == 1
                ? $"'{tagNames[0]}'"
                : $"({string.Join(";", tagNames.Select(t => $"'{t}'"))})";

            string timeBegin = start.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string timeEnd;
            string timeStep = "";

            switch (mode)
            {
                case 0: // 时间范围：带 TIMESTEP，聚合均值
                    timeEnd = end.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    timeStep = $",'TIMESTEP={timeStepSeconds},{aggregationType}'";
                    break;

                case 1: // 开始到结束：所有原始点
                    timeEnd = end.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    break;

                case 2: // 测量点数量：原始点，从 start 到记录末尾，结果截取
                    // TimeEnd = 0000... 表示读到归档最后一条
                    timeEnd = "0000-00-00 00:00:00.000";
                    break;

                default:
                    timeEnd = end.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    break;
            }

            string query = $"TAG:R,{tagList}," +
                           $"'{timeBegin}'," +
                           $"'{timeEnd}'" +
                           timeStep;

            return query;
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