namespace TestProj
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
            this.lblNewPwd     = new Krypton.Toolkit.KryptonLabel();
            this.txtNewPassword = new Krypton.Toolkit.KryptonTextBox();
            this.lblConfirm    = new Krypton.Toolkit.KryptonLabel();
            this.txtConfirm    = new Krypton.Toolkit.KryptonTextBox();
            this.lblNote       = new Krypton.Toolkit.KryptonLabel();
            this.btnOK         = new Krypton.Toolkit.KryptonButton();
            this.btnCancel     = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            //
            // lblNewPwd
            //
            this.lblNewPwd.Location = new System.Drawing.Point(30, 30);
            this.lblNewPwd.Name = "lblNewPwd";
            this.lblNewPwd.Size = new System.Drawing.Size(160, 40);
            this.lblNewPwd.Values.Text = "新密码：";
            //
            // txtNewPassword
            //
            this.txtNewPassword.Location = new System.Drawing.Point(200, 30);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(230, 40);
            this.txtNewPassword.UseSystemPasswordChar = true;
            //
            // lblConfirm
            //
            this.lblConfirm.Location = new System.Drawing.Point(30, 90);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(160, 40);
            this.lblConfirm.Values.Text = "确认密码：";
            //
            // txtConfirm
            //
            this.txtConfirm.Location = new System.Drawing.Point(200, 90);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(230, 40);
            this.txtConfirm.UseSystemPasswordChar = true;
            //
            // lblNote
            //
            this.lblNote.Location = new System.Drawing.Point(30, 148);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(400, 36);
            this.lblNote.StateCommon.ShortText.Color1 = System.Drawing.Color.Gray;
            this.lblNote.Values.Text = "（留空并确认可清除密码）";
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(130, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 48);
            this.btnOK.TabIndex = 1;
            this.btnOK.Values.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(270, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 48);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // ChangePasswordForm
            //
            this.AcceptButton    = this.btnOK;
            this.CancelButton    = this.btnCancel;
            this.ClientSize      = new System.Drawing.Size(460, 280);
            this.Controls.Add(this.lblNewPwd);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "ChangePasswordForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
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
