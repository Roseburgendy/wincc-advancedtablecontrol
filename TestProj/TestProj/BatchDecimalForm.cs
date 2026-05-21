using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using TestProj.Models;

namespace TestProj
{
    /// <summary>
    /// 按 TagName 前缀批量设置精确位数的规则编辑窗体。
    /// 规则列表按顺序匹配，第一条命中的规则生效。
    /// </summary>
    public partial class BatchDecimalForm : Krypton.Toolkit.KryptonForm
    {
        // ── DGV column indices ──────────────────────────────────────────────
        private const int ColPrefix     = 0;
        private const int ColDecimal    = 1;
        private const int ColMatchCount = 2;

        private BindingList<DecimalRule> _rules;

        public BatchDecimalForm()
        {
            InitializeComponent();
            this.Font = new Font("Segoe UI", 13F, FontStyle.Regular,
                                 GraphicsUnit.Point, (byte)134);

            // Load saved rules (deep copy so cancel doesn't mutate)
            var saved = ConfigManager.Config?.DecimalRules ?? new List<DecimalRule>();
            _rules = new BindingList<DecimalRule>(
                saved.Select(r => new DecimalRule
                {
                    Prefix       = r.Prefix,
                    DecimalPlaces = r.DecimalPlaces
                }).ToList());

            SetupDgv();

            dgvRules.DataSource = _rules;
            RefreshMatchCounts();

            dgvRules.CellEndEdit      += (s, e) => RefreshMatchCounts();
            dgvRules.RowsAdded        += (s, e) => RefreshMatchCounts();
            dgvRules.RowsRemoved      += (s, e) => RefreshMatchCounts();

            ApplyIcons();
            ApplyLanguage();
        }

        // ── DGV setup ────────────────────────────────────────────────────────

        private void SetupDgv()
        {
            dgvRules.AutoGenerateColumns = false;

            var colPrefix = new DataGridViewTextBoxColumn
            {
                Name             = "colPrefix",
                DataPropertyName = "Prefix",
                FillWeight       = 35F,
                MinimumWidth     = 100,
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill,
            };

            var colDecimal = new DataGridViewTextBoxColumn
            {
                Name             = "colDecimal",
                DataPropertyName = "DecimalPlaces",
                FillWeight       = 20F,
                MinimumWidth     = 80,
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            var colMatch = new DataGridViewTextBoxColumn
            {
                Name         = "colMatchCount",
                FillWeight   = 30F,
                MinimumWidth = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly     = true,
                DefaultCellStyle = {
                    Alignment       = DataGridViewContentAlignment.MiddleCenter,
                    BackColor       = SystemColors.ControlLight,
                    ForeColor       = SystemColors.GrayText
                }
            };

            dgvRules.Columns.Add(colPrefix);
            dgvRules.Columns.Add(colDecimal);
            dgvRules.Columns.Add(colMatch);

            // Row height
            dgvRules.RowTemplate.Height = 48;
        }

        /// <summary>刷新"匹配变量数"列，供用户即时预览每条规则能命中多少 Tag。</summary>
        private void RefreshMatchCounts()
        {
            var tags = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();

            // Commit pending cell edit before reading values
            if (dgvRules.IsCurrentCellInEditMode)
                dgvRules.CommitEdit(DataGridViewDataErrorContexts.Commit);

            foreach (DataGridViewRow row in dgvRules.Rows)
            {
                if (row.IsNewRow) continue;
                string prefix = row.Cells["colPrefix"].Value?.ToString() ?? "";
                int count = string.IsNullOrEmpty(prefix)
                    ? 0
                    : tags.Count(t =>
                        t.TagName?.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) == true);
                row.Cells["colMatchCount"].Value = count;
            }
        }

        // ── Icons & Language ─────────────────────────────────────────────────

        public void ApplyIcons()
        {
            string saved = ConfigManager.Config?.Theme ?? "Microsoft365Blue";
            Enum.TryParse<Krypton.Toolkit.PaletteMode>(saved, out var mode);

            btnAddRule.Values.Image    = IconManager.Make(IconChar.Plus,       mode, 28);
            btnDeleteRule.Values.Image = IconManager.Make(IconChar.Trash,      mode, 28);
            btnApply.Values.Image      = IconManager.Make(IconChar.Bolt,       mode, 28);
            btnOK.Values.Image         = IconManager.Make(IconChar.FloppyDisk, mode, 28);
            btnCancel.Values.Image     = IconManager.Make(IconChar.Xmark,      mode, 28);
        }

