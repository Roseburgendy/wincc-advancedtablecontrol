using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCHMIRUNTIME;
using System.Data.OleDb;

namespace TestProj
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // 读取WINCC系统变量
            HMIRuntime HMIRT = new HMIRuntime();  
            IHMITag Catalog = HMIRT.Tags["@DatasourceNameRT"];
            IHMITag PCName = HMIRT.Tags["@LocalMachineName"];

            // 声明OLEDB对象
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();

            // 连接字符串
            string ConnectionStr = @"Provider = WinCCOLEDBProvider.1;Catalog=" + Catalog.Read() + ";Data Source=" + PCName.Read() + "\\WINCC";

            // 打开连接
            OleDbConnection conn = new OleDbConnection(ConnectionStr);
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            // 查询数据
            string startTime = DT_begin.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = DT_end.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            cmd.CommandText = $"TAG:R,('Archive\\tag1';'Archive\\tag2'),'{startTime}','{endTime}'";

            da.SelectCommand = cmd;
            da.Fill(ds);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns["Timestamp"].ReadOnly = false;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Timestamp"] != DBNull.Value)
                    {
                        DateTime utcTime = (DateTime)row["Timestamp"];
                        // 将 UTC 时间转换为本地时间（北京时间）
                        row["Timestamp"] = utcTime.ToLocalTime();
                    }
                }

                // 绑定到 DataGridView 显示结果
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Timestamp"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss.fff";
            }

            conn.Close();
        }
        // unused event handlers
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
