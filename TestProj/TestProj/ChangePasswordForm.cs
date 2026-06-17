using System;
using System.Windows.Forms;

namespace TestProj
{
    public partial class ChangePasswordForm : Krypton.Toolkit.KryptonForm
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
            this.Text              = LangHelper.Get("Auth_ChangePasswordTitle");
            lblNewPwd.Values.Text  = LangHelper.Get("Auth_NewPassword");
            lblConfirm.Values.Text = LangHelper.Get("Auth_ConfirmPassword");
            lblNote.Values.Text    = LangHelper.Get("Auth_PasswordNote");
            btnOK.Values.Text      = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text  = LangHelper.Get("Msg_Cancel");
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            string newPwd     = txtNewPassword.Text;
            string confirmPwd = txtConfirm.Text;

            if (newPwd != confirmPwd)
            {
                MessageBox.Show(this, LangHelper.Get("Auth_PasswordMismatch"), 
                    LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtConfirm.Clear();
                txtConfirm.Focus();
                return;
            }

            AuthManager.SetPassword(newPwd);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
