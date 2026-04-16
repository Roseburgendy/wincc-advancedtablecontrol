using CCHMIRUNTIME;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TestProj
{
    public static class DbManager
    {
        private static OleDbConnection _conn;

        /// <summary>
        /// Get Connection with 
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection GetConnection()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
                return _conn;
            // 读取WINCC系统变量
            HMIRuntime HMIRT = new HMIRuntime();
            IHMITag Catalog = HMIRT.Tags["@DatasourceNameRT"];
            IHMITag PCName = HMIRT.Tags["@LocalMachineName"];

            // 连接字符串
            string ConnectionStr = @"Provider=WinCCOLEDBProvider.1;Catalog=" + Catalog.Read() + ";Data Source=" + PCName.Read() + "\\WINCC";

            // 打开连接
            _conn = new OleDbConnection(ConnectionStr);
            _conn.Open();
            return _conn;
        }


        /// <summary>
        /// Close Connection
        /// </summary>
        public static void CloseConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
                _conn = null;
            }
        }

        /// <summary>
        /// Fetch all tags in the WINCC archive database
        /// </summary>
        /// <returns>
        /// a list contains (ValueID, ValueName) of all tags in the WINCC archive database
        /// </returns>
        public static List<(long ValueID, string ValueName)> FetchAllTags()
        {
            var list = new List<(long,string)>();
            try
            {
                // Use SQL connection to read tags from WINCC archive database
                // OLEDB looks like it doesn't support reading schema info, so we have to use SQL connection directly
                HMIRuntime hmirt = new HMIRuntime();
                string pcName = hmirt.Tags["@LocalMachineName"].Read().ToString();
                string catalog = hmirt.Tags["@DatasourceNameRT"].Read().ToString();

                string sqlConnStr = $"Server={pcName}\\WINCC;" +
                                    $"Database={catalog};" +
                                    "Integrated Security=SSPI;";

                // select value id, name from the database
                using (var sqlConn = new SqlConnection(sqlConnStr))
                {
                    sqlConn.Open();

                    var cmd = new SqlCommand(
                        "SELECT ValueID, ValueName FROM dbo.Archive " +
                        "WHERE ValueName IS NOT NULL " +
                        "ORDER BY ValueName", sqlConn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                        list.Add((reader.GetInt64(0), reader.GetString(1)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取变量列表失败: {ex.Message}", "错误");
            }
            return list;
        }
    }
}
