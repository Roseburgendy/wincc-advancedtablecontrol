using System;
using System.Windows.Forms;

namespace TestProj
{
    public partial class LoginForm : Krypton.Toolkit.KryptonForm
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Text             = LangHelper.Get("Auth_LoginTitle");
            lblPrompt.Values.Text = LangHelper.Get("Auth_LoginPrompt");
            btnOK.Values.Text     = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text = LangHelper.Get("Msg_Cancel");

            lblHint.Values.Text = AuthManager.HasPassword
                ? ""
                : LangHelper.Get("Auth_NoPasswordHint");
            lblHint.Visible = !AuthManager.HasPassword;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (AuthManager.Login(txtPassword.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, LangHelper.Get("Auth_LoginFailed"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