        public void ApplyLanguage()
        {
            this.Text = LangHelper.Get("BDF_Title");
            lblHint.Values.Text = LangHelper.Get("BDF_Hint");

            dgvRules.Columns["colPrefix"].HeaderText     = LangHelper.Get("BDF_ColPrefix");
            dgvRules.Columns["colDecimal"].HeaderText    = LangHelper.Get("BDF_ColDecimal");
            dgvRules.Columns["colMatchCount"].HeaderText = LangHelper.Get("BDF_ColMatchCount");

            btnAddRule.Values.Text    = LangHelper.Get("BDF_BtnAdd");
            btnDeleteRule.Values.Text = LangHelper.Get("BDF_BtnDelete");
            btnApply.Values.Text      = LangHelper.Get("BDF_BtnApply");
            btnOK.Values.Text         = LangHelper.Get("GCP_BtnSave");
            btnCancel.Values.Text     = LangHelper.Get("GCP_BtnCancel");
        }

        // ── Button handlers ──────────────────────────────────────────────────

        private void BtnAddRule_Click(object sender, EventArgs e)
        {
            _rules.Add(new DecimalRule { Prefix = "", DecimalPlaces = 2 });
            int last = dgvRules.Rows.Count - 1;
            dgvRules.ClearSelection();
            dgvRules.Rows[last].Selected = true;
            dgvRules.CurrentCell = dgvRules.Rows[last].Cells["colPrefix"];
            dgvRules.BeginEdit(true);
            lblApplyResult.Visible = false;
        }

        private void BtnDeleteRule_Click(object sender, EventArgs e)
        {
            if (dgvRules.CurrentRow == null) return;
            int idx = dgvRules.CurrentRow.Index;
            if (idx >= 0 && idx < _rules.Count)
            {
                _rules.RemoveAt(idx);
                lblApplyResult.Visible = false;
                RefreshMatchCounts();
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            // Commit any in-progress cell edit
            dgvRules.EndEdit();

            var tags = ConfigManager.Config?.TagMeta;
            if (tags == null || tags.Count == 0)
            {
                MessageBox.Show(LangHelper.Get("BDF_NoTags"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Validate: all DecimalPlaces must be non-negative integers
            foreach (var rule in _rules)
            {
                if (rule.DecimalPlaces < 0)
                {
                    MessageBox.Show(LangHelper.Get("BDF_InvalidDecimal"),
                        LangHelper.Get("Msg_Warning"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int updated = ApplyRules(tags, _rules.ToList());
            ConfigManager.SaveConfig();
            RefreshMatchCounts();

            lblApplyResult.Values.Text = string.Format(LangHelper.Get("BDF_ApplyResult"), updated);
            lblApplyResult.Visible     = true;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            dgvRules.EndEdit();

            // Save rule list to config
            if (ConfigManager.Config != null)
            {
                ConfigManager.Config.DecimalRules = _rules.ToList();
                ConfigManager.SaveConfig();
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ── Core logic ───────────────────────────────────────────────────────

        /// <summary>
        /// 将规则列表应用到 tags 集合，返回实际被修改的变量数。
        /// 每个 tag 只由第一条命中的规则决定，后续规则跳过。
        /// </summary>
        public static int ApplyRules(List<TagMeta> tags, List<DecimalRule> rules)
        {
            int count = 0;
            var validRules = rules
                .Where(r => !string.IsNullOrEmpty(r.Prefix))
                .ToList();

            foreach (var tag in tags)
            {
                foreach (var rule in validRules)
                {
                    if (tag.TagName?.StartsWith(rule.Prefix,
                            StringComparison.OrdinalIgnoreCase) == true)
                    {
                        tag.DecimalPlaces = rule.DecimalPlaces;
                        count++;
                        break;   // first match wins
                    }
                }
            }
            return count;
        }
    }
}
