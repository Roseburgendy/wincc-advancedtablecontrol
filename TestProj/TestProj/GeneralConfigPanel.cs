using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Newtonsoft.Json;
using WINCCReporter.Models;

namespace WINCCReporter
{
    public partial class GeneralConfigPanel : Krypton.Toolkit.KryptonForm
    {
        private readonly BindingList<ReportTemplate> _bindingList;
        private readonly List<TagMeta> _allTags;
        private readonly Dictionary<long, TagMeta> _tagDict;   // fast ValueID → TagMeta lookup
        private ReportTemplate _editing;

        private readonly Krypton.Toolkit.KryptonManager _kryptonManager;
        /// <summary>主题切换时通知调用方（如 UserControl1）实时刷新图标。</summary>
        private readonly Action<Krypton.Toolkit.PaletteMode> _onThemeChanged;
        /// <summary>语言切换时通知调用方（如 UserControl1）实时刷新所有 UI 文字。</summary>
        private readonly Action _onLanguageChanged;
        public GeneralConfigPanel(
            List<ReportTemplate> templates,
            List<TagMeta> allTags,
            Krypton.Toolkit.KryptonManager kryptonManager,
            Action<Krypton.Toolkit.PaletteMode> onThemeChanged = null,
            Action onLanguageChanged = null)
        {
            InitializeComponent();

            _kryptonManager   = kryptonManager;
            _onThemeChanged   = onThemeChanged;
            _onLanguageChanged = onLanguageChanged;

            // --- Tab 1: Template Config ---
            _allTags  = allTags  ?? new List<TagMeta>();
            _tagDict  = _allTags.GroupBy(m => m.ValueID)
                                .ToDictionary(g => g.Key, g => g.First());
            templates = templates ?? new List<ReportTemplate>();

            pnlDetail.Visible = false;
            _bindingList = new BindingList<ReportTemplate>(templates);
            lstTemplates.DisplayMember = "DisplayName";
            lstTemplates.DataSource    = _bindingList;

            // Defer initial detail load to Shown event so the Krypton navigator
            // has finished its own layout pass before we make the panel visible.
            Shown += (s, e) =>
            {
                if (_bindingList.Count > 0)
                {
                    lstTemplates.SelectedIndex = 0;
                    _editing = CloneTemplate(_bindingList[0]);
                    BindDetail();
                }
            };

            // --- Tab 2: General Settings ---
            cmbLanguage.Items.AddRange(new object[] { "中文 (zh-CN)", "English (en-US)" });
            LoadGeneralSettings();

            // Config path: auto-determined — show only, open folder on button click
            kryptonTextBox1.ReadOnly = true;

            ApplyLanguage();
            ApplyIcons(_kryptonManager.GlobalPaletteMode);
        }

        /// <summary>根据当前主题重新生成并赋值所有按钮的图标。</summary>
        public void ApplyIcons(Krypton.Toolkit.PaletteMode mode)
        {
            // Tab 1 — Template Config
            btnAdd.Values.Image    = IconManager.Make(IconChar.Plus,       mode, 32);
            btnSave.Values.Image   = IconManager.Make(IconChar.FloppyDisk, mode, 32);
            btnCancel.Values.Image = IconManager.Make(IconChar.Xmark,      mode, 32);
            btnClone.Values.Image  = IconManager.Make(IconChar.Copy,       mode, 32);
            btnDelete.Values.Image = IconManager.Make(IconChar.Trash,      mode, 32);
            btnSelectTags.Values.Image       = IconManager.Make(IconChar.Tags,       mode, 32);
            btnConfigTag.Values.Image        = IconManager.Make(IconChar.Wrench,     mode, 32);

            // Tab 2 — General Settings
            btnSaveGeneral.Values.Image      = IconManager.Make(IconChar.FloppyDisk, mode, 32);
            btnBatchDecimal.Values.Image     = IconManager.Make(IconChar.Bolt,       mode, 32);
            btnBrowseBackupPath.Values.Image = IconManager.Make(IconChar.FolderOpen, mode, 22);
            btnChangePassword.Values.Image   = IconManager.Make(IconChar.Key,        mode, 32);
        }

