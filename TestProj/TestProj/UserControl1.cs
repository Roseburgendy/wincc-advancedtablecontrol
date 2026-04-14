using CCHMIRUNTIME;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            string json = @"[
        {
            'tagName': 'tag1',
            'displayName': 'Tag 1',
            'normalMin': 0,
            'normalMax': 100,
            'decimalPlaces': 2,
            'order': 1
        },
        {
            'tagName': 'tag2',
            'displayName': 'Tag 2',
            'normalMin': -50,
            'normalMax': 50,
            'decimalPlaces': 1,
            'order': 2
        }
    ]";

            var list = ConfigManager.ParseFromJson(json);
            // 结果拼成字符串显示
            var sb = new StringBuilder();
            foreach (var tag in list)
            {
                sb.AppendLine($"tagName: {tag.tagName}");
                sb.AppendLine($"displayName: {tag.displayName}");
                sb.AppendLine($"normalMin: {tag.normalMin}");
                sb.AppendLine($"normalMax: {tag.normalMax}");
                sb.AppendLine($"decimalPlaces: {tag.decimalPlaces}");
                sb.AppendLine($"order: {tag.order}");
                sb.AppendLine("---");
            }

            MessageBox.Show(sb.ToString(), "JSON 反序列化结果");
        }
    }
}
