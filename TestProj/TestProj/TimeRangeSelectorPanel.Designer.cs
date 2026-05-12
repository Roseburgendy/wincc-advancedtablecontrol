namespace TestProj
{
    partial class TimeRangeSelectorPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBox1    = new System.Windows.Forms.GroupBox();
            this.numCount     = new Krypton.Toolkit.KryptonNumericUpDown();
            this.numValue     = new Krypton.Toolkit.KryptonNumericUpDown();
            this.label6       = new Krypton.Toolkit.KryptonLabel();
            this.label5       = new Krypton.Toolkit.KryptonLabel();
            this.cmbUnit      = new Krypton.Toolkit.KryptonComboBox();
            this.label4       = new Krypton.Toolkit.KryptonLabel();
            this.label3       = new Krypton.Toolkit.KryptonLabel();
            this.dtpEndTime   = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate   = new System.Windows.Forms.DateTimePicker();
            this.label2       = new Krypton.Toolkit.KryptonLabel();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbMode      = new Krypton.Toolkit.KryptonComboBox();
            this.label1       = new Krypton.Toolkit.KryptonLabel();
            this.btnOK        = new Krypton.Toolkit.KryptonButton();
            this.btnCancel    = new Krypton.Toolkit.KryptonButton();
            this.btnApply     = new Krypton.Toolkit.KryptonButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMode)).BeginInit();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.numCount);
            this.groupBox1.Controls.Add(this.numValue);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpEndTime);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpStartTime);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.cmbMode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(28, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(932, 558);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时间范围配置";
            //
            // numCount
            //
            this.numCount.Location = new System.Drawing.Point(518, 481);
            this.numCount.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            this.numCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(119, 38);
            this.numCount.TabIndex = 14;
            this.numCount.Value = new decimal(new int[] { 120, 0, 0, 0 });
            //
            // numValue
            //
            this.numValue.Location = new System.Drawing.Point(366, 406);
            this.numValue.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            this.numValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(119, 38);
            this.numValue.TabIndex = 13;
            this.numValue.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numValue.ValueChanged += new System.EventHandler(this.RangeInput_Changed);
            //
            // label6
            //
            this.label6.Location = new System.Drawing.Point(338, 487);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 27);
            this.label6.TabIndex = 12;
            this.label6.Values.Text = "测量点数量：";
            //
            // label5
            //
            this.label5.Location = new System.Drawing.Point(533, 412);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 32);
            this.label5.TabIndex = 11;
            this.label5.Values.Text = "X";
            //
            // cmbUnit
            //
            this.cmbUnit.Items.AddRange(new object[] { "1秒", "1分钟", "1小时", "1天" });
            this.cmbUnit.Location = new System.Drawing.Point(595, 411);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(245, 35);
            this.cmbUnit.TabIndex = 10;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.RangeInput_Changed);
            //
            // label4
            //
            this.label4.Location = new System.Drawing.Point(338, 361);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 27);
            this.label4.TabIndex = 8;
            this.label4.Values.Text = "时间范围：";
            //
            // label3
            //
            this.label3.Location = new System.Drawing.Point(338, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 27);
            this.label3.TabIndex = 7;
            this.label3.Values.Text = "结束时间：";
            //
            // dtpEndTime
            //
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(595, 273);
            this.dtpEndTime.MaximumSize = new System.Drawing.Size(500, 500);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(232, 38);
            this.dtpEndTime.TabIndex = 6;
            //
            // dtpEndDate
            //
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(343, 273);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(232, 38);
            this.dtpEndDate.TabIndex = 5;
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(338, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 27);
            this.label2.TabIndex = 4;
            this.label2.Values.Text = "开始时间：";
            //
            // dtpStartTime
            //
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(595, 150);
            this.dtpStartTime.MaximumSize = new System.Drawing.Size(500, 500);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(232, 38);
            this.dtpStartTime.TabIndex = 3;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.RangeInput_Changed);
            //
            // dtpStartDate
            //
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(343, 150);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(232, 38);
            this.dtpStartDate.TabIndex = 2;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.RangeInput_Changed);
            //
            // cmbMode
            //
            this.cmbMode.Items.AddRange(new object[] {
                "1 - 时间范围",
                "2 - 开始到结束的时间",
                "3 - 测量点数量",
                "4 - 自定义聚类"});
            this.cmbMode.Location = new System.Drawing.Point(11, 153);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(261, 35);
            this.cmbMode.TabIndex = 1;
            this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.CmbMode_SelectedIndexChanged);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(6, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 27);
            this.label1.TabIndex = 0;
            this.label1.Values.Text = "设置：";
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(452, 604);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 57);
            this.btnOK.TabIndex = 1;
            this.btnOK.Values.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(636, 604);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 57);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // btnApply
            //
            this.btnApply.Location = new System.Drawing.Point(820, 604);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(140, 57);
            this.btnApply.TabIndex = 3;
            this.btnApply.Values.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            //
            // TimeRangeSelectorPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 687);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Name = "TimeRangeSelectorPanel";
            this.Text = "TimeRangeSelectorPanel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMode)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private Krypton.Toolkit.KryptonComboBox cmbMode;
        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label3;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonLabel label4;
        private Krypton.Toolkit.KryptonLabel label6;
        private Krypton.Toolkit.KryptonLabel label5;
        private Krypton.Toolkit.KryptonComboBox cmbUnit;
        private Krypton.Toolkit.KryptonButton btnOK;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnApply;
        private Krypton.Toolkit.KryptonNumericUpDown numCount;
        private Krypton.Toolkit.KryptonNumericUpDown numValue;
    }
}
