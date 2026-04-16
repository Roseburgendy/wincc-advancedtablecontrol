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
            ConfigManager.LoadConfig();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // 声明OLEDB对象
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();

            // 打开连接
            OleDbConnection conn = DbManager.GetConnection();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            // 查询数据
            string startTime = DT_begin.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = DT_end.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");

            // TODO:read tag with OLEDB
            // TagConfig 面板用 DbManager.FetchAllTags() 读变量列表
            // 用户勾选后，选中的变量存起来, datagridview1显示选中的变量列表，searchBtn_Click 用选中的变量查询
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
            var config = ConfigManager.Config;

            if (config == null )
            {
                MessageBox.Show("Config is null"); return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"归档名：{config.DataSource.ArchiveName}");
            sb.AppendLine($"变量数量:{config.TagMeta.Count}");
            sb.AppendLine("----");
            foreach (var tag in config.TagMeta)
            {
                sb.AppendLine($"{tag.TagName} | {tag.DisplayName} | {tag.NormalMin}~{tag.NormalMax}");
            }

            MessageBox.Show(sb.ToString(), "JSON 读取结果");
        }

        private void TagConfigBtn_Click(object sender, EventArgs e)
        {
            // TODO: Open tag config panel
            var panel = new TagConfigPanel();
            var form = new Form
            {
                Text = "变量配置",
                Width = 1300,
                Height = 1000,
                StartPosition = FormStartPosition.CenterScreen
            };
            panel.Dock = DockStyle.Fill;
            form.Controls.Add( panel );
            form.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
