namespace TestProj
{
    partial class ExcelImportConfigForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numStartRow = new System.Windows.Forms.NumericUpDown();
            this.numTagCol = new System.Windows.Forms.NumericUpDown();
            this.numDisplayCol = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numStartRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTagCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayCol)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 14F);
            this.label1.Location = new System.Drawing.Point(35, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据起始行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 14F);
            this.label2.Location = new System.Drawing.Point(35, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 42);
            this.label2.TabIndex = 1;
            this.label2.Text = "变量名列号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 14F);
            this.label3.Location = new System.Drawing.Point(35, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 42);
            this.label3.TabIndex = 2;
            this.label3.Text = "显示名列号";
            // 
            // numStartRow
            // 
            this.numStartRow.Location = new System.Drawing.Point(314, 74);
            this.numStartRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartRow.Name = "numStartRow";
            this.numStartRow.Size = new System.Drawing.Size(175, 38);
            this.numStartRow.TabIndex = 3;
            this.numStartRow.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numTagCol
            // 
            this.numTagCol.Location = new System.Drawing.Point(314, 172);
            this.numTagCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTagCol.Name = "numTagCol";
            this.numTagCol.Size = new System.Drawing.Size(175, 38);
            this.numTagCol.TabIndex = 4;
            this.numTagCol.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numDisplayCol
            // 
            this.numDisplayCol.Location = new System.Drawing.Point(314, 272);
            this.numDisplayCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDisplayCol.Name = "numDisplayCol";
            this.numDisplayCol.Size = new System.Drawing.Size(175, 38);
            this.numDisplayCol.TabIndex = 5;
            this.numDisplayCol.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(52, 413);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(198, 65);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(417, 413);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(198, 65);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.numStartRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTagCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDisplayCol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numStartRow;
        private System.Windows.Forms.NumericUpDown numTagCol;
        private System.Windows.Forms.NumericUpDown numDisplayCol;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
