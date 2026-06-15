using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using TestProj.Models;
namespace TestProj
{
    public partial class UserControl1: UserControl
    {
        private List<TagMeta> _tagMetaList = new List<TagMeta>(); // 变量信息
        private ReportTemplate _currentTemplate; // 模板对应选中变量名称和顺序
        private DateTime _quickQueryDate = DateTime.Today; // 快捷日期查询的当前日期
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
            LoadSavedTheme();
            ApplyLanguage();
            ApplyIcons(kryptonManager.GlobalPaletteMode);
            RefreshBackupButtons();
        }

        public void InitializeTemplateList()
        {
            cmbTemplate.DisplayMember = "DisplayName";
            cmbTemplate.ValueMember   = "Id";
            cmbTemplate.DataSource    = ConfigManager.Config?.Template
                                        ?? new List<ReportTemplate>();

            if (cmbTemplate.Items.Count > 0)
                cmbTemplate.SelectedIndex = 0;
        }

        #region theme related
        // 启动时将保存的主题还原
        private void LoadSavedTheme()
        {
            string saved = ConfigManager.Config?.Theme ?? "Microsoft365Blue";
            if (Enum.TryParse<Krypton.Toolkit.PaletteMode>(saved, out var mode))
                kryptonManager.GlobalPaletteMode = mode;
        }

