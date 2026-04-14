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
            // 读取WINCC系统变量
            HMIRuntime HMIRT = new HMIRuntime();
            IHMITag Catalog = HMIRT.Tags["@DatasourceNameRT"];
            IHMITag PCName = HMIRT.Tags["@LocalMachineName"];

            // 连接字符串
            string ConnectionStr = @"Provider = WinCCOLEDBProvider.1;Catalog=" + Catalog.Read() + ";Data Source=" + PCName.Read() + "\\WINCC";

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
                var conn = GetConnection();
                var cmd = new OleDbCommand($"TAGLIST:'{archiveName}\\'", conn);
                var da = new OleDbDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                    foreach (DataRow row in ds.Tables[0].Rows)
                        if (row[0] != DBNull.Value)
                            list.Add(row[0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取变量列表失败: {ex.Message}", "错误");
            }
            return list;
        }
    }
}
