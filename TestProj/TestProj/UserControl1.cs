using CCHMIRUNTIME;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace TestProj
{
    public partial class UserControl1: UserControl
    {
        /// <summary>
        /// 存储已选择的变量
        /// </summary>
        private List<string> _selectedTags = new List<string>();
        public UserControl1()
        {
            InitializeComponent();
            ConfigManager.LoadConfig();
        }

        /// <summary>
        /// 搜索按钮点击事件
        /// </summary>
        private void searchBtn_Click(object sender, EventArgs e)
        {
            if(_selectedTags == null || _selectedTags.Count == 0)
            {
                MessageBox.Show("请先选择变量", "提示");
                return;
            }
            try
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
                string tagList = string.Join(";",_selectedTags.Select(t => $"'{t}'"));
                // TODO:read tag with OLEDB
                // TagConfig 面板用 DbManager.FetchAllTags() 读变量列表
                // 用户勾选后，选中的变量存起来, datagridview1显示选中的变量列表，searchBtn_Click 用选中的变量查询

                cmd.CommandText = $"TAG:R,({tagList}),'{startTime}','{endTime}'";

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
                else
                {
                    MessageBox.Show("没有查询到数据", "提示");
                }
            }
            catch(Exception ex) {
                MessageBox.Show($"查询失败: {ex.Message}", "错误");

            }
            
        }

        /// <summary>
        /// 变量配置按钮点击事件，弹出TagConfig Panel
        /// </summary>
        private void TagConfigBtn_Click(object sender, EventArgs e)
        {
            if (_selectedTags == null || _selectedTags.Count == 0)
            {
                MessageBox.Show("请先选择变量", "提示");
                return;
            }

            var panel = new TagConfigPanel(_selectedTags);
            var form = new Form
            {
                Text = "变量配置",
                Width = 1300,
                Height = 1200,
                StartPosition = FormStartPosition.CenterScreen
            };
            panel.Dock = DockStyle.Fill;
            form.Controls.Add(panel);

            // 关闭时检查未保存修改
            form.FormClosing += (s, args) =>
            {
                if (panel.HasUnsavedChanges())
                {
                    var result = MessageBox.Show(
                        "有未保存的修改，是否保存？",
                        "提示", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Yes)
                        panel.SaveAll();
                    else if (result == DialogResult.Cancel)
                        args.Cancel = true;
                }
            };

            form.ShowDialog();
        }

        /// <summary>
        /// 变量选择按钮点击事件，弹出TagSelection Panel
        /// </summary>
        private void selectTagBtn_Click(object sender, EventArgs e)
        {
            var form = new TagSelectionPanel(_selectedTags);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 已选变量
                MessageBox.Show("Selected tags:\n" + string.Join("\n", _selectedTags));

            }
        }
    }
}