        /// <summary>刷新整个窗体的 UI 文字为当前语言。</summary>
        public void ApplyLanguage()
        {
            this.Text = LangHelper.Get("GCP_Title");

            // Navigator tab titles
            kryptonPage1.Text = LangHelper.Get("GCP_Tab_Templates");
            kryptonPage2.Text = LangHelper.Get("GCP_Tab_General");
            // Tab 1 — Template Config
            lblTemplatesHeader.Values.Text = LangHelper.Get("GCP_TemplatesHeader");
            btnAdd.Values.Text             = LangHelper.Get("GCP_BtnAdd");
            btnSave.Values.Text            = LangHelper.Get("GCP_BtnSave");
            btnCancel.Values.Text          = LangHelper.Get("GCP_BtnCancel");
            btnClone.Values.Text           = LangHelper.Get("GCP_BtnClone");
            btnDelete.Values.Text          = LangHelper.Get("GCP_BtnDelete");
            label2.Values.Text             = LangHelper.Get("GCP_LblName");
            labelDesc.Values.Text          = LangHelper.Get("GCP_LblDesc");
            labelTags.Values.Text          = LangHelper.Get("GCP_LblVars");
            btnSelectTags.Values.Text      = LangHelper.Get("GCP_BtnSelectVars");
            btnConfigTag.Values.Text       = LangHelper.Get("GCP_BtnConfigVars");
            lblTemplateExportPath.Values.Text      = LangHelper.Get("GCP_LblTemplateExportPath");

            // Tab 2 — General Settings: GroupBox headings
            kryptonGroupBox1.Values.Heading = LangHelper.Get("GCP_GroupAppearance");
            kryptonGroupBox2.Values.Heading = LangHelper.Get("GCP_GroupSystem");

            lblLanguage.Values.Text       = LangHelper.Get("GCP_LblLanguage");
            lblTheme.Values.Text          = LangHelper.Get("GCP_LblTheme");
            btnSaveGeneral.Values.Text    = LangHelper.Get("GCP_BtnSave");
            lblCompanyName.Values.Text    = LangHelper.Get("GCP_LblCompanyName");
            lblConfigPath.Values.Text     = LangHelper.Get("GCP_LblConfigPath");
            btnBatchDecimal.Values.Text   = LangHelper.Get("TCP_BtnBatchDecimal");
            lblMaxQueryDays.Values.Text        = LangHelper.Get("GCP_LblMaxQueryDays");
            lblMaxDataPoints.Values.Text       = LangHelper.Get("GCP_LblMaxDataPoints");
            lblQueryLimitsHint.Values.Text     = LangHelper.Get("GCP_HintQueryLimits");
            chkAutoConnectBackup.Values.Text   = LangHelper.Get("GCP_ChkAutoConnectBackup");
            lblBackupPath.Values.Text          = LangHelper.Get("GCP_LblBackupPath");
            btnChangePassword.Values.Text      = LangHelper.Get("GCP_BtnChangePassword");
            // Refresh tag summary text (it may contain a localised separator)
            UpdateTagSummary();
        }

        #region Template Config Panel Logic

