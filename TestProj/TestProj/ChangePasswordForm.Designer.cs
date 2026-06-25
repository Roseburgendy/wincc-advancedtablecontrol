namespace WINCCReporter
{
    partial class ChangePasswordForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNewPwd = new Krypton.Toolkit.KryptonLabel();
            this.txtNewPassword = new Krypton.Toolkit.KryptonTextBox();
            this.lblConfirm = new Krypton.Toolkit.KryptonLabel();
            this.txtConfirm = new Krypton.Toolkit.KryptonTextBox();
            this.lblNote = new Krypton.Toolkit.KryptonLabel();
            this.btnOK = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // lblNewPwd
            // 
            this.lblNewPwd.Location = new System.Drawing.Point(30, 30);
            this.lblNewPwd.Name = "lblNewPwd";
            this.lblNewPwd.Size = new System.Drawing.Size(200, 42);
            this.lblNewPwd.TabIndex = 0;
            this.lblNewPwd.Values.Text = "新密码：";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(282, 29);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.Size = new System.Drawing.Size(230, 43);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirm
            // 
            this.lblConfirm.Location = new System.Drawing.Point(30, 90);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(200, 42);
            this.lblConfirm.TabIndex = 2;
            this.lblConfirm.Values.Text = "确认密码：";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(282, 89);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.Size = new System.Drawing.Size(230, 43);
            this.txtConfirm.TabIndex = 3;
            this.txtConfirm.UseSystemPasswordChar = true;
            // 
            // lblNote
            // 
            this.lblNote.Location = new System.Drawing.Point(30, 162);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(500, 42);
            this.lblNote.StateCommon.ShortText.Color1 = System.Drawing.Color.Gray;
            this.lblNote.TabIndex = 4;
            this.lblNote.Values.Text = "（留空并确认可清除密码）";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(142, 245);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 48);
            this.btnOK.TabIndex = 1;
            this.btnOK.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOK.Values.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(282, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 48);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ChangePasswordForm
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(571, 315);
            this.Controls.Add(this.lblNewPwd);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Krypton.Toolkit.KryptonLabel   lblNewPwd;
        private Krypton.Toolkit.KryptonTextBox txtNewPassword;
        private Krypton.Toolkit.KryptonLabel   lblConfirm;
        private Krypton.Toolkit.KryptonTextBox txtConfirm;
        private Krypton.Toolkit.KryptonLabel   lblNote;
        private Krypton.Toolkit.KryptonButton  btnOK;
        private Krypton.Toolkit.KryptonButton  btnCancel;
    }
}
