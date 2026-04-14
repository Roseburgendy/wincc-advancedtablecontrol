using CCHMIRUNTIME;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TestProj
{
    public static class DbManager
    {
        private static OleDbConnection _conn;
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

        public static void CloseConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
                _conn = null;
            }
        }

        // 从归档读取所有变量名,并返回list
        public static List<string> FetchAllTags(string archiveName)
        {
            var list = new List<string>();
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

                using (var sqlConn = new System.Data.SqlClient.SqlConnection(sqlConnStr))
                {
                    sqlConn.Open();

                    var cmd = new System.Data.SqlClient.SqlCommand(
                        "SELECT ValueID, ValueName FROM dbo.Archive " +
                        "WHERE ValueName IS NOT NULL " +
                        "ORDER BY ValueName", sqlConn);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                        list.Add(reader.GetString(1));
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
