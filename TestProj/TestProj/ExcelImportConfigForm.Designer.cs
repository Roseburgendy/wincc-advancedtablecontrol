namespace TestProj
{
    partial class ExcelImportConfigForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.numStartRow = new Krypton.Toolkit.KryptonNumericUpDown();
            this.numTagCol = new Krypton.Toolkit.KryptonNumericUpDown();
            this.numDisplayCol = new Krypton.Toolkit.KryptonNumericUpDown();
            this.numDisplayEnCol = new Krypton.Toolkit.KryptonNumericUpDown();
            this.btnOK = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 42);
            this.label1.TabIndex = 0;
            this.label1.Values.Text = "数据起始行";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(29, 139);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 42);
            this.label2.TabIndex = 1;
            this.label2.Values.Text = "变量名列号";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(29, 221);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 42);
            this.label3.TabIndex = 2;
            this.label3.Values.Text = "中文显示名列号";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(29, 303);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(307, 42);
            this.label4.TabIndex = 6;
            this.label4.Values.Text = "英文显示名列号 (0=无)";
            // 
            // numStartRow
            // 
            this.numStartRow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numStartRow.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartRow.Location = new System.Drawing.Point(399, 59);
            this.numStartRow.Margin = new System.Windows.Forms.Padding(2);
            this.numStartRow.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numStartRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartRow.Name = "numStartRow";
            this.numStartRow.Size = new System.Drawing.Size(352, 42);
            this.numStartRow.TabIndex = 3;
            this.numStartRow.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numTagCol
            // 
            this.numTagCol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numTagCol.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTagCol.Location = new System.Drawing.Point(399, 139);
            this.numTagCol.Margin = new System.Windows.Forms.Padding(2);
            this.numTagCol.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTagCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTagCol.Name = "numTagCol";
            this.numTagCol.Size = new System.Drawing.Size(352, 42);
            this.numTagCol.TabIndex = 4;
            this.numTagCol.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numDisplayCol
            // 
            this.numDisplayCol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numDisplayCol.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDisplayCol.Location = new System.Drawing.Point(399, 221);
            this.numDisplayCol.Margin = new System.Windows.Forms.Padding(2);
            this.numDisplayCol.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDisplayCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDisplayCol.Name = "numDisplayCol";
            this.numDisplayCol.Size = new System.Drawing.Size(352, 42);
            this.numDisplayCol.TabIndex = 5;
            this.numDisplayCol.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numDisplayEnCol
            // 
            this.numDisplayEnCol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numDisplayEnCol.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDisplayEnCol.Location = new System.Drawing.Point(399, 302);
            this.numDisplayEnCol.Margin = new System.Windows.Forms.Padding(2);
            this.numDisplayEnCol.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDisplayEnCol.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDisplayEnCol.Name = "numDisplayEnCol";
            this.numDisplayEnCol.Size = new System.Drawing.Size(352, 42);
            this.numDisplayEnCol.TabIndex = 6;
            this.numDisplayEnCol.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(43, 393);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(163, 53);
            this.btnOK.TabIndex = 7;
            this.btnOK.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOK.Values.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(551, 393);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(163, 53);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ExcelImportConfigForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(783, 475);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numDisplayEnCol);
            this.Controls.Add(this.numDisplayCol);
            this.Controls.Add(this.numTagCol);
            this.Controls.Add(this.numStartRow);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(580, 522);
            this.Name = "ExcelImportConfigForm";
            this.Text = "导入配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonLabel label4;
        private Krypton.Toolkit.KryptonNumericUpDown numStartRow;
        private Krypton.Toolkit.KryptonNumericUpDown numTagCol;
        private Krypton.Toolkit.KryptonNumericUpDown numDisplayCol;
        private Krypton.Toolkit.KryptonNumericUpDown numDisplayEnCol;
        private Krypton.Toolkit.KryptonButton btnOK;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
