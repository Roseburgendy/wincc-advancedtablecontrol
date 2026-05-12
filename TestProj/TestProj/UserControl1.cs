using CCHMIRUNTIME;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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

        private void SearchBtn_Click(object sender, EventArgs e) { }

        private void TimeSelectBtn_Click(object sender, EventArgs e)
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

            using (var panel = new TimeRangeSelectorPanel())
            {
                // 必须在 ShowDialog 之前订阅，否则事件永远不触发
                panel.Applied += (start, end, mode, timeStep) =>
                {
                    RunQuery(sortedEntries,
                             start.ToUniversalTime(),
                             end.ToUniversalTime(),
                             timeStep);
                };
                panel.ShowDialog();
            }
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
                string reportTitle = _currentTemplate?.DisplayName
                             ?? ConfigManager.Config?.CompanyName
                             ?? "报表";
                dlg.FileName = $"{reportTitle}_{DateTime.Now:yyyyMMdd_HHmmss}";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                ExportToExcel(dlg.FileName);
                MessageBox.Show("导出成功", "提示");
            }
        }
        private void ExportToExcel(string filePath)
        {
            var dt = (dataGridView1.DataSource as DataTable);
            if (dt == null) return;

            int colCount = dt.Columns.Count;
            string reportTitle = _currentTemplate?.DisplayName
                              ?? ConfigManager.Config?.CompanyName
                              ?? "数据报表";

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("数据报表");

                // 报表标题（合并居中）
                ws.Cells[1, 1, 1, colCount].Merge = true;
                ws.Cells[1, 1].Value = reportTitle;
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // 列标题（加粗居中，带表格线）
                int colHeaderRow = 2;
                for (int c = 0; c < colCount; c++)
                {
                    var cell = ws.Cells[colHeaderRow, c + 1];
                    string colName = dt.Columns[c].ColumnName;
                    cell.Value = colName == "Timestamp" ? "日期" : colName;
                    cell.Style.Font.Bold = true;
                    cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    SetThinBorder(cell);
                }

                // 数据行 
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        var cell = ws.Cells[colHeaderRow + 1 + r, c + 1];
                        var val = dt.Rows[r][c];
                        cell.Value = val == DBNull.Value ? null : val;
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        SetThinBorder(cell);

                        if (dt.Columns[c].ColumnName == "Timestamp")
                        {
                            cell.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                            continue;
                        }

                        var meta = _tagMetaList.FirstOrDefault(
                            t => t.DisplayName == dt.Columns[c].ColumnName
                              || t.TagName == dt.Columns[c].ColumnName);

                        if (meta != null && val != DBNull.Value
                            && double.TryParse(val.ToString(), out double dval))
                        {
                            cell.Style.Numberformat.Format = "0." + new string('0', meta.DecimalPlaces);

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

                // 紧凑自适应列宽（minimumWidth=0 去掉额外 padding）
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                package.SaveAs(new System.IO.FileInfo(filePath));
            }
        }

        private static void SetThinBorder(ExcelRange cell)
        {
            var border = cell.Style.Border;
            border.Top.Style    = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            border.Left.Style   = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            border.Right.Style  = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
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

        #region 打印功能

        private DataTable _printData;
        private float[]   _printColWidths;
        private List<List<int>> _printColGroups;
        private int    _printCurrentGroup;
        private int    _printCurrentRow;
        private string _printTitle;
        private bool   _printLayoutReady;

        public void PrintBtn_Click(object sender, EventArgs e)
        {
            _printData = dataGridView1.DataSource as DataTable;
            if (_printData == null || _printData.Rows.Count == 0)
            {
                MessageBox.Show("请先查询数据", "提示");
                return;
            }

            _printTitle = _currentTemplate?.DisplayName ?? "数据报表";

            var doc = new PrintDocument();

            using (var setup = new PageSetupDialog
            {
                PageSettings   = doc.DefaultPageSettings,
                AllowOrientation = true,
                AllowMargins   = true
            })
            {
                if (setup.ShowDialog() != DialogResult.OK) return;
                doc.DefaultPageSettings = setup.PageSettings;
            }

            doc.BeginPrint += (s, ev) =>
            {
                _printCurrentRow   = 0;
                _printCurrentGroup = 0;
                _printColGroups    = null;
                _printLayoutReady  = false;
            };
            doc.PrintPage += PrintDoc_PrintPage;

            using (var preview = new PrintPreviewDialog
            {
                Document    = doc,
                WindowState = FormWindowState.Maximized
            })
                preview.ShowDialog();
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g      = e.Graphics;
            var bounds = e.MarginBounds;

            using (var titleFont  = new Font("Arial", 14f, FontStyle.Bold))
            using (var headerFont = new Font("Arial",  9f, FontStyle.Bold))
            using (var dataFont   = new Font("Arial",  8f))
            {
                if (!_printLayoutReady)
                {
                    BuildPrintLayout(g, bounds.Width, headerFont);
                    _printLayoutReady = true;
                }

                var   colIndices = _printColGroups[_printCurrentGroup];
                float groupW     = colIndices.Sum(c => _printColWidths[c]);
                float scale      = groupW <= bounds.Width ? 1f : bounds.Width / groupW;
                var   scaledW    = colIndices.Select(c => _printColWidths[c] * scale).ToArray();

                float x0         = bounds.Left;
                float y          = bounds.Top;
                float pageBottom = bounds.Bottom;
                float titleH     = titleFont.GetHeight(g)  + 6f;
                float hdrH       = headerFont.GetHeight(g) + 6f;
                float rowH       = dataFont.GetHeight(g)   + 4f;

                // ── 标题（每组列的首页显示一次） ──
                if (_printCurrentRow == 0)
                {
                    using (var sf = new StringFormat
                    {
                        Alignment     = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    })
                        g.DrawString(_printTitle, titleFont, Brushes.Black,
                            new RectangleF(x0, y, bounds.Width, titleH), sf);
                    y += titleH + 6f;
                }

                // ── 列标题行（灰底加粗居中） ──
                float hx = x0;
                for (int i = 0; i < colIndices.Count; i++)
                {
                    int    c    = colIndices[i];
                    string hdr  = _printData.Columns[c].ColumnName == "Timestamp"
                                  ? "日期" : _printData.Columns[c].ColumnName;
                    var rect = new RectangleF(hx, y, scaledW[i], hdrH);
                    g.FillRectangle(Brushes.LightGray, rect);
                    g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
                    using (var sf = new StringFormat
                    {
                        Alignment     = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    })
                        g.DrawString(hdr, headerFont, Brushes.Black, rect, sf);
                    hx += scaledW[i];
                }
                y += hdrH;

                // ── 数据行（间隔底色 + 灰色边框） ──
                bool alt = false;
                while (_printCurrentRow < _printData.Rows.Count && y + rowH <= pageBottom)
                {
                    hx = x0;
                    for (int i = 0; i < colIndices.Count; i++)
                    {
                        int    c   = colIndices[i];
                        var    val = _printData.Rows[_printCurrentRow][c];
                        string txt = val == DBNull.Value ? "" : val.ToString();

                        if (_printData.Columns[c].ColumnName == "Timestamp" && val is DateTime dt)
                            txt = dt.ToString("yyyy-MM-dd HH:mm:ss");

                        var rect = new RectangleF(hx, y, scaledW[i], rowH);
                        if (alt) g.FillRectangle(Brushes.WhiteSmoke, rect);
                        g.DrawRectangle(Pens.Gray, rect.X, rect.Y, rect.Width, rect.Height);
                        using (var sf = new StringFormat
                        {
                            Alignment     = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center,
                            FormatFlags   = StringFormatFlags.NoWrap
                        })
                            g.DrawString(txt, dataFont, Brushes.Black, rect, sf);
                        hx += scaledW[i];
                    }
                    y += rowH;
                    _printCurrentRow++;
                    alt = !alt;
                }

                // ── 分页判断 ──
                if (_printCurrentRow < _printData.Rows.Count)
                {
                    e.HasMorePages = true;  // 当前列组还有剩余行，继续下一页
                }
                else if (_printCurrentGroup < _printColGroups.Count - 1)
                {
                    _printCurrentGroup++;   // 列超宽，切换到下一列组
                    _printCurrentRow = 0;
                    e.HasMorePages   = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
        }

        private void BuildPrintLayout(Graphics g, float printableWidth, Font headerFont)
        {
            int colCount    = _printData.Columns.Count;
            _printColWidths = new float[colCount];

            for (int c = 0; c < colCount; c++)
            {
                string hdr = _printData.Columns[c].ColumnName == "Timestamp"
                             ? "日期" : _printData.Columns[c].ColumnName;
                float w = g.MeasureString(hdr, headerFont).Width + 16f;
                _printColWidths[c] = Math.Max(w, 60f);
            }

            // 按可打印宽度分组，超宽则强制新建一组（新页）
            _printColGroups = new List<List<int>>();
            var   group = new List<int>();
            float gw    = 0f;
            for (int c = 0; c < colCount; c++)
            {
                if (group.Count > 0 && gw + _printColWidths[c] > printableWidth)
                {
                    _printColGroups.Add(group);
                    group = new List<int>();
                    gw    = 0f;
                }
                group.Add(c);
                gw += _printColWidths[c];
            }
            if (group.Count > 0) _printColGroups.Add(group);
        }

        #endregion

     
    }
}
