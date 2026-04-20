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
        private List<string> _selectedTags;
        private BindingList<TagMeta> _bindingList;
        private bool _isDirty = false;
        private bool _isSyncing = false; // 防止双向同步时互相触发

        public TagConfigPanel(List<string> selectedTags)
        {
            InitializeComponent();
            _selectedTags = selectedTags;
            InitGrid();
            LoadData();

            // 选中行 → 填入下方表单
            dgvTags.SelectionChanged += DgvTags_SelectionChanged;

            // 下方表单修改 → 同步回选中行
            txtDisplayName.TextChanged += FormField_Changed;
            txtDecimalPlaces.TextChanged += FormField_Changed;
            txtOrder.TextChanged += FormField_Changed;
            txtMax.TextChanged += FormField_Changed;
            txtMin.TextChanged += FormField_Changed;
        }

        // 初始化 DataGridView 列
        private void InitGrid()
        {
            dgvTags.Columns.Clear();

            dgvTags.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "变量名称",
                DataPropertyName = "TagName",
                Width = 180,
                ReadOnly = true
            });
            dgvTags.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "别名",
                DataPropertyName = "DisplayName",
                Width = 140
            });
            dgvTags.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "数据类型",
                DataPropertyName = "DataType",
                Width = 80,
                ReadOnly = true
            });
            dgvTags.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "小数位",
                DataPropertyName = "DecimalPlaces",
                Width = 60
            });
            dgvTags.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "顺序",
                DataPropertyName = "Order",
                Width = 50
            });
            dgvTags.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "下限值",
                DataPropertyName = "NormalMin",
                Width = 70
            });
            dgvTags.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "上限值",
                DataPropertyName = "NormalMax",
                Width = 70
            });

            // 表格直接编辑后同步到下方表单
            dgvTags.CellValueChanged += DgvTags_CellValueChanged;
        }

        // 加载数据：已选变量 + JSON 已有配置合并
        public void LoadData()
        {
            var allMeta = ConfigManager.Config?.TagMeta ?? new List<TagMeta>();

            // 同时从数据库拿 DataType
            var dbTags = DbManager.FetchAllTags();
            var dbDict = dbTags.ToDictionary(
                t => t.ValueName.Contains("\\")
                    ? t.ValueName.Split('\\').Last()
                    : t.ValueName,
                t => t.DataType); // tagName → DataType

            var toShow = new List<TagMeta>();
            foreach (var fullName in _selectedTags)
            {
                string tagName = fullName.Contains("\\")
                    ? fullName.Split('\\').Last() : fullName; //to get "tag1" in Archive/tag1

                var existing = allMeta.FirstOrDefault(
                    t => t.TagName == tagName); // find existing config in json

                // 取数据库里的 DataType
                dbDict.TryGetValue(tagName, out string dataType);

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
                        TagName = tagName,
                        DisplayName = tagName,
                        DataType = dataType ?? "",
                        NormalMin = 0,
                        NormalMax = 100,
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

        // 选中行 → 填入下方表单
        private void DgvTags_SelectionChanged(object sender, EventArgs e)
        {
            if (_isSyncing) return;
            var meta = GetSelectedMeta();
            if (meta == null) return;

            _isSyncing = true;
            txtTagName.Text = meta.TagName;
            txtDisplayName.Text = meta.DisplayName;
            txtDataType.Text = meta.DataType;
            txtDecimalPlaces.Text = meta.DecimalPlaces.ToString();
            txtOrder.Text = meta.Order.ToString();
            txtMax.Text = meta.NormalMax.ToString();
            txtMin.Text = meta.NormalMin.ToString();
            _isSyncing = false;
        }

        // 下方表单修改 → 同步回 DataGridView 选中行
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
                txtMax.Text, out double mx) ? mx : 100;
            meta.NormalMin = double.TryParse(
                txtMin.Text, out double mn) ? mn : 0;

            dgvTags.Refresh();
            _isSyncing = false;
            MarkDirty();
        }

        // DataGridView 直接编辑后同步到下方表单
        private void DgvTags_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (_isSyncing || e.RowIndex < 0) return;
            DgvTags_SelectionChanged(sender, e);
            MarkDirty();
        }

        // 标记有未保存修改，当前行变黄
        private void MarkDirty()
        {
            _isDirty = true;
            int idx = dgvTags.CurrentRow?.Index ?? -1;
            if (idx >= 0)
                dgvTags.Rows[idx].DefaultCellStyle.BackColor =
                    Color.LightYellow;
        }

        // 保存配置按钮
        private void SaveConfigBtn_Click_1(object sender, EventArgs e)
        {
            SaveAll();
            MessageBox.Show("保存成功", "提示");
        }
        public bool HasUnsavedChanges() => _isDirty;

        // 把编辑结果合并回全量配置并写入 JSON
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

        private TagMeta GetSelectedMeta()
        {
            if (dgvTags.CurrentRow?.DataBoundItem is TagMeta meta)
                return meta;
            return null;
        }
 
    }
}