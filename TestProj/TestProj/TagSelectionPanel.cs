using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestProj
{
    public partial class TagSelectionPanel : Form
    {
        /// <summary>
        /// 用户确认后，外部通过这个属性拿到选中的变量列表
        /// ValueName（后续用于 TAG:R 查询）
        /// </summary>
        public List<string> SelectedTags { get; private set; }
            = new List<string>();

        // 上次已选的变量，重新打开面板时用于恢复勾选状态
        private List<string> _previousSelected;

        // 防止父子节点互相触发 AfterCheck 造成无限递归
        private bool _isUpdating = false;

        public TagSelectionPanel(List<string> previousSelected = null)
        {
            InitializeComponent();
            _previousSelected = previousSelected ?? new List<string>();
            LoadTree();
        }

        /// <summary>
        /// 从数据库读取所有归档变量，按归档名分组后构建树形结构
        /// </summary>
        private void LoadTree()
        {
            // 从 SQL dbo.Archive 读取所有变量，返回 (ValueID, ValueName) 列表
            // ValueName 格式："归档名\变量名"，例如 "Archive\tag1"
            var tags = DbManager.FetchAllTags();
            tagTreeView.Nodes.Clear();
            tagTreeView.CheckBoxes = true;

            // 按 \ 前面的归档名分组
            // 例如："Archive\tag1" 和 "Archive\tag2" 归到 "Archive" 组
            // 没有 \ 的变量归到 "未分类" 组
            var groups = tags
                .GroupBy(t => t.ValueName.Contains("\\")
                    ? t.ValueName.Split('\\')[0]
                    : "未分类")
                .OrderBy(g => g.Key); // 按归档名字母顺序排列

            // 逐个归档建树
            foreach (var group in groups)
            {
                // 归档名作为父节点，例如 "Archive"
                var parentNode = new TreeNode(group.Key);

                // 该归档下的每个变量作为子节点
                foreach (var (valueID, valueName, datatype) in
                         group.OrderBy(t => t.ValueName))
                {
                    // 取 \ 后面的部分作为显示名，例如 "Archive\tag1" → "tag1"
                    string tagName = valueName.Contains("\\")
                        ? valueName.Split('\\').Last()
                        : valueName;

                    var childNode = new TreeNode(tagName)
                    {
                        // Tag 存 ValueName
                        Tag = valueName,
                        // 如果上次已选过这个变量，恢复勾选状态
                        Checked = _previousSelected.Contains(valueName.ToString())
                    };
                    parentNode.Nodes.Add(childNode);
                }

                // 如果该归档下所有变量都被勾选，父节点也自动勾选
                parentNode.Checked = parentNode.Nodes
                    .Cast<TreeNode>().All(n => n.Checked);

                tagTreeView.Nodes.Add(parentNode);
                parentNode.Expand(); // 默认展开所有归档
            }

            // 注册勾选事件（在树建完之后注册，避免构建时触发）
            tagTreeView.AfterCheck += TagTreeView_AfterCheck;
        }

        /// <summary>
        /// 点击确定按钮：收集所有勾选的子节点，存入 SelectedTags
        /// </summary>
        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            SelectedTags.Clear();

            // 只收集叶子节点（变量），不收集父节点（归档名）
            foreach (TreeNode parent in tagTreeView.Nodes)
                foreach (TreeNode child in parent.Nodes)
                    if (child.Checked)
                        SelectedTags.Add(child.Tag.ToString());

            if (SelectedTags.Count == 0)
            {
                MessageBox.Show("请至少选择一个变量", "提示");
                return;
            }

            // 关闭窗口并告知调用方用户点了确定
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