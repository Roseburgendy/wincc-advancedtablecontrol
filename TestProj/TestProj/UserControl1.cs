using CCHMIRUNTIME;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
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
        private List<TagMeta> _tagMetaList = new List<TagMeta>();
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
            if (_selectedTags == null || _selectedTags.Count == 0)
            {
                MessageBox.Show("请先选择变量", "提示");
                return;
            }

            try
            {
                var ds = ExecuteTagQuery(_selectedTags, DT_begin.Value, DT_end.Value);
                var raw = PrepareResultTable(ds);

                if (raw == null || raw.Rows.Count == 0)
                {
                    MessageBox.Show("没有查询到数据", "提示");
                    return;
                }

                var wide = PivotToWide(raw, _selectedTags);
                CleanUnusedColumns(wide, new[] { "Quality", "Flags", "ValueID" });

                _tagMetaList = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();
                BindResultTable(wide);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}", "错误");
            }
        }

        private DataSet ExecuteTagQuery(List<string> tags, DateTime begin, DateTime end)
        {
            var ds = new DataSet();
            string startTime = begin.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = end.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string tagList = string.Join(";", tags.Select(t => $"'{t}'"));

            using (var conn = DbManager.GetConnection())
            using (var cmd = conn.CreateCommand())
            using (var da = new OleDbDataAdapter())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"TAG:R,({tagList}),'{startTime}','{endTime}'";
                da.SelectCommand = (OleDbCommand)cmd;
                da.Fill(ds);
            }

            return ds;
        }

        private DataTable PrepareResultTable(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0) return null;
            var dt = ds.Tables[0];

            var dbTags = DbManager.FetchAllTags();
            var idToName = dbTags.ToDictionary(t => t.ValueID, t => t.ValueName);

            if (!dt.Columns.Contains("TagName"))
                dt.Columns.Add("TagName", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                if (dt.Columns.Contains("ValueID") && row["ValueID"] != DBNull.Value)
                {
                    long vid = Convert.ToInt64(row["ValueID"]);
                    row["TagName"] = idToName.TryGetValue(vid, out string name) ? name : vid.ToString();
                }

                if (dt.Columns.Contains("Timestamp") && row["Timestamp"] != DBNull.Value)
                {
                    row["Timestamp"] = ((DateTime)row["Timestamp"]).ToLocalTime();
                }
            }

            return dt;
        }

        private void CleanUnusedColumns(DataTable table, IEnumerable<string> cols)
        {
            foreach (var col in cols)
            {
                if (table.Columns.Contains(col))
                    table.Columns.Remove(col);
            }
        }

        private void BindResultTable(DataTable table)
        {
            dataGridView1.DataSource = null;  // 强制清除旧列
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = table;
            if (dataGridView1.Columns.Contains("Timestamp"))
                dataGridView1.Columns["Timestamp"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Width = col.Name == "Timestamp" ? 300 : 200;
            }

            dataGridView1.CellFormatting -= DataGridView1_CellFormatting;
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Timestamp") return;
            if(e.Value == null || e.Value == DBNull.Value) return;

            var meta = _tagMetaList.FirstOrDefault(t => t.DisplayName == colName
            || t.TagName == colName);

            if(!double.TryParse(e.Value.ToString(), out double val)) return;


            // 小数位
            string format = "F" + (meta?.DecimalPlaces ?? 2);
            e.Value = val.ToString(format);
            e.FormattingApplied = true; 

            // 超限定义
            bool exceedMax = (meta.NormalMax.HasValue && val > meta.NormalMax.Value);

            bool belowMin = (meta.NormalMin.HasValue && val < meta.NormalMin.Value);

            // 超过最大值： 加粗变红
            // 低于最小值 ： 加粗变黄
            // TODO: 自定义超限颜色
            if (exceedMax)
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            }
            else if (belowMin)
            {
                e.CellStyle.BackColor = Color.Yellow;
                e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            }
            else
            {
                e.CellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                e.CellStyle.Font = dataGridView1.Font;
            }
        }

        /// <summary>
        /// 将原本OLEDB自带数据源格式转换成以时间戳为行，变量为列的宽表格式，方便在DataGridView中显示
        /// </summary>
        /// <param name="raw"> 自带数据源</param>
        /// <param name="tags"> 变量信息</param>
        /// <returns></returns>
        private DataTable PivotToWide(DataTable raw, List<string> tags)
        {
            var wide = new DataTable();

            // 添加时间戳列
            wide.Columns.Add("Timestamp", typeof(DateTime));
            var allMeta = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();
            var nameMap = allMeta.ToDictionary(t => t.TagName, t=> t.DisplayName);

            var sortedTags = tags.OrderBy(t => {
                var meta = allMeta.FirstOrDefault(m => m.TagName == t);
                return meta?.Order.HasValue == true ? meta.Order.Value : int.MaxValue;
            }).ToList();
            // 添加变量列
            foreach (var tag in sortedTags)
            {
                // 有displayName就用，没有就用TagName
                string colName = nameMap.TryGetValue(tag, out string display) ? display : tag;
                wide.Columns.Add(colName, typeof(double));
            }

            // 从TagName - ColName 映射
            var tagToCol = sortedTags.ToDictionary(tag => tag,
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
            return wide;
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

        private void BtnToday_Click(object sender, EventArgs e)
        {
            DT_begin.Value = DateTime.Today;
            DT_end.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }
        private void BtnThisWeek_Click(object sender, EventArgs e)
        {
            int diff = (int)DateTime.Today.DayOfWeek - (int)DayOfWeek.Monday;
            if (diff<0) diff += 7;
            DT_begin.Value = DateTime.Today.AddDays(-diff);
            DT_end.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }
        private void BtnThisMonth_Click(object sender, EventArgs e)
        {
            DT_begin.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DT_end.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        private void ExportExcelBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("请先查询数据", "提示");
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel 文件|*.xlsx";
                dlg.FileName = $"报表_{DateTime.Now:yyyyMMdd_HHmmss}";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                ExportToExcel(dlg.FileName);
                MessageBox.Show("导出成功", "提示");
            }
        }
        private void ExportToExcel(string filePath)
        {
            var dt = (dataGridView1.DataSource as DataTable);
            if (dt == null) return;

            var cfg = ConfigManager.Config;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("数据报表");

                // ── 抬头 ──
                int headerRow = 1;
                ws.Cells[headerRow, 1].Value = cfg?.CompanyName ?? "";
                ws.Cells[headerRow, 1].Style.Font.Bold = true;
                ws.Cells[headerRow, 1].Style.Font.Size = 14;
                ws.Cells[headerRow, 1, headerRow, dt.Columns.Count].Merge = true;
                ws.Cells[headerRow, 1].Style.HorizontalAlignment =
                    OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Cells[headerRow + 1, 1].Value = $"导出时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                ws.Cells[headerRow + 1, 1].Style.Font.Italic = true;

                // ── 列标题 ──
                int colHeaderRow = headerRow + 3;
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    var cell = ws.Cells[colHeaderRow, c + 1];
                    cell.Value = dt.Columns[c].ColumnName;
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 114, 196));
                    cell.Style.Font.Color.SetColor(Color.White);
                    cell.Style.HorizontalAlignment =
                        OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                // ── 数据行 ──
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        var cell = ws.Cells[colHeaderRow + 1 + r, c + 1];
                        var val = dt.Rows[r][c];
                        cell.Value = val == DBNull.Value ? null : val;

                        // Timestamp 列格式
                        if (dt.Columns[c].ColumnName == "Timestamp")
                        {
                            cell.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                            continue;
                        }

                        // 超限标红
                        var meta = _tagMetaList.FirstOrDefault(
                            t => t.DisplayName == dt.Columns[c].ColumnName
                              || t.TagName == dt.Columns[c].ColumnName);

                        if (meta != null && val != DBNull.Value
                            && double.TryParse(val.ToString(), out double dval))
                        {
                            // 小数位
                            cell.Style.Numberformat.Format =
                                "0." + new string('0', meta.DecimalPlaces);

                            bool outOfRange =
                                (meta.NormalMin.HasValue && dval < meta.NormalMin.Value) ||
                                (meta.NormalMax.HasValue && dval > meta.NormalMax.Value);

                            if (outOfRange)
                            {
                                cell.Style.Font.Color.SetColor(Color.Red);
                                cell.Style.Font.Bold = true;
                            }
                        }
                    }
                }

                // ── 列宽自适应 ──
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                package.SaveAs(new System.IO.FileInfo(filePath));
            }
        }
    }
}
