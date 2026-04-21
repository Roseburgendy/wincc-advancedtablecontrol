using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TestProj.Models;

namespace TestProj
{
    public partial class TagConfigPanel : Form
    {
        private readonly List<string> _selectedTags;
        private BindingList<TagMeta> _bindingList;
        private bool _isDirty = false;
        private bool _isSyncing = false;
        public Dictionary<string,TagMeta> _editingBackups = new Dictionary<string, TagMeta>(); // 备份
        private TagMeta _currentEditing;
        public TagConfigPanel(List<string> selectedTags)
        {
            InitializeComponent();
            _selectedTags = selectedTags;
            LoadData();
            dgvTags.CellValueChanged += DgvTags_CellValueChanged;
            // 选中行 → 填入下方表单
            dgvTags.SelectionChanged += DgvTags_SelectionChanged;

            // 下方表单修改 → 同步回选中行
            txtDisplayName.TextChanged += FormField_Changed;
            txtDecimalPlaces.TextChanged += FormField_Changed;
            txtOrder.TextChanged += FormField_Changed;
            txtMax.TextChanged += FormField_Changed;
            txtMin.TextChanged += FormField_Changed;
        }


        // 加载数据
        public void LoadData()
        {
            var allMeta = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();

            // 同时从数据库拿 DataType
            var dbTags = DbManager.FetchAllTags();
            var dbDict = dbTags.ToDictionary(
                t => t.ValueName,
                t => t.DataType); // tagName → DataType

            var toShow = new List<TagMeta>();
            foreach (var fullName in _selectedTags)
            {
                var existing = allMeta.FirstOrDefault(
                    t => t.TagName == fullName); // find existing config in json

                // 取数据库里的 DataType
                dbDict.TryGetValue(fullName, out string dataType);

                if (existing != null)
                {
                    // 更新 DataType（可能变过）
                    existing.DataType = dataType ?? existing.DataType;
                    toShow.Add(existing);
                }
                else
                {
                    toShow.Add(new TagMeta
                    {
                        TagName = fullName,
                        DisplayName = fullName,
                        DataType = dataType ?? "",
                        NormalMin = null,
                        NormalMax = null,
                        DecimalPlaces = 2,
                        Order = toShow.Count + 1
                    });
                }
            }

            _bindingList = new BindingList<TagMeta>(toShow.OrderBy(t => t.Order).ToList());
            dgvTags.DataSource = _bindingList;

            // 默认选中第一行
            if (dgvTags.Rows.Count > 0)
                dgvTags.Rows[0].Selected = true;
        }

        /// <summary>
        /// 选中表格中一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvTags_SelectionChanged(object sender, EventArgs e)
        {
            if (_isSyncing) return; 
            var meta = GetSelectedMeta();
            if (meta == null) return;
            // 将当前行存档
            if(!_editingBackups.ContainsKey(meta.TagName))
            {
                    _editingBackups[meta.TagName] = new TagMeta
                    {
                        TagName = meta.TagName,
                        DisplayName = meta.DisplayName,
                        DataType = meta.DataType,
                        NormalMin = meta.NormalMin,
                        NormalMax = meta.NormalMax,
                        DecimalPlaces = meta.DecimalPlaces,
                        Order = meta.Order
                    };

            }
            _currentEditing = meta;
            _isSyncing = true;
            LoadToForm(meta);
            _isSyncing = false;
        }

        // 保存配置按钮
        private void SaveConfigBtn_Click_1(object sender, EventArgs e)
        {
            SaveAll();
            MessageBox.Show("保存成功", "提示");
            _editingBackups.Clear();
        }

        /// <summary>
        /// 下方表单修改订阅事件
        /// </summary>
        /// <param name="sender">The source of the event, typically a form field control.</param>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        private void FormField_Changed(object sender, EventArgs e)
        {
            if (_isSyncing) return;
            var meta = GetSelectedMeta();
            if (meta == null) return;

            _isSyncing = true;
            meta.DisplayName = txtDisplayName.Text.Trim();
            meta.DecimalPlaces = int.TryParse(
                txtDecimalPlaces.Text, out int dp) ? dp : 2;
            meta.Order = int.TryParse(
                txtOrder.Text, out int ord) ? ord : 1;
            meta.NormalMax = double.TryParse(
                txtMax.Text, out double mx) ? mx : (double?)null;
            meta.NormalMin = double.TryParse(
                txtMin.Text, out double mn) ? mn : (double?)null;

            dgvTags.Refresh();
            _isSyncing = false;
            MarkDirty();
        }

       /// <summary>
       /// 表格数据修改同步下方表单
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void DgvTags_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (_isSyncing || e.RowIndex < 0) return;
            DgvTags_SelectionChanged(sender, e);
            MarkDirty();
        }

        #region Utilities
        // 标记有未保存修改，当前行变黄
        private void MarkDirty()
        {
            _isDirty = true;
            int idx = dgvTags.CurrentRow?.Index ?? -1;
            if (idx >= 0)
                dgvTags.Rows[idx].DefaultCellStyle.BackColor =
                    Color.LightYellow;
        }
        private TagMeta GetSelectedMeta()
        {
            if (dgvTags.CurrentRow?.DataBoundItem is TagMeta meta)
                return meta;
            return null;
        }

        #endregion

        #region 外部接口
        public bool HasUnsavedChanges() => _isDirty;
        public void LoadToForm(TagMeta meta)
        {
            txtTagName.Text = meta.TagName;
            txtDisplayName.Text = meta.DisplayName;
            txtDataType.Text = meta.DataType;
            txtDecimalPlaces.Text = meta.DecimalPlaces.ToString();
            txtOrder.Text = meta.Order.ToString();
            txtMax.Text = meta.NormalMax.ToString();
            txtMin.Text = meta.NormalMin.ToString();
        }
        public void SaveAll()
        {
            var allMeta = ConfigManager.Config?.TagMeta
                          ?? new List<TagMeta>();

            foreach (var edited in _bindingList)
            {
                var existing = allMeta.FirstOrDefault(
                    t => t.TagName == edited.TagName);

                if (existing != null)
                {
                    existing.DisplayName = edited.DisplayName;
                    existing.DataType = edited.DataType;
                    existing.NormalMin = edited.NormalMin;
                    existing.NormalMax = edited.NormalMax;
                    existing.DecimalPlaces = edited.DecimalPlaces;
                    existing.Order = edited.Order;
                }
                else
                {
                    allMeta.Add(edited);
                }
            }

            ConfigManager.Config.TagMeta = allMeta;
            ConfigManager.SaveConfig();
            _isDirty = false;

            foreach (DataGridViewRow row in dgvTags.Rows)
                row.DefaultCellStyle.BackColor = Color.Empty;
        }

        public void DiscardAllChanges()
        {
            foreach(var backup in _editingBackups)
            {
                var meta = _bindingList.FirstOrDefault(t => t.TagName == backup.Key);
                if (meta != null)
                {
                    meta.DisplayName = backup.Value.DisplayName;
                    meta.DataType = backup.Value.DataType;
                    meta.NormalMin = backup.Value.NormalMin;
                    meta.NormalMax = backup.Value.NormalMax;
                    meta.DecimalPlaces = backup.Value.DecimalPlaces;
                    meta.Order = backup.Value.Order;
                }
            }

            _editingBackups.Clear();

            dgvTags.Refresh();
            if (_currentEditing != null)
             {
                    LoadToForm(_currentEditing);
            }
        }
        #endregion

    }
}