        private void LstTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedItem is ReportTemplate t)
            {
                _editing = CloneTemplate(t);
                BindDetail();
            }
        }

        /// <summary>
        /// Fast manual deep-copy of a ReportTemplate — avoids JSON serialization overhead
        /// on every selection change.
        /// </summary>
        private static ReportTemplate CloneTemplate(ReportTemplate src) =>
            new ReportTemplate
            {
                Id          = src.Id,
                DisplayName = src.DisplayName,
                Description = src.Description,
                ExportPath  = src.ExportPath ?? "",
                SelectedTags = src.SelectedTags
                    .Select(e => new TemplateTagEntry { ValueID = e.ValueID, Order = e.Order })
                    .ToList()
            };

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _editing = new ReportTemplate
            {
                Id           = "tpl_" + DateTime.Now.Ticks,
                DisplayName  = "新建模板",
                Description  = "",
                SelectedTags = new List<TemplateTagEntry>()
            };
            lstTemplates.SelectedIndex = -1;
            BindDetail();
            txtName.Focus();
            txtName.SelectAll();
        }

        private void BtnSelectTags_Click(object sender, EventArgs e)
        {
            var previousSelected = _editing.SelectedTags
                .Select(entry => entry.ValueID)
                .ToList();

            using (var panel = new TagSelectionPanel(previousSelected))
            {
                if (panel.ShowDialog() == DialogResult.OK)
                {
                    var existingDict = _editing.SelectedTags
                        .ToDictionary(entry => entry.ValueID, entry => entry.Order);

                    int maxOrder = existingDict.Count > 0 ? existingDict.Values.Max() : 0;
                    int newIdx   = 0;

                    _editing.SelectedTags = panel.SelectedTags.Select(entry =>
                        new TemplateTagEntry
                        {
                            ValueID = entry.ValueID,
                            Order   = existingDict.TryGetValue(entry.ValueID, out int o)
                                        ? o
                                        : maxOrder + (++newIdx)
                        }).ToList();

                    UpdateTagSummary();
                }
            }
        }




        private void BtnConfigTag_Click(object sender, EventArgs e)
        {
            if (_editing.SelectedTags.Count == 0)
            {
                MessageBox.Show(LangHelper.Get("GCP_NoVarsSelected"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var panel = new TagConfigPanel(_editing.SelectedTags))
            {
                panel.FormClosing += (s, args) =>
                {
                    if (panel.HasUnsavedChanges())
                    {
                        var result = MessageBox.Show(
                            LangHelper.Get("GCP_UnsavedChanges"),
                            LangHelper.Get("Msg_Info"),
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)        panel.SaveAll();
                        else if (result == DialogResult.Cancel) args.Cancel = true;
                        else if (result == DialogResult.No)     panel.DiscardAllChanges();
                    }
                };
                panel.ShowDialog();
                UpdateTagSummary();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(LangHelper.Get("GCP_TemplateNameEmpty"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            _editing.DisplayName = txtName.Text.Trim();
            _editing.Description = txtDesc.Text.Trim();
            _editing.ExportPath  = kryptonTextBox3.Text.Trim();

            var existing = _bindingList.FirstOrDefault(t => t.Id == _editing.Id);
            if (existing != null)
            {
                int idx = _bindingList.IndexOf(existing);
                _bindingList[idx] = _editing;
                lstTemplates.SelectedIndex = idx;
            }
            else
            {
                _bindingList.Add(_editing);
                lstTemplates.SelectedIndex = _bindingList.Count - 1;
            }

            ConfigManager.Config.Template = _bindingList.ToList();
            ConfigManager.SaveConfig();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedItem is ReportTemplate t)
            {
                _editing = JsonConvert.DeserializeObject<ReportTemplate>(
                           JsonConvert.SerializeObject(t));
                BindDetail();
            }
            else
            {
                pnlDetail.Visible = false;
                _editing = null;
            }
        }

        private void BtnClone_Click(object sender, EventArgs e)
        {
            if (_editing == null) return;
            _editing = new ReportTemplate
            {
                Id          = "tpl_" + DateTime.Now.Ticks,
                DisplayName = _editing.DisplayName + "_副本",
                Description = _editing.Description,
                SelectedTags = _editing.SelectedTags.Select(entry => new TemplateTagEntry
                {
                    ValueID = entry.ValueID,
                    Order   = entry.Order
                }).ToList()
            };
            lstTemplates.SelectedIndex = -1;
            BindDetail();
            txtName.Focus();
            txtName.SelectAll();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var t = lstTemplates.SelectedItem as ReportTemplate;
            if (t == null) return;

            var confirm = MessageBox.Show(
                string.Format(LangHelper.Get("GCP_ConfirmDelete"), t.DisplayName),
                LangHelper.Get("GCP_DeleteTitle"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            _bindingList.Remove(t);
            _editing = null;
            pnlDetail.Visible = _bindingList.Count > 0;

            if (_bindingList.Count > 0)
                lstTemplates.SelectedIndex = 0;

            ConfigManager.Config.Template = _bindingList.ToList();
            ConfigManager.SaveConfig();
        }

        private void BindDetail()
        {
            txtName.Text         = _editing.DisplayName;
            txtDesc.Text         = _editing.Description;
            kryptonTextBox3.Text = _editing.ExportPath ?? "";
            UpdateTagSummary();
            pnlDetail.Visible = true;
        }

        private void UpdateTagSummary()
        {
            if (_editing == null || _editing.SelectedTags.Count == 0)
            {
                txtTagSummary.Text = LangHelper.Get("GCP_NoVars");
                return;
            }

            var names = _editing.SelectedTags
                .OrderBy(entry => entry.Order)
                .Select(entry =>
                {
                    _tagDict.TryGetValue(entry.ValueID, out var meta);
                    return meta?.DisplayName ?? meta?.TagName ?? entry.ValueID.ToString();
                })
                .ToList();

            string sep = LangHelper.Get("GCP_NameSep");
            txtTagSummary.Text = string.Format(
                LangHelper.Get("GCP_VarsSelected"),
                names.Count,
                string.Join(sep, names));
        }

        #endregion

        #region General Settings Panel Logic
        private void LoadGeneralSettings()
        {
            var cfg = ConfigManager.Config;

            kryptonTextBox2.Text = cfg?.CompanyName ?? "";
            kryptonTextBox1.Text = ConfigManager.ConfigPath ?? "";

            string lang = cfg?.Language ?? "zh-CN";
            cmbLanguage.SelectedIndex = lang == "en-US" ? 1 : 0;

            string theme = cfg?.Theme ?? "Microsoft365Blue";
            if (Enum.TryParse<Krypton.Toolkit.PaletteMode>(theme, out var mode))
                cmbTheme.DefaultPalette = mode;

            numMaxQueryDays.Value  = Math.Max(0, Math.Min((decimal)(cfg?.MaxQueryDays  ?? 31),  numMaxQueryDays.Maximum));
            numMaxDataPoints.Value = Math.Max(0, Math.Min((decimal)(cfg?.MaxDataPoints ?? 50000), numMaxDataPoints.Maximum));

            chkAutoConnectBackup.Checked = cfg?.AutoConnectBackup ?? false;
            txtBackupPath.Text           = cfg?.BackupPath ?? "";
        }

        private void CmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConfigManager.Config != null)
                ConfigManager.Config.Language = cmbLanguage.SelectedIndex == 1 ? "en-US" : "zh-CN";
            ApplyLanguage();                  // 刷新本窗口
            _onLanguageChanged?.Invoke();     // 通知调用方（UserControl1 等）同步刷新
        }

        private void CmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Apply immediately so the user gets live feedback while the dialog is open
            var mode = cmbTheme.DefaultPalette;
            if (_kryptonManager != null)
                _kryptonManager.GlobalPaletteMode = mode;
            ApplyIcons(mode);           // 刷新本窗体图标
            _onThemeChanged?.Invoke(mode); // 实时通知 UserControl1 刷新工具栏图标
        }

        private void BtnBrowseExportPath_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = LangHelper.Get("GCP_BrowseExportPath");
                string current = kryptonTextBox3.Text.Trim();
                dlg.SelectedPath = Directory.Exists(current) ? current : Application.StartupPath;

                if (dlg.ShowDialog() == DialogResult.OK)
                    kryptonTextBox3.Text = dlg.SelectedPath;
            }
        }


        private void BtnBrowseBackupPath_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = LangHelper.Get("Backup_FolderBrowseDesc");
                string current = txtBackupPath.Text.Trim();
                dlg.SelectedPath = Directory.Exists(current) ? current : Application.StartupPath;

                if (dlg.ShowDialog() == DialogResult.OK)
                    txtBackupPath.Text = dlg.SelectedPath;
            }
        }

        private void BtnSaveGeneral_Click(object sender, EventArgs e)
        {
            if (ConfigManager.Config == null) return;
            ConfigManager.Config.CompanyName    = kryptonTextBox2.Text.Trim();
            ConfigManager.Config.Language       = cmbLanguage.SelectedIndex == 1 ? "en-US" : "zh-CN";
            ConfigManager.Config.Theme          = cmbTheme.DefaultPalette.ToString();
            ConfigManager.Config.MaxQueryDays      = (int)numMaxQueryDays.Value;
            ConfigManager.Config.MaxDataPoints     = (int)numMaxDataPoints.Value;
            ConfigManager.Config.AutoConnectBackup = chkAutoConnectBackup.Checked;
            ConfigManager.Config.BackupPath        = txtBackupPath.Text.Trim();
            ConfigManager.SaveConfig();

            ApplyLanguage();   // 保存后刷新本窗体文字（语言可能已变）
            MessageBox.Show(LangHelper.Get("GCP_GeneralSaved"), LangHelper.Get("Msg_Info"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBatchDecimal_Click(object sender, EventArgs e)
        {
            using (var form = new BatchDecimalForm())
                form.ShowDialog(this);
            // No DGV to refresh here — changes are persisted in config immediately
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            using (var dlg = new ChangePasswordForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string msg = AuthManager.HasPassword
                        ? LangHelper.Get("Auth_PasswordChanged")
                        : LangHelper.Get("Auth_PasswordCleared");
                    MessageBox.Show(this, msg, LangHelper.Get("Msg_Info"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        private void kryptonNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
