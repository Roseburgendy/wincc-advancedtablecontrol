namespace TestProj
{
    partial class BackupConnectForm
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
            this.lblTitle   = new Krypton.Toolkit.KryptonLabel();
            this.lblFolder  = new Krypton.Toolkit.KryptonLabel();
            this.txtFolder  = new Krypton.Toolkit.KryptonTextBox();
            this.btnBrowse  = new Krypton.Toolkit.KryptonButton();
            this.lstBackups = new Krypton.Toolkit.KryptonListBox();
            this.btnOK      = new Krypton.Toolkit.KryptonButton();
            this.btnCancel  = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(760, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Values.Text = "选择想要连接的备份。";
            //
            // lblFolder
            //
            this.lblFolder.Location = new System.Drawing.Point(20, 76);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(180, 42);
            this.lblFolder.TabIndex = 1;
            this.lblFolder.Values.Text = "备份文件夹：";
            //
            // txtFolder
            //
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(210, 76);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(580, 43);
            this.txtFolder.TabIndex = 2;
            this.txtFolder.Leave += new System.EventHandler(this.TxtFolder_Leave);
            //
            // btnBrowse
            //
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(800, 74);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(80, 46);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnBrowse.Values.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            //
            // lstBackups
            //
            this.lstBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBackups.Location = new System.Drawing.Point(20, 138);
            this.lstBackups.Name = "lstBackups";
            this.lstBackups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBackups.Size = new System.Drawing.Size(860, 310);
            this.lstBackups.TabIndex = 4;
            //
            // btnOK
            //
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(560, 466);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 52);
            this.btnOK.TabIndex = 5;
            this.btnOK.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOK.Values.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(730, 466);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 52);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // BackupConnectForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lstBackups);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "BackupConnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择想要连接的备份。";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonLabel  lblTitle;
        private Krypton.Toolkit.KryptonLabel  lblFolder;
        private Krypton.Toolkit.KryptonTextBox txtFolder;
        private Krypton.Toolkit.KryptonButton  btnBrowse;
        private Krypton.Toolkit.KryptonListBox lstBackups;
        private Krypton.Toolkit.KryptonButton  btnOK;
        private Krypton.Toolkit.KryptonButton  btnCancel;
    }
}
