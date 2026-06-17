namespace TestProj
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPrompt   = new Krypton.Toolkit.KryptonLabel();
            this.lblHint     = new Krypton.Toolkit.KryptonLabel();
            this.txtPassword = new Krypton.Toolkit.KryptonTextBox();
            this.btnOK       = new Krypton.Toolkit.KryptonButton();
            this.btnCancel   = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            //
            // lblPrompt
            //
            this.lblPrompt.Location = new System.Drawing.Point(30, 30);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(400, 40);
            this.lblPrompt.Values.Text = "请输入管理员密码：";
            //
            // txtPassword
            //
            this.txtPassword.Location = new System.Drawing.Point(30, 80);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(400, 40);
            this.txtPassword.UseSystemPasswordChar = true;
            //
            // lblHint
            //
            this.lblHint.Location = new System.Drawing.Point(30, 132);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(400, 40);
            this.lblHint.StateCommon.ShortText.Color1 = System.Drawing.Color.Gray;
            this.lblHint.Values.Text = "";
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(140, 192);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 48);
            this.btnOK.TabIndex = 1;
            this.btnOK.Values.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(280, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 48);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // LoginForm
            //
            this.AcceptButton    = this.btnOK;
            this.CancelButton    = this.btnCancel;
            this.ClientSize      = new System.Drawing.Size(460, 270);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "LoginForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Krypton.Toolkit.KryptonLabel   lblPrompt;
        private Krypton.Toolkit.KryptonLabel   lblHint;
        private Krypton.Toolkit.KryptonTextBox txtPassword;
        private Krypton.Toolkit.KryptonButton  btnOK;
        private Krypton.Toolkit.KryptonButton  btnCancel;
    }
}
