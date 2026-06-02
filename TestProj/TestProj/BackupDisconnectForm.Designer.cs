namespace TestProj
{
    partial class BackupDisconnectForm
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
            this.lblTitle = new Krypton.Toolkit.KryptonLabel();
            this.lstBackups = new Krypton.Toolkit.KryptonListBox();
            this.btnOK = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(31, 24);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(996, 57);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Values.Text = "选择想要断开的备份。";
            // 
            // lstBackups
            // 
            this.lstBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBackups.Location = new System.Drawing.Point(31, 103);
            this.lstBackups.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lstBackups.Name = "lstBackups";
            this.lstBackups.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lstBackups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBackups.Size = new System.Drawing.Size(1027, 405);
            this.lstBackups.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(591, 535);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(218, 68);
            this.btnOK.TabIndex = 2;
            this.btnOK.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOK.Values.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(840, 535);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(218, 68);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BackupDisconnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 624);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstBackups);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MinimumSize = new System.Drawing.Size(918, 539);
            this.Name = "BackupDisconnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择想要断开的备份。";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel  lblTitle;
        private Krypton.Toolkit.KryptonListBox lstBackups;
        private Krypton.Toolkit.KryptonButton  btnOK;
        private Krypton.Toolkit.KryptonButton  btnCancel;
    }
}
