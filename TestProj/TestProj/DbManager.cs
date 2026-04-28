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
        /// <param name="tagNames">变量全名列表</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="timeStepSeconds">0=原始点，>0=按秒聚合均值</param>
        public static DataTable FetchData(
            List<string> tagNames,
            DateTime start,
            DateTime end,
            int timeStepSeconds = 0)
        {
            var result = new DataTable();
            if (tagNames == null || tagNames.Count == 0)
                return result;

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

                // ValueID → TagName
                var dbTags = FetchAllTags();
                var idToName = dbTags.ToDictionary(t => t.ValueID, t => t.ValueName);

                if (!result.Columns.Contains("TagName"))
                    result.Columns.Add("TagName", typeof(string));

                foreach (DataRow row in result.Rows)
                {
                    if (result.Columns.Contains("ValueID")
                        && row["ValueID"] != DBNull.Value)
                    {
                        long vid = Convert.ToInt64(row["ValueID"]);
                        row["TagName"] = idToName.TryGetValue(vid, out string name)
                                         ? name : vid.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据失败: {ex.Message}", "错误");
            }

            return result;
        }

        private static string BuildQuery(List<string> tagNames,DateTime start,DateTime end,int timeStepSeconds)
        {
            string tagList = tagNames.Count == 1
                ? $"'{tagNames[0]}'"
                : $"({string.Join(";", tagNames.Select(t => $"'{t}'"))})";

            string timeBegin = start.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string timeEnd = end.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // timeStepSeconds=0 → 原始点，不带 TIMESTEP
            // timeStepSeconds>0 → 聚合均值，type=5
            string timeStep = timeStepSeconds > 0
                ? $",'TIMESTEP={timeStepSeconds},5'"
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