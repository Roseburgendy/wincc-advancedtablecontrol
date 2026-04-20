using CCHMIRUNTIME;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using TestProj.Models;

namespace TestProj
{
    public partial class UserControl1: UserControl
    {
        /// <summary>
        /// 存储已选择的变量,format: <ArchiveName>//<TagName>
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
        private void SearchBtn_Click(object sender, EventArgs e)
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

                    var dbTags = DbManager.FetchAllTags();
                    var idToName = dbTags.ToDictionary(t => t.ValueID, t => t.ValueName);

                    dt.Columns.Add("TagName", typeof(string));
                    // tagname
                    foreach(DataRow row in dt.Rows)
                    {
                       long vid = Convert.ToInt64(row["ValueID"]);
                       row["TagName"] = idToName.TryGetValue(vid, out string name) ? name : vid.ToString();
                    }
                    // 日期时间
                    dt.Columns["Timestamp"].ReadOnly = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Timestamp"] != DBNull.Value)
                        {
                            // 将 UTC 时间转换为本地时间（北京时间）
                            row["Timestamp"] = ((DateTime)row["Timestamp"]).ToLocalTime();
                        }
                    }
                    DataTable wide = PivotToWide(dt, _selectedTags);

                    foreach(var col in new[] {"Quality", "Flags","ValueID"})
                        if(wide.Columns.Contains(col))
                            wide.Columns.Remove(col);

                    // 绑定到 DataGridView 显示结果
                    dataGridView1.DataSource = wide;
                    dataGridView1.Columns["Timestamp"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (col.Name == "Timestamp")
                            col.Width = 300;
                        else
                            col.Width = 100; // 数据列统一宽度
                    }
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

        private DataTable PivotToWide(DataTable raw, List<string> tags)
        {
            var wide = new DataTable();
            // 添加时间戳列
            wide.Columns.Add("Timestamp", typeof(DateTime));

            var allMeta = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();
            var nameMap = allMeta.ToDictionary(t => t.TagName, t=> t.DisplayName);

            // 添加变量列
            foreach (var tag in tags)
            {
                // 有displayName就用，没有就用TagName
                string colName = nameMap.TryGetValue(tag, out string display) ? display : tag;
                wide.Columns.Add(colName, typeof(double));
            }

            // 从TagName - ColName 映射
            var tagToCol = tags.ToDictionary(tag => tag,
                tag => nameMap.TryGetValue(tag, out string display) ? display : tag);

            var groups = raw.AsEnumerable().GroupBy(r => r["Timestamp"]);

            // pivot to wide table
            foreach(var group in groups)
            {
                var row = wide.NewRow();
                row["Timestamp"] = group.Key;
                foreach(var item in group)
                {
                    string tagName = item["TagName"].ToString();
                    if (tagToCol.TryGetValue(tagName, out string colName) 
                        && item["RealValue"] != DBNull.Value)
                    {
                        if(double.TryParse(item["RealValue"].ToString(), out double value))
                            row[colName] = value;
                    }
                }
                wide.Rows.Add(row);
            }
            return wide.DefaultView.ToTable();
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

            // 关闭时检查未保存修改
            panel.FormClosing += (s, args) =>
            {
                if (panel.HasUnsavedChanges())
                {
                    var result = MessageBox.Show(
                        "有未保存的修改，是否在关闭前保存？",
                        "提示",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                        panel.SaveAll();          // 保存后继续关闭
                    else if (result == DialogResult.Cancel)
                        args.Cancel = true;       // 取消关闭，回到界面
                    else if (result == DialogResult.No)
                        panel.DiscardAllChanges();
                }
            };

            panel.ShowDialog();
        }

        /// <summary>
        /// 变量选择按钮点击事件，弹出TagSelection Panel
        /// </summary>
        private void SelectTagBtn_Click(object sender, EventArgs e)
        {
            var panel = new TagSelectionPanel(_selectedTags);

            if (panel.ShowDialog() == DialogResult.OK)
            {
                _selectedTags = panel.SelectedTags;
                lblSelectedTags.Text = string.Join(";  ",
                    _selectedTags.Select(t => t.Contains("\\")
                        ? t.Split('\\').Last() : t));
            }
        }
    }
}
