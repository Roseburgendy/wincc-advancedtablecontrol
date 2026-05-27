using System;
using System.Windows.Forms;

namespace TestProj
{
    /// <summary>
    /// 选择并断开 WinCC 备份归档的对话框（仿 WinCC Online Table Control 样式）。
    /// 使用 HMIRuntime.Logging.DataLogs.Remove 断开。
    /// </summary>
    public partial class BackupDisconnectForm : Krypton.Toolkit.KryptonForm
    {
        /// <summary>用户选中要断开的备份文件夹路径，OK 后有效。</summary>
        public string SelectedFolder { get; private set; }

        public BackupDisconnectForm()
        {
            InitializeComponent();
            this.Text             = LangHelper.Get("Backup_DisconnectTitle");
            lblTitle.Values.Text  = LangHelper.Get("Backup_DisconnectTitle");
            btnOK.Values.Text     = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text = LangHelper.Get("Msg_Cancel");

            // 显示所有已通过 BackupManager.ConnectBackup 连接的备份文件夹
            foreach (string folder in BackupManager.ConnectedFolders)
                lstBackups.Items.Add(folder);

            btnOK.Enabled = lstBackups.Items.Count > 0;

            if (lstBackups.Items.Count > 0)
                lstBackups.SelectedIndex = 0;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (lstBackups.SelectedIndex < 0)
            {
                MessageBox.Show(LangHelper.Get("Backup_NoDisconnectSelected"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SelectedFolder = lstBackups.SelectedItem?.ToString();
            DialogResult   = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
