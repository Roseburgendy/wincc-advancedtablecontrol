using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WINCCReporter
{
    /// <summary>
    /// 选择并断开 WinCC 备份归档的对话框。
    /// 列表直接显示 BackupManager.ConnectedArchives 中记录的已连接条目，支持多选。
    /// </summary>
    public partial class BackupDisconnectForm : Krypton.Toolkit.KryptonForm
    {
        /// <summary>用户选中要断开的归档条目列表，OK 后有效。</summary>
        public List<ConnectedArchiveEntry> SelectedEntries { get; private set; } = new List<ConnectedArchiveEntry>();

        public BackupDisconnectForm()
        {
            InitializeComponent();
            this.Text             = LangHelper.Get("Backup_DisconnectTitle");
            lblTitle.Values.Text  = LangHelper.Get("Backup_DisconnectTitle");
            btnOK.Values.Text     = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text = LangHelper.Get("Msg_Cancel");

            foreach (var entry in BackupManager.ConnectedArchives)
                lstBackups.Items.Add(entry.DisplayName);

            btnOK.Enabled = lstBackups.Items.Count > 0;

            if (lstBackups.Items.Count > 0)
                lstBackups.SelectedIndex = 0;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (lstBackups.SelectedIndices.Count == 0)
            {
                MessageBox.Show(LangHelper.Get("Backup_NoDisconnectSelected"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedEntries = new List<ConnectedArchiveEntry>();
            foreach (int idx in lstBackups.SelectedIndices)
                SelectedEntries.Add(BackupManager.ConnectedArchives[idx]);

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
