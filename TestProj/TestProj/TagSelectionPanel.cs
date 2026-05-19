using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestProj.Models;

namespace TestProj
{
    public partial class TagSelectionPanel : Krypton.Toolkit.KryptonForm
    {
        public List<TemplateTagEntry> SelectedTags { get; private set; }
            = new List<TemplateTagEntry>();

        private readonly List<long> _previousSelected;
        private bool _isUpdating = false;

        public TagSelectionPanel(List<long> previousSelected = null)
        {
            InitializeComponent();
            this.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)134);
            _previousSelected = previousSelected ?? new List<long>();
            LoadTree();
            ApplyLanguage();
        }

        /// <summary>刷新窗体所有 UI 文字为当前语言。</summary>
        public void ApplyLanguage()
        {
            this.Text             = LangHelper.Get("TSP_Title");
            confirmBtn.Values.Text = LangHelper.Get("TSP_BtnConfirm");
        }

        private void LoadTree()
        {
            var tags = DbManager.FetchAllTags();
            tagTreeView.Nodes.Clear();
            tagTreeView.CheckBoxes = true;

            var groups = tags
                .GroupBy(t => t.ValueName.Contains("\\")
                    ? t.ValueName.Split('\\')[0]
                    : "未分类")
                .OrderBy(g => g.Key);

            foreach (var group in groups)
            {
                var parentNode = new TreeNode(group.Key);

                foreach (var (valueID, valueName, datatype) in group.OrderBy(t => t.ValueName))
                {
                    string displayName = valueName.Contains("\\")
                        ? valueName.Split('\\').Last()
                        : valueName;

                    var childNode = new TreeNode(displayName)
                    {
                        Tag = new TemplateTagEntry { ValueID = valueID },
                        Checked = _previousSelected.Contains(valueID)
                    };
                    parentNode.Nodes.Add(childNode);
                }

                parentNode.Checked = parentNode.Nodes.Cast<TreeNode>().All(n => n.Checked);
                tagTreeView.Nodes.Add(parentNode);
                parentNode.Expand();
            }

            tagTreeView.AfterCheck += TagTreeView_AfterCheck;
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            SelectedTags.Clear();

            foreach (TreeNode parent in tagTreeView.Nodes)
                foreach (TreeNode child in parent.Nodes)
                    if (child.Checked)
                        SelectedTags.Add((TemplateTagEntry)child.Tag);

            if (SelectedTags.Count == 0)
            {
                MessageBox.Show(LangHelper.Get("TSP_NoSelection"), LangHelper.Get("Msg_Info"));
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 勾选联动逻辑：
        /// - 勾选/取消父节点 → 所有子节点跟随
        /// - 勾选/取消子节点 → 更新父节点状态（全选才勾父节点）
        /// </summary>
        private void TagTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // 防止父子节点互相触发形成死循环
            if (_isUpdating || e.Action == TreeViewAction.Unknown)
                return;

            _isUpdating = true;

            if (e.Node.Nodes.Count > 0)
            {
                // 操作的是父节点：子节点全部跟随父节点的勾选状态
                foreach (TreeNode child in e.Node.Nodes)
                    child.Checked = e.Node.Checked;
            }
            else
            {
                // 操作的是子节点：检查兄弟节点是否全部勾选
                // 全部勾选则父节点也勾上，否则父节点取消勾选
                var parent = e.Node.Parent;
                if (parent != null)
                    parent.Checked = parent.Nodes.Cast<TreeNode>().All(n => n.Checked);
            }

            _isUpdating = false;
        }
    }
}