using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Newtonsoft.Json;
using TestProj.Models;

namespace TestProj
{
    public partial class GeneralConfigPanel : Krypton.Toolkit.KryptonForm
    {
        // =====================================================================
        // Tab 1 — Template Config
        // =====================================================================

        private readonly BindingList<ReportTemplate> _bindingList;
        private readonly List<TagMeta> _allTags;
        private ReportTemplate _editing;

        // =====================================================================
        // Dependencies
        // =====================================================================

        private readonly Krypton.Toolkit.KryptonManager _kryptonManager;
        /// <summary>主题切换时通知调用方（如 UserControl1）实时刷新图标。</summary>
        private readonly Action<Krypton.Toolkit.PaletteMode> _onThemeChanged;

        public GeneralConfigPanel(
            List<ReportTemplate> templates,
            List<TagMeta> allTags,
            Krypton.Toolkit.KryptonManager kryptonManager,
            Action<Krypton.Toolkit.PaletteMode> onThemeChanged = null)
        {
            InitializeComponent();
            this.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)134);

            _kryptonManager  = kryptonManager;
            _onThemeChanged  = onThemeChanged;

            // --- Tab 1: Template Config ---
            _allTags  = allTags  ?? new List<TagMeta>();
            templates = templates ?? new List<ReportTemplate>();

            _bindingList = new BindingList<ReportTemplate>(templates);
            lstTemplates.DisplayMember = "DisplayName";
            lstTemplates.DataSource    = _bindingList;
            pnlDetail.Visible = false;

            // --- Tab 2: General Settings ---
            cmbLanguage.Items.AddRange(new object[] { "中文 (zh-CN)", "English (en-US)" });
            LoadGeneralSettings();

            // Wire combos AFTER loading settings so init doesn't fire a save
            cmbLanguage.SelectedIndexChanged += CmbLanguage_SelectedIndexChanged;
            cmbTheme.SelectedIndexChanged    += CmbTheme_SelectedIndexChanged;

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
            btnSelectTags.Values.Image = IconManager.Make(IconChar.Tags,   mode, 32);
            btnConfigTag.Values.Image  = IconManager.Make(IconChar.Wrench, mode, 32);

            // Tab 2 — General Settings
            btnSaveGeneral.Values.Image = IconManager.Make(IconChar.FloppyDisk, mode, 32);

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

            // Tab 2 — General Settings
            lblLanguage.Values.Text    = LangHelper.Get("GCP_LblLanguage");
            lblTheme.Values.Text       = LangHelper.Get("GCP_LblTheme");
            btnSaveGeneral.Values.Text = LangHelper.Get("GCP_BtnSave");

            // Refresh tag summary text (it may contain a localised separator)
            UpdateTagSummary();
        }

        // =====================================================================
        // Tab 1 — Template Config logic (mirrors TemplateConfigPanel.cs)
        // =====================================================================

        private void LstTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedItem is ReportTemplate t)
            {
                _editing = JsonConvert.DeserializeObject<ReportTemplate>(
                           JsonConvert.SerializeObject(t));
                BindDetail();
            }
        }

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
            txtName.Text = _editing.DisplayName;
            txtDesc.Text = _editing.Description;
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
                    var meta = _allTags.FirstOrDefault(m => m.ValueID == entry.ValueID);
                    return meta?.DisplayName ?? meta?.TagName ?? entry.ValueID.ToString();
                })
                .ToList();

            string sep = LangHelper.Get("GCP_NameSep");
            txtTagSummary.Text = string.Format(
                LangHelper.Get("GCP_VarsSelected"),
                names.Count,
                string.Join(sep, names));
        }

        // =====================================================================
        // Tab 2 — General Settings
        // =====================================================================

        private void LoadGeneralSettings()
        {
            var cfg = ConfigManager.Config;

            string lang = cfg?.Language ?? "zh-CN";
            cmbLanguage.SelectedIndex = lang == "en-US" ? 1 : 0;

            string theme = cfg?.Theme ?? "Microsoft365Blue";
            if (Enum.TryParse<Krypton.Toolkit.PaletteMode>(theme, out var mode))
                cmbTheme.DefaultPalette = mode;
        }

        private void CmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 实时预览：切语言下拉时立刻刷新窗体文字
            if (ConfigManager.Config != null)
                ConfigManager.Config.Language = cmbLanguage.SelectedIndex == 1 ? "en-US" : "zh-CN";
            ApplyLanguage();
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

        private void BtnSaveGeneral_Click(object sender, EventArgs e)
        {
            if (ConfigManager.Config == null) return;
            ConfigManager.Config.Language    = cmbLanguage.SelectedIndex == 1 ? "en-US" : "zh-CN";
            ConfigManager.Config.Theme       = cmbTheme.DefaultPalette.ToString();
            ConfigManager.SaveConfig();

            ApplyLanguage();   // 保存后刷新本窗体文字（语言可能已变）
            MessageBox.Show(LangHelper.Get("GCP_GeneralSaved"), LangHelper.Get("Msg_Info"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
