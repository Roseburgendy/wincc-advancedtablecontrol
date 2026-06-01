using System.Windows.Forms;

namespace TestProj
{
    /// <summary>
    /// 选择并断开 WinCC 备份归档的对话框。
    /// 列表直接显示 BackupManager.ConnectedArchives 中记录的已连接条目。
    /// </summary>
    public partial class BackupDisconnectForm : Krypton.Toolkit.KryptonForm
    {
        /// <summary>用户选中要断开的归档条目，OK 后有效。传给 BackupManager.DisconnectBackup。</summary>
        public ConnectedArchiveEntry SelectedEntry { get; private set; }

        public BackupDisconnectForm()
        {
            InitializeComponent();
            this.Text             = LangHelper.Get("Backup_DisconnectTitle");
            lblTitle.Values.Text  = LangHelper.Get("Backup_DisconnectTitle");
            btnOK.Values.Text     = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text = LangHelper.Get("Msg_Cancel");

            // 直接显示已记录的已连接归档条目（ConnectBackup 时已解析好文件名和时间范围）
            foreach (var entry in BackupManager.ConnectedArchives)
                lstBackups.Items.Add(entry.DisplayName);

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
            SelectedEntry = BackupManager.ConnectedArchives[lstBackups.SelectedIndex];
            DialogResult  = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