        /// <summary>根据当前语言刷新工具栏所有控件的文字。</summary>
        public void ApplyLanguage()
        {
            TimeSelectBtn.Values.Text        = LangHelper.Get("UC_TimeSelect");
            label3.Values.Text               = LangHelper.Get("UC_SelectTemplate");
            btnImportExcel.Values.Text       = LangHelper.Get("UC_ImportExcel");
            PrintBtn.Values.Text             = LangHelper.Get("UC_Print");
            btnConfig.Values.Text            = LangHelper.Get("UC_Config");
            ExportExcelBtn.Values.Text       = LangHelper.Get("UC_ExportExcel");
            btnConnectBackup.Values.Text    = LangHelper.Get("UC_ConnectBackup");
            btnDisconnectBackup.Values.Text = LangHelper.Get("UC_DisconnectBackup");
            btnTodayQuery.Values.Text       = LangHelper.Get("UC_TodayQuery");
            btnPrevDay.Values.Text          = LangHelper.Get("UC_PrevDay");
            btnNextDay.Values.Text          = LangHelper.Get("UC_NextDay");

            // 若表格已有数据，实时刷新列标题（语言切换后 DisplayName 行同步更新）
            if (dataGridView1.DataSource != null)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name == "Timestamp")
                        col.HeaderText = LangHelper.Get("UC_ColTimestamp");
                    else
                        col.HeaderText = BuildTagHeader(FindMetaByColName(col.Name), col.Name);
                }
            }
        }

        /// <summary>根据当前主题重新生成并赋值工具栏各按钮的图标。</summary>
        public void ApplyIcons(Krypton.Toolkit.PaletteMode mode)
        {
            TimeSelectBtn.Values.Image       = IconManager.Make(IconChar.CalendarDay,   mode, 40);
            btnImportExcel.Values.Image      = IconManager.Make(IconChar.FileImport,    mode, 40);
            PrintBtn.Values.Image            = IconManager.Make(IconChar.Print,         mode, 40);
            btnConfig.Values.Image           = IconManager.Make(IconChar.Sliders,       mode, 40);
            ExportExcelBtn.Values.Image      = IconManager.Make(IconChar.FileExcel,     mode, 40);
            btnConnectBackup.Values.Image    = IconManager.Make(IconChar.HardDrive,    mode, 40);
            btnDisconnectBackup.Values.Image = IconManager.Make(IconChar.TimesCircle,  mode, 40);
            btnTodayQuery.Values.Image       = IconManager.Make(IconChar.CalendarCheck, mode, 40);
            btnPrevDay.Values.Image          = IconManager.Make(IconChar.ChevronLeft,   mode, 40);
            btnNextDay.Values.Image          = IconManager.Make(IconChar.ChevronRight,  mode, 40);
        }

        #endregion
        
        #region Query Function

        /// <summary>显示/隐藏进度条并切换查询按钮的可用状态。</summary>
        private void SetQueryRunning(bool running)
        {
            progressBar1.Visible         = running;
            TimeSelectBtn.Enabled        = !running;
            ExportExcelBtn.Enabled       = !running;
            PrintBtn.Enabled             = !running;
            btnImportExcel.Enabled       = !running;
            btnConnectBackup.Enabled    = !running;
            btnDisconnectBackup.Enabled = !running;
            btnTodayQuery.Enabled       = !running;
            btnPrevDay.Enabled          = !running;
            btnNextDay.Enabled          = !running;
        }

        private async Task RunQueryAsync(List<TemplateTagEntry> entries, DateTime start, DateTime end, int timeStep)
        {
            SetQueryRunning(true);
            try
            {
                var cfg = ConfigManager.Config;

                // 安全检查 1：时间跨度上限
                int maxDays = cfg?.MaxQueryDays ?? 31;
                if (maxDays > 0)
                {
                    int spanDays = (int)Math.Ceiling((end - start).TotalDays);
                    if (spanDays > maxDays)
                    {
                        MessageBox.Show(
                            string.Format(LangHelper.Get("Msg_RangeExceedWarning"), spanDays, maxDays),
                            LangHelper.Get("Msg_Warning"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 自动连接备份
                if (cfg?.AutoConnectBackup == true)
                {
                    string bkFolder = cfg.BackupPath ?? "";
                    if (BackupManager.NeedsConnection(bkFolder, start, end))
                    {
                        using (var progressDlg = new BackupProgressForm(bkFolder, start, end))
                            progressDlg.ShowDialog(this);
                    }
                }

                var valueIds = entries.Select(e => e.ValueID).ToList();

                // 把耗时的 DB 查询 + 数据转换移到后台线程
                DataTable wide = await Task.Run(() =>
                {
                    var raw = DbManager.FetchData(valueIds, start, end, timeStep);
                    if (raw == null || raw.Rows.Count == 0) return null;

                    foreach (DataRow row in raw.Rows)
                        if (raw.Columns.Contains("Timestamp") && row["Timestamp"] != DBNull.Value)
                            row["Timestamp"] = ((DateTime)row["Timestamp"]).ToLocalTime();

                    var w = PivotToWide(raw, entries);
                    CleanUnusedColumns(w, new[] { "Quality", "Flags", "ValueID" });
                    return w;
                });

                if (wide == null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    MessageBox.Show(LangHelper.Get("Msg_NoData"), LangHelper.Get("Msg_Info"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ── 安全检查 2：数据行数上限 ────────────────────────────────
                int maxPts = cfg?.MaxDataPoints ?? 50000;
                bool truncated = false;
                if (maxPts > 0 && wide.Rows.Count > maxPts)
                {
                    var trimmed = wide.Clone();
                    for (int i = 0; i < maxPts; i++) trimmed.ImportRow(wide.Rows[i]);
                    wide = trimmed;
                    truncated = true;
                }

                _tagMetaList = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();
                BindResultTable(wide);

                if (truncated)
                    MessageBox.Show(
                        string.Format(LangHelper.Get("Msg_DataTruncated"), maxPts),
                        LangHelper.Get("Msg_Warning"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}", "错误");
            }
            finally
            {
                SetQueryRunning(false);
            }
        }

        private async Task RunQueryByCountAsync(List<TemplateTagEntry> entries, DateTime startUTC, int count)
        {
            SetQueryRunning(true);
            try
            {
                var cfg = ConfigManager.Config;

                // 安全检查：数据点数上限
                int maxPts = cfg?.MaxDataPoints ?? 50000;
                if (maxPts > 0 && count > maxPts)
                {
                    var ans = MessageBox.Show(
                        string.Format(LangHelper.Get("Msg_CountExceedWarning"), count, maxPts),
                        LangHelper.Get("Msg_Warning"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (ans != DialogResult.Yes) return;
                    count = maxPts;
                }

                // 自动连接备份（FetchByCount 只有 start 时间，按1年窗口匹配
                if (cfg?.AutoConnectBackup == true)
                {
                    string   bkFolder = cfg.BackupPath ?? "";
                    DateTime bkEnd    = startUTC.AddDays(365);
                    if (BackupManager.NeedsConnection(bkFolder, startUTC, bkEnd))
                    {
                        using (var progressDlg = new BackupProgressForm(bkFolder, startUTC, bkEnd))
                            progressDlg.ShowDialog(this);
                    }
                }

                var valueIds = entries.Select(e => e.ValueID).ToList();

                DataTable wide = await Task.Run(() =>
                {
                    var raw = DbManager.FetchDataByCount(valueIds, startUTC, count);
                    if (raw == null || raw.Rows.Count == 0) return null;

                    foreach (DataRow row in raw.Rows)
                        if (raw.Columns.Contains("Timestamp") && row["Timestamp"] != DBNull.Value)
                            row["Timestamp"] = ((DateTime)row["Timestamp"]).ToLocalTime();

                    var w = PivotToWide(raw, entries);
                    CleanUnusedColumns(w, new[] { "Quality", "Flags", "ValueID" });
                    return w;
                });

                if (wide == null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    MessageBox.Show(LangHelper.Get("Msg_NoData"), LangHelper.Get("Msg_Info"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _tagMetaList = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();
                BindResultTable(wide);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}", "错误");
            }
            finally
            {
                SetQueryRunning(false);
            }
        }

        #endregion

        #region Panel Control Functions
        /// <summary>
        /// 时间选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                panel.Applied += async (start, end, mode, timeStep) =>
                {
                    var startUTC = start.ToUniversalTime();
                    var endUTC = end.ToUniversalTime();

                    switch (mode)
                    {
                        case 2: // 测量点数量：从 start 取前 N 条原始点
                            await RunQueryByCountAsync(sortedEntries, startUTC, panel.SelectedCount);
                            break;
                        default: // 0-时间范围 / 1-开始到结束 / 3-自定义聚类
                            await RunQueryAsync(sortedEntries, startUTC, endUTC, timeStep);
                            break;
                    }
                };
                panel.ShowDialog();
            }
        }

        // ── 快捷日期查询（今日 / 前一天 / 后一天）────────────────────────────

        private void BtnTodayQuery_Click(object sender, EventArgs e)
        {
            _quickQueryDate = DateTime.Today;
            _ = ExecuteDayQueryAsync(_quickQueryDate);
        }

        private void BtnPrevDay_Click(object sender, EventArgs e)
        {
            _quickQueryDate = _quickQueryDate.AddDays(-1);
            _ = ExecuteDayQueryAsync(_quickQueryDate);
        }

        private void BtnNextDay_Click(object sender, EventArgs e)
        {
            _quickQueryDate = _quickQueryDate.AddDays(1);
            _ = ExecuteDayQueryAsync(_quickQueryDate);
        }

        /// <summary>
        /// 查询指定日期的整点记录（步长 1 小时，00:00 ~ 23:59）。
        /// </summary>
        private async Task ExecuteDayQueryAsync(DateTime date)
        {
            if (_currentTemplate == null)
            {
                MessageBox.Show("请先选择报表模板", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // 当天 00:00:00 ~ 23:59:59（本地时间），整点步长 3600 秒
            var startLocal = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Local);
            var endLocal   = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, DateTimeKind.Local);
            await RunQueryAsync(sortedEntries, startLocal.ToUniversalTime(), endLocal.ToUniversalTime(), 3600);
        }

        /// <summary>
        /// 批量导入Excel报表模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportFromExcel_Click(object sender, EventArgs e)
        {

            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Excel 文件|*.xlsx";
                dlg.Title = "选择变量别名表";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                int dataStartRow, tagNameCol, displayNameCol, displayNameEnCol;

                using (var cfg = new ExcelImportConfigForm())
                {
                    if (cfg.ShowDialog() != DialogResult.OK) return;
                    dataStartRow = cfg.DataStartRow; // 数据开始行
                    tagNameCol = cfg.TagNameCol;  // TagName 列
                    displayNameCol = cfg.DisplayNameCol; // 中文显示名称列
                    displayNameEnCol = cfg.DisplayNameEnCol; // 英文显示名称列（0 = 不使用）
                }

                try
                {
                    int updatedTags = 0;
                    int importedTemplates = 0;

                    using (var package = new ExcelPackage(new FileInfo(dlg.FileName)))
                    {
                        // 一个excel文件，读取多个worksheet
                        foreach (var sheet in package.Workbook.Worksheets)
                        {
                            if (sheet.Dimension == null) continue;
                            int rowCount = sheet.Dimension.End.Row;
                            var selectedTags = new List<TemplateTagEntry>();

                            // 从第 dataStartRow 行开始读取，直到最后一行。
                            // 每行根据 tagNameCol / displayNameCol / displayNameEnCol 列号读取 TagName / DisplayName / DisplayNameEn
                            for (int row = dataStartRow; row <= rowCount; row++)
                            {
                                string tagName = sheet.Cells[row, tagNameCol].Text?.Trim();
                                string displayName = sheet.Cells[row, displayNameCol].Text?.Trim();
                                string displayNameEn = displayNameEnCol > 0
                                    ? sheet.Cells[row, displayNameEnCol].Text?.Trim()
                                    : null;
                                if (string.IsNullOrEmpty(tagName)) continue;

                                // excel内的tagName和归档变量匹配
                                // （支持带路径的 TagName，如 "设备1\变量A"，也支持不带路径的短 TagName，如 "变量A"）
                                var meta = ConfigManager.Config.TagMeta
                                    .FirstOrDefault(m =>
                                    {
                                        string shortName = m.TagName.Contains("\\")
                                            ? m.TagName.Split('\\')[1] : m.TagName;
                                        return shortName == tagName;
                                    });

                                // 更新 TagMeta DisplayName / DisplayNameEn（中英显示名）if have
                                if (meta != null)
                                {
                                    if (!string.IsNullOrEmpty(displayName))
                                    {
                                        meta.DisplayName = displayName;
                                        updatedTags++;
                                    }
                                    if (!string.IsNullOrEmpty(displayNameEn))
                                        meta.DisplayNameEn = displayNameEn;
                                }


                                if (meta == null || meta.ValueID == 0) continue;

                                // 所选变量加入template tag entry
                                selectedTags.Add(new TemplateTagEntry
                                {
                                    ValueID = meta.ValueID,
                                    Order = selectedTags.Count + 1
                                });
                            }

                            if (selectedTags.Count == 0) continue;
                            if (ConfigManager.Config.Template == null)
                                ConfigManager.Config.Template = new List<ReportTemplate>();

                            // 写入 Template：同名则更新，否则新增
                            var existing = ConfigManager.Config.Template
                                .FirstOrDefault(t => t.DisplayName == sheet.Name);
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

        /// <summary>
        /// 配置按钮点击事件 — 打开综合配置面板（模板 / 通用设置 / 导出设置）
        /// </summary>
        private void BtnConfig_Click(object sender, EventArgs e)
        {
            using (var panel = new GeneralConfigPanel(
                ConfigManager.Config.Template,
                ConfigManager.Config.TagMeta,
                kryptonManager,
                mode => ApplyIcons(mode),                          // 主题切换时实时刷新工具栏图标
                () => ApplyLanguage()))                            // 语言切换时实时刷新主界面
            {
                panel.ShowDialog();
            }

            // 关闭配置窗口后，先设置显示成员再绑定数据源，避免 ComboBox 渲染异常
            var selectedId = _currentTemplate?.Id;
            cmbTemplate.DataSource    = null;
            cmbTemplate.DisplayMember = "DisplayName";
            cmbTemplate.ValueMember   = "Id";
            cmbTemplate.DataSource    = ConfigManager.Config?.Template;

            // 恢复之前选中的模板
            if (selectedId != null)
                cmbTemplate.SelectedValue = selectedId;

            // 重新应用可能在配置面板中更改的主题/语言，并同步刷新图标颜色
            LoadSavedTheme();
            ApplyLanguage();
            ApplyIcons(kryptonManager.GlobalPaletteMode);
            RefreshBackupButtons();
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PrintBtn_Click(object sender, EventArgs e)
        {
            _printData = dataGridView1.DataSource as DataTable;
            if (_printData == null || _printData.Rows.Count == 0)
            {
                MessageBox.Show("请先查询数据", "提示");
                return;
            }

            _printTitle = _currentTemplate?.DisplayName ?? "数据报表";
            _printSubtitle = ConfigManager.Config?.CompanyName ?? "";
            _printTime = DateTime.Now;
            _currentPage = 0;
            _totalPages = 0;

            var doc = new PrintDocument();

            using (var setup = new PageSetupDialog
            {
                PageSettings = doc.DefaultPageSettings,
                AllowOrientation = true,
                AllowMargins = true
            })
            {
                if (setup.ShowDialog() != DialogResult.OK) return;
                doc.DefaultPageSettings = setup.PageSettings;
            }

            doc.BeginPrint += (s, ev) =>
            {
                _printCurrentRow = 0;
                _printCurrentGroup = 0;
                _printColGroups = null;
                _printLayoutReady = false;
                _currentPage = 0;
            };
            doc.PrintPage += PrintDoc_PrintPage;

            using (var preview = new PrintPreviewDialog
            {
                Document = doc,
                WindowState = FormWindowState.Maximized
            })
            {
                // 挂 Load 事件：PrintPreviewDialog 在 Load 后才完成工具栏布局，
                // 必须在此之后修改，否则会被内部初始化代码覆盖
                preview.Load += (s, args) =>
                {
                    var toolStrip = preview.Controls.OfType<System.Windows.Forms.ToolStrip>().FirstOrDefault();
                    if (toolStrip == null) return;

                    toolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
                    toolStrip.AutoSize         = false;
                    toolStrip.Height           = 72;
                    toolStrip.Padding          = new System.Windows.Forms.Padding(4, 6, 4, 6);

                    var bigFont = new System.Drawing.Font("Segoe UI", 13F);
                    foreach (System.Windows.Forms.ToolStripItem item in toolStrip.Items)
                    {
                        item.Font         = bigFont;
                        item.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.SizeToFit;
                    }
                };

                preview.ShowDialog();
            }
        }

        /// <summary>
        /// 导出excel按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                // 优先用当前模板的导出路径，回退到全局配置路径
                string exportPath = _currentTemplate?.ExportPath;
                if (!string.IsNullOrWhiteSpace(exportPath)
                    && System.IO.Directory.Exists(exportPath))
                    dlg.InitialDirectory = exportPath;

                dlg.FileName = $"{reportTitle}_{DateTime.Now:yyyyMMdd_HHmmss}";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                ExportToExcel(dlg.FileName);
                MessageBox.Show("导出成功", "提示");
            }
        }

        /// <summary>
        /// 根据 AutoConnectBackup 配置决定连接/断开备份按钮的可见性。
        /// </summary>
        private void RefreshBackupButtons()
        {
            bool autoConnect = ConfigManager.Config?.AutoConnectBackup ?? false;
            btnConnectBackup.Visible    = !autoConnect;
            btnDisconnectBackup.Visible = !autoConnect;
        }

        /// <summary>连接备份按钮点击事件。</summary>
        private void BtnConnectBackup_Click(object sender, EventArgs e)
        {
            // read the backup path from the configuration
            string backupFolder = ConfigManager.Config?.BackupPath ?? "";
            using (var dlg = new BackupConnectForm(backupFolder))
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                using (var progressDlg = new BackupProgressForm(dlg.SelectedFolder, dlg.SelectedFiles))
                    progressDlg.ShowDialog(this);
            }
        }

        /// <summary>断开备份按钮点击事件。</summary>
        private void BtnDisconnectBackup_Click(object sender, EventArgs e)
        {
            if (BackupManager.ConnectedArchives.Count == 0)
            {
                MessageBox.Show(LangHelper.Get("Backup_NoBackupsConnected"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var dlg = new BackupDisconnectForm())
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                using (var progressDlg = new BackupDisconnectProgressForm(dlg.SelectedEntries))
                    progressDlg.ShowDialog(this);
            }
        }

        /// <summary>
        /// 模板选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTemplate = cmbTemplate.SelectedItem as ReportTemplate;
        }
        
        #endregion

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
            // 开双缓冲（DoubleBuffered 是 protected，需反射启用），消除选行/列时的全表闪烁
            typeof(System.Windows.Forms.DataGridView).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance  |
                System.Reflection.BindingFlags.SetProperty,
                null, dataGridView1, new object[] { true });

            // 强制清除旧列
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = table;

            // 列头换行 + 高度自适应（同时显示 TagName 和 DisplayName）
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name == "Timestamp")
                {
                    col.HeaderText = LangHelper.Get("UC_ColTimestamp");
                    col.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    col.Width = 300;
                }
                else
                {
                    col.HeaderText = BuildTagHeader(FindMetaByColName(col.Name), col.Name);
                    col.Width = 200;
                }

                // 禁用点列头排序（排序会触发 DataTable 重排 → 全表重绘）
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
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

            var meta = FindMetaByColName(colName);

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

            // ValueID → 列名（根据当前语言选择 DisplayName 或 DisplayNameEn）
            var idToCol = sortedEntries.ToDictionary(e => e.ValueID, e =>
            {
                var meta = allMeta.FirstOrDefault(m => m.ValueID == e.ValueID);
                return GetDisplayName(meta) ?? e.ValueID.ToString();
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


        /// <summary>取 TagName 最后一段（去掉 / 或 \ 前的路径前缀）。</summary>
        private static string ShortTagName(TagMeta meta, string fallback = "")
        {
            string full = meta?.TagName;
            if (string.IsNullOrWhiteSpace(full)) return fallback;
            return full.Split('/', '\\').Last();
        }
        /// <summary>生成 DGV 列头文字：短 TagName 换行 DisplayName（当前语言）。
        /// 若两者相同则只显示一行。</summary>
        private string BuildTagHeader(TagMeta meta, string fallback)
        {
            if (meta == null) return fallback;
            string tag = ShortTagName(meta, fallback);
            string disp = GetDisplayName(meta) ?? fallback;
            return (string.IsNullOrWhiteSpace(disp) || disp == tag)
                   ? tag : tag + "\n" + disp;
        }

        /// <summary>根据当前语言返回变量的显示名称</summary>
        private string GetDisplayName(TagMeta meta)
        {
            if (meta == null) return null;
            bool useEn = ConfigManager.Config?.Language == "en-US";
            if (useEn && !string.IsNullOrWhiteSpace(meta.DisplayNameEn))
                return meta.DisplayNameEn;
            return !string.IsNullOrWhiteSpace(meta.DisplayName) ? meta.DisplayName : meta.TagName;
        }

        /// <summary>通过列名（中文/英文/TagName）查找对应 TagMeta</summary>
        private TagMeta FindMetaByColName(string colName)
        {
            return _tagMetaList.FirstOrDefault(t =>
                t.DisplayName == colName ||
                t.DisplayNameEn == colName ||
                t.TagName == colName);
        }

        #endregion

        #region 导出相关功能

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

                // 第 2 行：TagName；第 3 行：DisplayName；两行一起构成列标题
                int tagNameRow  = 2;
                int dispNameRow = 3;
                int dataStartRow = 4;

                for (int c = 0; c < colCount; c++)
                {
                    string colName = dt.Columns[c].ColumnName;
                    bool isTimestamp = colName == "Timestamp";
                    var meta = isTimestamp ? null : FindMetaByColName(colName);

                    // TagName 行
                    var tagCell = ws.Cells[tagNameRow, c + 1];
                    tagCell.Value = isTimestamp
                        ? LangHelper.Get("UC_ColTimestamp")
                        : ShortTagName(meta, colName);
                    tagCell.Style.Font.Bold = true;
                    tagCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    SetThinBorder(tagCell);

                    // DisplayName 行（Timestamp 列合并两行；变量列显示 DisplayName）
                    var dispCell = ws.Cells[dispNameRow, c + 1];
                    if (isTimestamp)
                    {
                        ws.Cells[tagNameRow, c + 1, dispNameRow, c + 1].Merge = true;
                    }
                    else
                    {
                        dispCell.Value = GetDisplayName(meta) ?? ShortTagName(meta, colName);
                        dispCell.Style.Font.Bold = true;
                        dispCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        SetThinBorder(dispCell);
                    }
                }

                // 数据行（从第 4 行开始）
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        var cell = ws.Cells[dataStartRow + r, c + 1];
                        var val = dt.Rows[r][c];
                        cell.Value = val == DBNull.Value ? null : val;
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        SetThinBorder(cell);

                        if (dt.Columns[c].ColumnName == "Timestamp")
                        {
                            cell.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                            continue;
                        }

                        var meta = FindMetaByColName(dt.Columns[c].ColumnName);

                        if (meta != null && val != DBNull.Value
                            && double.TryParse(val.ToString(), out double dval))
                        {
                            cell.Style.Numberformat.Format = "0." + new string('0', meta.DecimalPlaces);
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

        #region 打印功能

        private DataTable       _printData;
        private float[]         _printColWidths;
        private List<List<int>> _printColGroups;
        private int             _printCurrentGroup;
        private int             _printCurrentRow;
        private string          _printTitle;
        private string          _printSubtitle;   // 公司名 / 抬头
        private bool            _printLayoutReady;
        private int             _currentPage;
        private int             _totalPages;
        private DateTime        _printTime;



        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g      = e.Graphics;
            var bounds = e.MarginBounds;

            using (var titleFont    = new Font("Arial", 14f, FontStyle.Bold))
            using (var subtitleFont = new Font("Arial", 10f, FontStyle.Regular))
            using (var headerFont   = new Font("Arial",  9f, FontStyle.Bold))
            using (var dataFont     = new Font("Arial",  8f))
            using (var footerFont   = new Font("Arial",  8f, FontStyle.Italic))
            {
                if (!_printLayoutReady)
                {
                    BuildPrintLayout(g, bounds.Width, headerFont, dataFont);
                    _totalPages       = CalculateTotalPages(g, bounds, titleFont, subtitleFont, headerFont, dataFont, footerFont);
                    _printLayoutReady = true;
                }

                _currentPage++;

                var   colIndices = _printColGroups[_printCurrentGroup];
                float groupW     = colIndices.Sum(c => _printColWidths[c]);
                float scale      = groupW <= bounds.Width ? 1f : bounds.Width / groupW;
                var   scaledW    = colIndices.Select(c => _printColWidths[c] * scale).ToArray();

                float x0         = bounds.Left;
                float y          = bounds.Top;
                float titleH     = titleFont.GetHeight(g)    + 6f;
                float subH       = subtitleFont.GetHeight(g) + 4f;
                float hdrH       = headerFont.GetHeight(g)   + 6f;
                float rowH       = dataFont.GetHeight(g)     + 4f;
                float footH      = footerFont.GetHeight(g)   + 4f;
                float pageBottom = bounds.Bottom - footH - 2f;   // 留出页脚空间

                // 报表标题 + 抬头（每页都有）
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    g.DrawString(_printTitle, titleFont, Brushes.Black,
                        new RectangleF(x0, y, bounds.Width, titleH), sf);
                    y += titleH;

                    if (!string.IsNullOrWhiteSpace(_printSubtitle))
                    {
                        g.DrawString(_printSubtitle, subtitleFont, Brushes.DimGray,
                            new RectangleF(x0, y, bounds.Width, subH), sf);
                        y += subH;
                    }
                }
                y += 6f;

                // ── 列标题：第1行 TagName，第2行 DisplayName（灰底加粗居中）──
                float hx;
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    // 第1行 — TagName
                    hx = x0;
                    for (int i = 0; i < colIndices.Count; i++)
                    {
                        int    c       = colIndices[i];
                        string colName = _printData.Columns[c].ColumnName;
                        string hdr     = colName == "Timestamp"
                                         ? LangHelper.Get("UC_ColTimestamp")
                                         : ShortTagName(FindMetaByColName(colName), colName);
                        var rect = new RectangleF(hx, y, scaledW[i], hdrH);
                        g.FillRectangle(Brushes.LightGray, rect);
                        g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
                        g.DrawString(hdr, headerFont, Brushes.Black, rect, sf);
                        hx += scaledW[i];
                    }
                    y += hdrH;

                    // 第2行 — DisplayName（Timestamp 列留空）
                    hx = x0;
                    for (int i = 0; i < colIndices.Count; i++)
                    {
                        int    c       = colIndices[i];
                        string colName = _printData.Columns[c].ColumnName;
                        string disp    = colName == "Timestamp"
                                         ? ""
                                         : GetDisplayName(FindMetaByColName(colName))
                                           ?? ShortTagName(FindMetaByColName(colName), colName);
                        var rect = new RectangleF(hx, y, scaledW[i], hdrH);
                        g.FillRectangle(Brushes.LightGray, rect);
                        g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
                        g.DrawString(disp, headerFont, Brushes.Black, rect, sf);
                        hx += scaledW[i];
                    }
                    y += hdrH;
                }

                // ── 数据行（间隔底色 + 灰色边框） ──
                bool alt = false;
                while (_printCurrentRow < _printData.Rows.Count && y + rowH <= pageBottom)
                {
                    hx = x0;
                    for (int i = 0; i < colIndices.Count; i++)
                    {
                        int    c       = colIndices[i];
                        var    val     = _printData.Rows[_printCurrentRow][c];
                        string colName = _printData.Columns[c].ColumnName;
                        string txt     = val == DBNull.Value ? "" : val.ToString();

                        if (colName == "Timestamp" && val is DateTime dt)
                            txt = dt.ToString("yyyy-MM-dd HH:mm:ss");
                        else if (val != DBNull.Value && double.TryParse(txt, out double dval))
                            txt = dval.ToString("F" + (FindMetaByColName(colName)?.DecimalPlaces ?? 2));

                        var rect = new RectangleF(hx, y, scaledW[i], rowH);
                        if (alt) g.FillRectangle(Brushes.WhiteSmoke, rect);
                        g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
                        using (var sf = new StringFormat
                        {
                            Alignment   = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center,
                            FormatFlags = StringFormatFlags.NoWrap
                        })
                            g.DrawString(txt, dataFont, Brushes.Black, rect, sf);
                        hx += scaledW[i];
                    }
                    y += rowH;
                    _printCurrentRow++;
                    alt = !alt;
                }

                // ── 页脚：页码居中，导出时间右对齐 ──
                float footY = bounds.Bottom - footH;
                string pageStr  = $"{_currentPage} / {_totalPages}";
                string timeStr  = _printTime.ToString("yyyy-MM-dd HH:mm:ss");

                using (var sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                using (var sfRight  = new StringFormat { Alignment = StringAlignment.Far,    LineAlignment = StringAlignment.Center })
                {
                    var footRect = new RectangleF(x0, footY, bounds.Width, footH);
                    g.DrawString(pageStr, footerFont, Brushes.Gray, footRect, sfCenter);
                    g.DrawString(timeStr, footerFont, Brushes.Gray, footRect, sfRight);
                }

                // ── 分页判断 ──
                if (_printCurrentRow < _printData.Rows.Count)
                {
                    e.HasMorePages = true;
                }
                else if (_printCurrentGroup < _printColGroups.Count - 1)
                {
                    _printCurrentGroup++;
                    _printCurrentRow = 0;
                    e.HasMorePages   = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
        }

        /// <summary>预算总页数，用于显示"X / N"页码。</summary>
        private int CalculateTotalPages(Graphics g, RectangleF bounds,
            Font titleFont, Font subtitleFont, Font headerFont, Font dataFont, Font footerFont)
        {
            float titleH  = titleFont.GetHeight(g)    + 6f;
            float subH    = string.IsNullOrWhiteSpace(_printSubtitle) ? 0f : subtitleFont.GetHeight(g) + 4f;
            float hdrH    = headerFont.GetHeight(g)   + 6f;
            float rowH    = dataFont.GetHeight(g)     + 4f;
            float footH   = footerFont.GetHeight(g)   + 4f + 2f;
            float usable  = bounds.Height - footH;

            // 每页都有标题+抬头，可用行数一致
            float headerArea = titleH + subH + 6f + 2 * hdrH;
            int   rowsPerPage = Math.Max(1, (int)Math.Floor((usable - headerArea) / rowH));

            int total     = 0;
            int remaining = _printData.Rows.Count;

            foreach (var group in _printColGroups)
            {
                int r = remaining;
                if (r <= 0) { total++; continue; }
                while (r > 0) { r -= rowsPerPage; total++; }
            }

            return Math.Max(1, total);
        }

        private void BuildPrintLayout(Graphics g, float printableWidth, Font headerFont, Font dataFont)
        {
            int colCount    = _printData.Columns.Count;
            _printColWidths = new float[colCount];

            for (int c = 0; c < colCount; c++)
            {
                string colName = _printData.Columns[c].ColumnName;

                // 从列标题宽度起步：取 TagName 和 DisplayName 两行中较宽的那个
                var   hdrMeta = colName == "Timestamp" ? null : FindMetaByColName(colName);
                string hdr    = colName == "Timestamp"
                                ? LangHelper.Get("UC_ColTimestamp")
                                : ShortTagName(hdrMeta, colName);
                string disp   = colName == "Timestamp"
                                ? ""
                                : GetDisplayName(hdrMeta) ?? hdr;
                float w = Math.Max(
                    g.MeasureString(hdr,  headerFont).Width,
                    g.MeasureString(disp, headerFont).Width) + 16f;

                // 扫描所有数据行，取最宽的单元格文本（与打印时相同的格式逻辑）
                foreach (DataRow row in _printData.Rows)
                {
                    var    val = row[c];
                    if (val == null || val == DBNull.Value) continue;

                    string txt;
                    if (colName == "Timestamp" && val is DateTime dt)
                    {
                        txt = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else if (double.TryParse(val.ToString(), out double dval))
                    {
                        var meta = FindMetaByColName(colName);
                        txt = dval.ToString("F" + (meta?.DecimalPlaces ?? 2));
                    }
                    else
                    {
                        txt = val.ToString();
                    }

                    float dataW = g.MeasureString(txt, dataFont).Width + 8f;
                    if (dataW > w) w = dataW;
                }

                _printColWidths[c] = Math.Max(w, 60f);
            }

            // 找 Timestamp 列（每个列组都要含它）
            int tsColIdx = -1;
            for (int c = 0; c < colCount; c++)
                if (_printData.Columns[c].ColumnName == "Timestamp") { tsColIdx = c; break; }
            float tsW = tsColIdx >= 0 ? _printColWidths[tsColIdx] : 0f;

            // 按可打印宽度分组；第2组起自动预留 Timestamp 列宽
            _printColGroups = new List<List<int>>();
            var   group     = new List<int>();
            float gw        = 0f;
            bool  firstGroup = true;

            for (int c = 0; c < colCount; c++)
            {
                if (c == tsColIdx && !firstGroup) continue;  // 非首组中跳过 Timestamp（稍后插到头部）

                float reserved = firstGroup ? 0f : tsW;      // 非首组要为 Timestamp 预留宽度
                if (group.Count > 0 && gw + _printColWidths[c] > printableWidth - reserved)
                {
                    _printColGroups.Add(group);
                    group      = new List<int> { tsColIdx };  // 新组从 Timestamp 列开始
                    gw         = tsW;
                    firstGroup = false;
                }
                group.Add(c);
                gw += _printColWidths[c];
            }
            if (group.Count > 0) _printColGroups.Add(group);
        }

        #endregion

    }
}
