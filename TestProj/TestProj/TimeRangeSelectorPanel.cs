using System;
using System.Windows.Forms;

namespace TestProj
{
    public partial class TimeRangeSelectorPanel : Form
    {
        public DateTime SelectedStartTime { get; private set; }
        public DateTime SelectedEndTime { get; private set; }
        public int SelectedMode { get; private set; }
        public int SelectedCount { get; private set; }
        public int SelectedTimeStep { get; private set; }

        public event Action<DateTime, DateTime, int, int> Applied;

        public TimeRangeSelectorPanel(DateTime? initialStart = null,
                                      DateTime? initialEnd = null)
        {
            InitializeComponent();

            dtpStartDate.Value = initialStart?.Date ?? DateTime.Today;
            dtpStartTime.Value = initialStart ?? DateTime.Now.AddHours(-1);
            dtpEndDate.Value = initialEnd?.Date ?? DateTime.Today;
            dtpEndTime.Value = initialEnd ?? DateTime.Now;

            cmbUnit.SelectedIndex = 2; // 默认 1小时
            cmbMode.SelectedIndex = 0;


            UpdateEnabledState();
        }

        // 模式切换

        private void CmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
            if (cmbMode.SelectedIndex == 0)
                RecalcEndFromRange();
        }

        private void UpdateEnabledState()
        {
            int mode = cmbMode.SelectedIndex;

            // 开始时间：所有模式都启用
            dtpStartDate.Enabled = true;
            dtpStartTime.Enabled = true;

            // 结束时间：mode 1（开始到结束）和 mode 3（自定义聚类）启用
            dtpEndDate.Enabled = mode == 1 || mode == 3;
            dtpEndTime.Enabled = mode == 1 || mode == 3;

            // 时间范围条数 numValue 和 "X" 标签：仅 mode 0（时间范围）
            numValue.Visible = mode == 0;
            numValue.Enabled = mode == 0;
            label5.Visible   = mode == 0; // "X" 乘号

            // 粒度下拉：mode 0（计算区间）和 mode 3（聚类步长）
            cmbUnit.Enabled = mode == 0 || mode == 3;

            // label4 语义：mode 3 时改为"聚合粒度："，其他为"时间范围："
            label4.Text = mode == 3 ? "聚合粒度：" : "时间范围：";

            // 测量点数量：mode 2
            numCount.Enabled = mode == 2;
        }

        // mode=0: 推算结束时间

        private void RangeInput_Changed(object sender, EventArgs e)
        {
            if (cmbMode.SelectedIndex == 0)
                RecalcEndFromRange();
        }

        private void RecalcEndFromRange()
        {
            var start = GetStart();
            var end = start.AddSeconds(GetRangeSeconds());
            dtpEndDate.Value = end.Date;
            dtpEndTime.Value = end;
        }

        private double GetRangeSeconds()
        {
            double count = (double)numValue.Value;
            switch (cmbUnit.SelectedIndex)
            {
                case 0: return count * 1;
                case 1: return count * 60;
                case 2: return count * 3600;
                case 3: return count * 86400;
                default: return count * 3600;
            }
        }

        private int GetTimeStepSeconds()
        {
            // 只有 mode 3（自定义聚类）才使用聚合步长；其他 mode 返回原始点（0）
            if (cmbMode.SelectedIndex != 3) return 0;

            switch (cmbUnit.SelectedIndex)
            {
                case 0: return 1;
                case 1: return 60;
                case 2: return 3600;
                case 3: return 86400;
                default: return 3600;
            }
        }

        // 读取当前 Start / End

        private DateTime GetStart()
        {
            return dtpStartDate.Value.Date + dtpStartTime.Value.TimeOfDay;
        }

        private (DateTime start, DateTime end) GetCurrentRange()
        {
            var start = GetStart();

            switch (cmbMode.SelectedIndex)
            {
                case 0: // 时间范围：start + numValue × unit
                    return (start, start.AddSeconds(GetRangeSeconds()));

                case 1: // 开始到结束：原始点
                    var end1 = dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay;
                    return (start, end1);

                case 2: // 测量点数量：结束时间占位
                    return (start, start);

                case 3: // 自定义区间聚合：手动选 start + end
                    var end3 = dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay;
                    return (start, end3);

                default:
                    return (start, start.AddHours(1));
            }
        }

        private bool ValidateRange()
        {
            if (cmbMode.SelectedIndex == 2) return true;

            var (start, end) = GetCurrentRange();
            if (start >= end)
            {
                MessageBox.Show("开始时间不能晚于或等于结束时间", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // 填充对外属性

        private void ApplySelection()
        {
            var (start, end) = GetCurrentRange();
            SelectedStartTime = start;
            SelectedEndTime = end;
            SelectedMode = cmbMode.SelectedIndex;
            SelectedCount = (int)numCount.Value;
            SelectedTimeStep = GetTimeStepSeconds();

            Applied?.Invoke(start, end, SelectedMode, SelectedTimeStep);
        }

        // 按钮事件

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateRange()) return;
            ApplySelection();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (!ValidateRange()) return;
            ApplySelection();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}