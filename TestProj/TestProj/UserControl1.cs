using CCHMIRUNTIME;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TestProj.Models;
namespace TestProj
{
    public partial class UserControl1: UserControl
    {
        private List<TagMeta> _tagMetaList = new List<TagMeta>(); // 变量信息
        private ReportTemplate _currentTemplate; // 模板对应选中变量名称和顺序
        public UserControl1()
        {
            InitializeComponent();

            try
            {
                DbManager.GetConnection(); // 内部会触发 ConfigManager.Init
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化连接失败: {ex.Message}", "错误");
                return;
            }

            if (ConfigManager.Config.TagMeta == null || ConfigManager.Config.TagMeta.Count == 0)
            {
                var tags = DbManager.FetchAllTags();
                ConfigManager.Config.TagMeta = tags.Select(t => new TagMeta
                {
                    ValueID = t.ValueID,
                    TagName = t.ValueName
                }).ToList();
                ConfigManager.SaveConfig();
            }

            InitializeTemplateList();
        }

        public void InitializeTemplateList()
        {
            cmbTemplate.DisplayMember = "DisplayName";
            cmbTemplate.ValueMember = "Id";
            cmbTemplate.DataSource = ConfigManager.Config?.Template
                                        ?? new List<ReportTemplate>();

            if (cmbTemplate.Items.Count > 0)
                cmbTemplate.SelectedIndex = 0;
        }

        /// <summary>
        /// 搜索按钮点击事件
        /// </summary>
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (_currentTemplate == null)
            {
                MessageBox.Show("请先选择报表模板", "提示");
                return;
            }

            var sortedEntries = _currentTemplate.SelectedTags
                .OrderBy(entry => entry.Order)
                .ToList();

            if (sortedEntries.Count == 0)
            {
                MessageBox.Show("当前模板没有配置变量", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var start = dtpStartDate.Value.Date + dtpStartTime.Value.TimeOfDay;
            var end   = dtpEndDate.Value.Date   + dtpEndTime.Value.TimeOfDay;

            if (start >= end)
            {
                MessageBox.Show("开始时间不能晚于结束时间", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var startUTC = start.ToUniversalTime();
            var endUTC   = end.ToUniversalTime();

            int timeStep = (int)numValue.Value * GetTimeStepSeconds();
            RunQuery(sortedEntries, startUTC, endUTC, timeStep);
        }

        private void RunQuery(List<TemplateTagEntry> entries, DateTime start, DateTime end, int timeStep)
        {
            try
            {
                var valueIds = entries.Select(e => e.ValueID).ToList();
                var raw = DbManager.FetchData(valueIds, start, end, timeStep);

                if (raw == null || raw.Rows.Count == 0)
                {
                    MessageBox.Show("没有查询到数据", "提示");
                    return;
                }

                foreach (DataRow row in raw.Rows)
                {
                    if (raw.Columns.Contains("Timestamp")
                        && row["Timestamp"] != DBNull.Value)
                        row["Timestamp"] = ((DateTime)row["Timestamp"]).ToLocalTime();
                }

                var wide = PivotToWide(raw, entries);
                CleanUnusedColumns(wide, new[] { "Quality", "Flags", "ValueID" });

                _tagMetaList = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();
                BindResultTable(wide);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}", "错误");
            }
        }

        /// <summary>
        /// 配置按钮点击事件
        /// </summary>
        private void BtnConfig_Click(object sender, EventArgs e)
        {
            using (var panel = new TemplateConfigPanel(
                ConfigManager.Config.Template, 
                ConfigManager.Config.TagMeta))
            {
                panel.ShowDialog();
            }
            // 关闭配置窗口后
            var selectedId = _currentTemplate?.Id;
            cmbTemplate.DataSource = null;
            cmbTemplate.DataSource = ConfigManager.Config?.Template;
            cmbTemplate.DisplayMember = "DisplayName";
            cmbTemplate.ValueMember = "Id";

            // 恢复之前选中的模板
            if (selectedId != null)
                cmbTemplate.SelectedValue = selectedId;
        }


        #region Utils
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
            // 强制清除旧列
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = table;
            if (dataGridView1.Columns.Contains("Timestamp"))
            {
                var tsCol = dataGridView1.Columns["Timestamp"];
                tsCol.HeaderText = "日期";
                tsCol.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }

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
        }

        /// <summary>
        /// 将 OLEDB 原始格式转换为宽表（以时间戳为行、变量为列）
        /// </summary>
        private DataTable PivotToWide(DataTable raw, List<TemplateTagEntry> sortedEntries)
        {
            var wide = new DataTable();
            wide.Columns.Add("Timestamp", typeof(DateTime));

            var allMeta = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();

            // ValueID → 列名（优先 DisplayName，fallback TagName，再 fallback ValueID）
            var idToCol = sortedEntries.ToDictionary(e => e.ValueID, e =>
            {
                var meta = allMeta.FirstOrDefault(m => m.ValueID == e.ValueID);
                return meta?.DisplayName ?? meta?.TagName ?? e.ValueID.ToString();
            });

            foreach (var colName in idToCol.Values)
                wide.Columns.Add(colName, typeof(double));

            var groups = raw.AsEnumerable().GroupBy(r => r["Timestamp"]);

            foreach (var group in groups)
            {
                var row = wide.NewRow();
                row["Timestamp"] = group.Key;
                foreach (var item in group)
                {
                    if (item["ValueID"] == DBNull.Value) continue;
                    long vid = Convert.ToInt64(item["ValueID"]);
                    if (idToCol.TryGetValue(vid, out string colName)
                        && item["RealValue"] != DBNull.Value
                        && double.TryParse(item["RealValue"].ToString(), out double val))
                    {
                        row[colName] = val;
                    }
                }
                wide.Rows.Add(row);
            }
            return wide;
        }


        private int GetTimeStepSeconds()
        {
            switch (cmbTimestep.SelectedIndex)
            {
                case 0: return 0;      // N/A，原始点
                case 1: return 1;      // 1秒
                case 2: return 60;     // 1分钟
                case 3: return 3600;   // 1小时
                case 4: return 86400;  // 1天
                default: return 0;
            }
        }

        #endregion

        #region 导出相关功能
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

        #endregion

        private void CmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTemplate = cmbTemplate.SelectedItem as ReportTemplate;
        }

