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
        private class TagRow
        {
            public string TagName { get; set; }
            public string DisplayName { get; set; }
            public string DataType { get; set; }
            public int DecimalPlaces { get; set; }
            public double? NormalMin { get; set; }
            public double? NormalMax { get; set; }
            public int Order { get; set; }
        }

        private readonly List<string> _selectedTags;
        private readonly List<TemplateTagEntry> _templateEntries;
        private BindingList<TagRow> _bindingList;
        private bool _isDirty = false;
        private bool _isSyncing = false;
        private Dictionary<string, TagRow> _editingBackups = new Dictionary<string, TagRow>();
        private TagRow _currentEditing;

        public TagConfigPanel(List<string> selectedTags, List<TemplateTagEntry> templateEntries)
        {
            InitializeComponent();
            _selectedTags = selectedTags;
            _templateEntries = templateEntries;

            LoadData();

            dgvTags.CellValueChanged += DgvTags_CellValueChanged;
            dgvTags.SelectionChanged += DgvTags_SelectionChanged;

            txtDisplayName.TextChanged += FormField_Changed;
            txtDecimalPlaces.TextChanged += FormField_Changed;
            txtOrder.TextChanged += FormField_Changed;
            txtMax.TextChanged += FormField_Changed;
            txtMin.TextChanged += FormField_Changed;
        }

        public void LoadData()
        {
            var allMeta = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();
            var dbTags = DbManager.FetchAllTags();
            var dbDict = dbTags.ToDictionary(t => t.ValueName, t => t.DataType);

            var toShow = new List<TagRow>();
            foreach (var fullName in _selectedTags)
            {
                var meta = allMeta.FirstOrDefault(t => t.TagName == fullName);
                var entry = _templateEntries.FirstOrDefault(e => e.TagName == fullName);
                dbDict.TryGetValue(fullName, out string dataType);

                toShow.Add(new TagRow
                {
                    TagName = fullName,
                    DisplayName = meta?.DisplayName ?? fullName,
                    DataType = dataType ?? meta?.DataType ?? "",
                    DecimalPlaces = meta?.DecimalPlaces ?? 2,
                    NormalMin = meta?.NormalMin,
                    NormalMax = meta?.NormalMax,
                    Order = entry?.Order ?? (toShow.Count + 1)
                });
            }

            _bindingList = new BindingList<TagRow>(
                                    toShow.OrderBy(t => t.Order).ToList());
            dgvTags.DataSource = _bindingList;

            if (dgvTags.Rows.Count > 0)
                dgvTags.Rows[0].Selected = true;
        }

        private void DgvTags_SelectionChanged(object sender, EventArgs e)
        {
            if (_isSyncing) return;
            var row = GetSelectedRow();
            if (row == null) return;

            if (!_editingBackups.ContainsKey(row.TagName))
            {
                _editingBackups[row.TagName] = new TagRow
                {
                    TagName = row.TagName,
                    DisplayName = row.DisplayName,
                    DataType = row.DataType,
                    NormalMin = row.NormalMin,
                    NormalMax = row.NormalMax,
                    DecimalPlaces = row.DecimalPlaces,
                    Order = row.Order
                };
            }

            _currentEditing = row;
            _isSyncing = true;
            LoadToForm(row);
            _isSyncing = false;
        }

        private void FormField_Changed(object sender, EventArgs e)
        {
            if (_isSyncing) return;
            var row = GetSelectedRow();
            if (row == null) return;

            _isSyncing = true;
            row.DisplayName = txtDisplayName.Text.Trim();
            row.DecimalPlaces = int.TryParse(txtDecimalPlaces.Text, out int dp) ? dp : 2;
            row.NormalMax = double.TryParse(txtMax.Text, out double mx) ? mx : (double?)null;
            row.NormalMin = double.TryParse(txtMin.Text, out double mn) ? mn : (double?)null;

            int newOrder = int.TryParse(txtOrder.Text, out int ord) ? ord : 1;
            AdjustOrders(row, newOrder);

            dgvTags.Refresh();
            _isSyncing = false;
            MarkDirty();
        }

        private void DgvTags_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isSyncing || e.RowIndex < 0) return;
            DgvTags_SelectionChanged(sender, e);
            MarkDirty();
        }

        private void AdjustOrders(TagRow target, int desiredOrder)
        {
            if (target == null || _bindingList == null || _bindingList.Count == 0) return;

            int count = _bindingList.Count;
            desiredOrder = Math.Max(1, Math.Min(desiredOrder, count));
            int oldOrder = target.Order;
            if (desiredOrder == oldOrder) return;

            if (desiredOrder < oldOrder)
            {
                foreach (var r in _bindingList.Where(
                    t => t != target && t.Order >= desiredOrder && t.Order < oldOrder))
                    r.Order++;
            }
            else
            {
                foreach (var r in _bindingList.Where(
                    t => t != target && t.Order <= desiredOrder && t.Order > oldOrder))
                    r.Order--;
            }

            target.Order = desiredOrder;

            var sorted = _bindingList.OrderBy(t => t.Order).ToList();
            _bindingList = new BindingList<TagRow>(sorted);
            dgvTags.DataSource = _bindingList;

            _currentEditing = _bindingList.FirstOrDefault(t => t.TagName == target.TagName);

            for (int i = 0; i < dgvTags.Rows.Count; i++)
            {
                if ((dgvTags.Rows[i].DataBoundItem as TagRow)?.TagName == target.TagName)
                {
                    dgvTags.ClearSelection();
                    dgvTags.Rows[i].Selected = true;
                    dgvTags.CurrentCell = dgvTags.Rows[i].Cells[0];
                    break;
                }
            }
        }

        #region Utilities
        private void MarkDirty()
        {
            _isDirty = true;
            int idx = dgvTags.CurrentRow?.Index ?? -1;
            if (idx >= 0)
                dgvTags.Rows[idx].DefaultCellStyle.BackColor = Color.LightYellow;
        }

        private TagRow GetSelectedRow()
        {
            if (dgvTags.CurrentRow?.DataBoundItem is TagRow row)
                return row;
            return null;
        }

        private void LoadToForm(TagRow row)
        {
            txtTagName.Text = row.TagName;
            txtDisplayName.Text = row.DisplayName;
            txtDataType.Text = row.DataType;
            txtDecimalPlaces.Text = row.DecimalPlaces.ToString();
            txtOrder.Text = row.Order.ToString();
            txtMax.Text = row.NormalMax?.ToString() ?? "";
            txtMin.Text = row.NormalMin?.ToString() ?? "";
        }
        #endregion

        #region 外部接口
        public bool HasUnsavedChanges() => _isDirty;




        public void SaveAll()
        {
            var allMeta = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();

            foreach (var row in _bindingList)
            {
                // TagMeta 只存显示属性，不存 Order
                var existing = allMeta.FirstOrDefault(t => t.TagName == row.TagName);
                if (existing != null)
                {
                    existing.DisplayName = row.DisplayName;
                    existing.DataType = row.DataType;
                    existing.NormalMin = row.NormalMin;
                    existing.NormalMax = row.NormalMax;
                    existing.DecimalPlaces = row.DecimalPlaces;
                }
                else
                {
                    allMeta.Add(new TagMeta
                    {
                        TagName = row.TagName,
                        DisplayName = row.DisplayName,
                        DataType = row.DataType,
                        NormalMin = row.NormalMin,
                        NormalMax = row.NormalMax,
                        DecimalPlaces = row.DecimalPlaces
                    });
                }

                // Order 写回 templateEntries
                var entry = _templateEntries.FirstOrDefault(e => e.TagName == row.TagName);
                if (entry != null)
                    entry.Order = row.Order;
            }

            ConfigManager.Config.TagMeta = allMeta;
            ConfigManager.SaveConfig();
            _isDirty = false;

            foreach (DataGridViewRow r in dgvTags.Rows)
                r.DefaultCellStyle.BackColor = Color.Empty;
        }

        public void DiscardAllChanges()
        {
            foreach (var backup in _editingBackups)
            {
                var row = _bindingList.FirstOrDefault(t => t.TagName == backup.Key);
                if (row != null)
                {
                    row.DisplayName = backup.Value.DisplayName;
                    row.DataType = backup.Value.DataType;
                    row.NormalMin = backup.Value.NormalMin;
                    row.NormalMax = backup.Value.NormalMax;
                    row.DecimalPlaces = backup.Value.DecimalPlaces;
                    row.Order = backup.Value.Order;

                    // 还原 templateEntries
                    var entry = _templateEntries.FirstOrDefault(e => e.TagName == backup.Key);
                    if (entry != null)
                        entry.Order = backup.Value.Order;
                }
            }

            _editingBackups.Clear();
            dgvTags.Refresh();
            if (_currentEditing != null)
                LoadToForm(_currentEditing);
        }

        private void SaveConfigBtn_Click_1(object sender, EventArgs e)
        {
            SaveAll();
            MessageBox.Show("保存成功", "提示");
            _editingBackups.Clear();
        }
        #endregion
    }
}