using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using TestProj.Models;

namespace TestProj
{
    public partial class TemplateConfigPanel : Form
    {
        private readonly BindingList<ReportTemplate> _bindingList;
        private readonly List<TagMeta> _allTags;
        private ReportTemplate _editing;

        public TemplateConfigPanel(List<ReportTemplate> templates, List<TagMeta> allTags)
        {
            InitializeComponent();
            _allTags = allTags ?? new List<TagMeta>();
            templates = templates ?? new List<ReportTemplate>();

            _bindingList = new BindingList<ReportTemplate>(templates);
            lstTemplates.DisplayMember = "DisplayName";
            lstTemplates.DataSource = _bindingList;

            pnlDetail.Visible = false;
        }


        /// <summary>
        /// 选择一个模板
        /// </summary>
        private void LstTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTemplates.SelectedItem is ReportTemplate t)
            {
                _editing = JsonConvert.DeserializeObject<ReportTemplate>(
                           JsonConvert.SerializeObject(t));
                BindDetail();
            }
        }

        /// <summary>
        /// 创建新的模板按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _editing = new ReportTemplate
            {
                Id = "tpl_" + DateTime.Now.Ticks,
                DisplayName = "新建模板",
                Description = "",
                SelectedTags = new List<TemplateTagEntry>()
            };
            lstTemplates.SelectedIndex = -1;
            BindDetail();
            txtName.Focus();
            txtName.SelectAll();
        }

        /// <summary>
        /// 变量选择按钮点击事件，弹出TagSelection Panel
        /// </summary>
        private void BtnSelectTags_Click(object sender, EventArgs e)
        {
            // TagSelectionPanel 需要传 List<string>，从 entries 里取
            var previousSelected = _editing.SelectedTags
                .Select(entry => entry.TagName)
                .ToList();

            using (var panel = new TagSelectionPanel(previousSelected))
            {
                if (panel.ShowDialog() == DialogResult.OK)
                {
                    // 保留已有 Order，新增的从末尾续编
                    var existingDict = _editing.SelectedTags
                        .ToDictionary(entry => entry.TagName, entry => entry.Order);

                    int maxOrder = existingDict.Count > 0 ? existingDict.Values.Max() : 0;

                    _editing.SelectedTags = panel.SelectedTags.Select((tagName, idx) =>
                        new TemplateTagEntry
                        {
                            TagName = tagName,
                            Order = existingDict.TryGetValue(tagName, out int o)
                                      ? o
                                      : maxOrder + idx + 1
                        }).ToList();

                    UpdateTagSummary();
                }
            }
        }

        /// <summary>
        /// 变量配置按钮点击事件，弹出TagConfig Panel
        /// </summary>
        private void BtnConfigTag_Click(object sender, EventArgs e)
        {
            if (_editing.SelectedTags.Count == 0)
            {
                MessageBox.Show("请先选择变量", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedTagNames = _editing.SelectedTags
                .Select(entry => entry.TagName)
                .ToList();

            using (var panel = new TagConfigPanel(selectedTagNames, _editing.SelectedTags))
            {
                panel.FormClosing += (s, args) =>
                {
                    if (panel.HasUnsavedChanges())
                    {
                        var result = MessageBox.Show(
                            "有未保存的修改，是否在关闭前保存？",
                            "提示",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes) panel.SaveAll();
                        else if (result == DialogResult.Cancel) args.Cancel = true;
                        else if (result == DialogResult.No) panel.DiscardAllChanges();
                    }
                };
                panel.ShowDialog();
                UpdateTagSummary();
            }
        }

        #region 模板数据读写操作
        /// <summary>
        /// 保存按钮
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("模板名称不能为空", "提示",
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

            // 同步到 ConfigManager
            ConfigManager.Config.Template = _bindingList.ToList();
            ConfigManager.SaveConfig();
        }
        /// <summary>
        /// 取消修改按钮
        /// </summary>
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

        /// <summary>
        /// 复制并创建新模板按钮
        /// </summary>
        private void BtnClone_Click(object sender, EventArgs e)
        {
            if (_editing == null) return;
            _editing = new ReportTemplate
            {
                Id = "tpl_" + DateTime.Now.Ticks,
                DisplayName = _editing.DisplayName + "_副本",
                Description = _editing.Description,
                // 改后
                SelectedTags = _editing.SelectedTags.Select(entry => new TemplateTagEntry{
                    TagName = entry.TagName,
                    Order = entry.Order
                }).ToList()
            };
            lstTemplates.SelectedIndex = -1;
            BindDetail();
            txtName.Focus();
            txtName.SelectAll();
        }

        /// <summary>
        /// 删除模板按钮
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // 改成
            var t = lstTemplates.SelectedItem as ReportTemplate;
            if (t == null) return;

            var confirm = MessageBox.Show(
                $"确认删除模板「{t.DisplayName}」？",
                "删除确认",
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
        #endregion

        #region Utility

        // 详情绑定
        private void BindDetail()
        {
            txtName.Text = _editing.DisplayName;
            txtDesc.Text = _editing.Description;
            UpdateTagSummary();
            pnlDetail.Visible = true;
        }

        // 展示已选变量的数量和名称（优先显示 DisplayName）
        private void UpdateTagSummary()
        {
            if (_editing == null || _editing.SelectedTags.Count == 0)
            {
                txtTagSummary.Text = "未选择任何变量";
                return;
            }
            var names = _editing.SelectedTags
                .OrderBy(entry => entry.Order)
                .Select(entry => _allTags.FirstOrDefault(m => m.TagName == entry.TagName)
                                 ?.DisplayName ?? entry.TagName)
                .ToList();
            txtTagSummary.Text = $"已选 {names.Count} 个：{string.Join("、", names)}";
        }

        #endregion
    }
}