        private void BtnImportFromExcel_Click(object sender, EventArgs e)
        {
           
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Excel 文件|*.xlsx";
                dlg.Title = "选择变量别名表";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                int dataStartRow, tagNameCol, displayNameCol;

                using(var cfg = new ExcelImportConfigForm())
                {
                    if(cfg.ShowDialog() != DialogResult.OK) return;
                    dataStartRow = cfg.DataStartRow;
                    tagNameCol = cfg.TagNameCol;
                    displayNameCol = cfg.DisplayNameCol;
                }

                try
                {
                    int updatedTags = 0;
                    int importedTemplates = 0;

                    using (var package = new ExcelPackage(new FileInfo(dlg.FileName)))
                    {
                        foreach (var sheet in package.Workbook.Worksheets)
                        {
                            if (sheet.Dimension == null) continue;
                            int rowCount = sheet.Dimension.End.Row;
                            var selectedTags = new List<TemplateTagEntry>();

                            for (int row = dataStartRow; row <= rowCount; row++)
                            {
                                string tagName = sheet.Cells[row, tagNameCol].Text?.Trim();
                                string displayName = sheet.Cells[row, displayNameCol].Text?.Trim();
                                if (string.IsNullOrEmpty(tagName)) continue;

                                // 更新 TagMeta DisplayName
                                var meta = ConfigManager.Config.TagMeta
                                    .FirstOrDefault(m =>
                                    {
                                        string shortName = m.TagName.Contains("\\")
                                            ? m.TagName.Split('\\')[1] : m.TagName;
                                        return shortName == tagName;
                                    });

                                if (meta != null && !string.IsNullOrEmpty(displayName))
                                {
                                    meta.DisplayName = displayName;
                                    updatedTags++;
                                }

                                if (meta == null || meta.ValueID == 0) continue;
                                selectedTags.Add(new TemplateTagEntry
                                {
                                    ValueID = meta.ValueID,
                                    Order   = selectedTags.Count + 1
                                });
                            }

                            if (selectedTags.Count == 0) continue;
                            if (ConfigManager.Config.Template == null)
                                ConfigManager.Config.Template = new List<ReportTemplate>();

                            // 写入 Template：同名则更新，否则新增
                            var existing = ConfigManager.Config.Template
                                .FirstOrDefault(t => t.DisplayName == sheet.Name);

                            // 写入template selected tags
                            if (existing != null)
                            {
                                existing.SelectedTags = selectedTags;
                            }
                            else
                            {
                                ConfigManager.Config.Template.Add(new ReportTemplate
                                {
                                    Id = "tpl_" + DateTime.Now.Ticks + importedTemplates,
                                    DisplayName = sheet.Name,
                                    Description = "",
                                    SelectedTags = selectedTags
                                });
                            }

                            importedTemplates++;
                        }
                    }

                    ConfigManager.SaveConfig();
                    MessageBox.Show(
                        $"导入完成：{importedTemplates} 个模板，更新 {updatedTags} 个变量别名。",
                        "成功");

                    InitializeTemplateList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导入失败: {ex.Message}", "错误");
                }
            }
        }
    }
}
