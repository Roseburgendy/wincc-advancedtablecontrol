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
            this.label1        = new Krypton.Toolkit.KryptonLabel();
            this.label2        = new Krypton.Toolkit.KryptonLabel();
            this.label3        = new Krypton.Toolkit.KryptonLabel();
            this.numStartRow   = new Krypton.Toolkit.KryptonNumericUpDown();
            this.numTagCol     = new Krypton.Toolkit.KryptonNumericUpDown();
            this.numDisplayCol = new Krypton.Toolkit.KryptonNumericUpDown();
            this.btnOK         = new Krypton.Toolkit.KryptonButton();
            this.btnCancel     = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.Font = new System.Drawing.Font("SimSun", 14F);
            this.label1.Location = new System.Drawing.Point(35, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 42);
            this.label1.TabIndex = 0;
            this.label1.Values.Text = "数据起始行";
            //
            // label2
            //
            this.label2.Font = new System.Drawing.Font("SimSun", 14F);
            this.label2.Location = new System.Drawing.Point(35, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 42);
            this.label2.TabIndex = 1;
            this.label2.Values.Text = "变量名列号";
            //
            // label3
            //
            this.label3.Font = new System.Drawing.Font("SimSun", 14F);
            this.label3.Location = new System.Drawing.Point(35, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 42);
            this.label3.TabIndex = 2;
            this.label3.Values.Text = "显示名列号";
            //
            // numStartRow
            //
            this.numStartRow.Location = new System.Drawing.Point(314, 74);
            this.numStartRow.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numStartRow.Name = "numStartRow";
            this.numStartRow.Size = new System.Drawing.Size(175, 38);
            this.numStartRow.TabIndex = 3;
            this.numStartRow.Value = new decimal(new int[] { 2, 0, 0, 0 });
            //
            // numTagCol
            //
            this.numTagCol.Location = new System.Drawing.Point(314, 172);
            this.numTagCol.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numTagCol.Name = "numTagCol";
            this.numTagCol.Size = new System.Drawing.Size(175, 38);
            this.numTagCol.TabIndex = 4;
            this.numTagCol.Value = new decimal(new int[] { 2, 0, 0, 0 });
            //
            // numDisplayCol
            //
            this.numDisplayCol.Location = new System.Drawing.Point(314, 272);
            this.numDisplayCol.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numDisplayCol.Name = "numDisplayCol";
            this.numDisplayCol.Size = new System.Drawing.Size(175, 38);
            this.numDisplayCol.TabIndex = 5;
            this.numDisplayCol.Value = new decimal(new int[] { 3, 0, 0, 0 });
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(52, 413);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(198, 65);
            this.btnOK.TabIndex = 7;
            this.btnOK.Values.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.BtnOk_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(417, 413);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(198, 65);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // ExcelImportConfigForm
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(698, 516);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numDisplayCol);
            this.Controls.Add(this.numTagCol);
            this.Controls.Add(this.numStartRow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ExcelImportConfigForm";
            this.Text = "导入配置";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonNumericUpDown numStartRow;
        private Krypton.Toolkit.KryptonNumericUpDown numTagCol;
        private Krypton.Toolkit.KryptonNumericUpDown numDisplayCol;
        private Krypton.Toolkit.KryptonButton btnOK;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
