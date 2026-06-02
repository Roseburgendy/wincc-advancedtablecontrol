using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TestProj
{
    public partial class BackupConnectForm : Krypton.Toolkit.KryptonForm
    {
        /// <summary>用户选中的备份文件夹路径，OK 后有效。</summary>
        public string SelectedFolder { get; private set; }

        /// <summary>
        /// 用户在列表中选中的备份文件集合，OK 后有效。
        /// 空列表表示未选中具体文件，BackupProgressForm 会连接文件夹下所有归档。
        /// </summary>
        public List<BackupFileInfo> SelectedFiles { get; private set; } = new List<BackupFileInfo>();

        private List<BackupFileInfo> _backupFiles = new List<BackupFileInfo>();

        public BackupConnectForm(string initialFolder = "")
        {
            InitializeComponent();
            this.Text             = LangHelper.Get("Backup_ConnectTitle");
            lblTitle.Values.Text  = LangHelper.Get("Backup_ConnectTitle");
            lblFolder.Values.Text = LangHelper.Get("Backup_FolderLabel");
            btnBrowse.Values.Text = "...";
            btnOK.Values.Text     = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text = LangHelper.Get("Msg_Cancel");

            if (!string.IsNullOrWhiteSpace(initialFolder))
            {
                txtFolder.Text = initialFolder;
                LoadBackupFiles(initialFolder);
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = LangHelper.Get("Backup_FolderBrowseDesc");
                string current = txtFolder.Text.Trim();
                if (Directory.Exists(current))
                    dlg.SelectedPath = current;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dlg.SelectedPath;
                    LoadBackupFiles(dlg.SelectedPath);
                }
            }
        }

        private void TxtFolder_Leave(object sender, EventArgs e)
        {
            string folder = txtFolder.Text.Trim();
            if (Directory.Exists(folder))
                LoadBackupFiles(folder);
        }

        private void LoadBackupFiles(string folder)
        {
            _backupFiles = BackupManager.GetBackupFilesInFolder(folder);
            lstBackups.Items.Clear();
            foreach (var info in _backupFiles)
                lstBackups.Items.Add(info);

            if (lstBackups.Items.Count > 0)
                lstBackups.SelectedIndex = 0;

            btnOK.Enabled = Directory.Exists(folder);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            string folder = txtFolder.Text.Trim();
            if (!Directory.Exists(folder))
            {
                MessageBox.Show(LangHelper.Get("Backup_FolderNotFound"), LangHelper.Get("Msg_Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedFolder = folder;

            // 收集所有选中的文件（可多选）
            SelectedFiles = new List<BackupFileInfo>();
            foreach (int idx in lstBackups.SelectedIndices)
            {
                if (idx < _backupFiles.Count)
                    SelectedFiles.Add(_backupFiles[idx]);
            }
            // SelectedFiles 为空时，BackupProgressForm 会连接文件夹下所有归档

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
