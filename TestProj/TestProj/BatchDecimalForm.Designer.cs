namespace WINCCReporter
{
    partial class BatchDecimalForm
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
            this.lblHint = new Krypton.Toolkit.KryptonLabel();
            this.dgvRules = new System.Windows.Forms.DataGridView();
            this.btnAddRule = new Krypton.Toolkit.KryptonButton();
            this.btnDeleteRule = new Krypton.Toolkit.KryptonButton();
            this.lblApplyResult = new Krypton.Toolkit.KryptonLabel();
            this.btnOK = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHint
            // 
            this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHint.Location = new System.Drawing.Point(15, 11);
            this.lblHint.Margin = new System.Windows.Forms.Padding(2);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(1300, 56);
            this.lblHint.TabIndex = 0;
            this.lblHint.Values.Text = "按 TagName 前缀批量设置精确位数。规则从上到下依次匹配，命中第一条即生效。";
            // 
            // dgvRules
            // 
            this.dgvRules.AllowUserToAddRows = false;
            this.dgvRules.AllowUserToDeleteRows = false;
            this.dgvRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRules.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvRules.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRules.Location = new System.Drawing.Point(15, 83);
            this.dgvRules.Margin = new System.Windows.Forms.Padding(2);
            this.dgvRules.Name = "dgvRules";
            this.dgvRules.RowHeadersWidth = 40;
            this.dgvRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRules.Size = new System.Drawing.Size(1345, 466);
            this.dgvRules.TabIndex = 1;
            // 
            // btnAddRule
            // 
            this.btnAddRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddRule.Location = new System.Drawing.Point(15, 559);
            this.btnAddRule.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(235, 56);
            this.btnAddRule.TabIndex = 2;
            this.btnAddRule.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddRule.Values.Text = "+ 新增规则";
            this.btnAddRule.Click += new System.EventHandler(this.BtnAddRule_Click);
            // 
            // btnDeleteRule
            // 
            this.btnDeleteRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteRule.Location = new System.Drawing.Point(299, 559);
            this.btnDeleteRule.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Size = new System.Drawing.Size(235, 56);
            this.btnDeleteRule.TabIndex = 3;
            this.btnDeleteRule.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDeleteRule.Values.Text = "- 删除选中";
            this.btnDeleteRule.Click += new System.EventHandler(this.BtnDeleteRule_Click);
            // 
            // lblApplyResult
            // 
            this.lblApplyResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApplyResult.Location = new System.Drawing.Point(922, 571);
            this.lblApplyResult.Margin = new System.Windows.Forms.Padding(2);
            this.lblApplyResult.Name = "lblApplyResult";
            this.lblApplyResult.Size = new System.Drawing.Size(438, 44);
            this.lblApplyResult.TabIndex = 4;
            this.lblApplyResult.Values.Text = "";
            this.lblApplyResult.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(975, 629);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(195, 60);
            this.btnOK.TabIndex = 5;
            this.btnOK.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOK.Values.Text = "保存并应用";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1185, 629);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(175, 60);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BatchDecimalForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1375, 706);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.dgvRules);
            this.Controls.Add(this.btnAddRule);
            this.Controls.Add(this.btnDeleteRule);
            this.Controls.Add(this.lblApplyResult);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(700, 580);
            this.Name = "BatchDecimalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "批量精确位数规则";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel  lblHint;
        private System.Windows.Forms.DataGridView dgvRules;
        private Krypton.Toolkit.KryptonButton btnAddRule;
        private Krypton.Toolkit.KryptonButton btnDeleteRule;
        private Krypton.Toolkit.KryptonLabel  lblApplyResult;
        private Krypton.Toolkit.KryptonButton btnOK;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
