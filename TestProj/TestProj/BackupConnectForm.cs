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
        /// 若用户选中了具体的备份文件，则为该文件解析出的开始时间（本地时间）；
        /// 否则为 null（表示连接文件夹下所有归档）。
        /// </summary>
        public DateTime? SelectedTimeFrom { get; private set; }

        /// <summary>
        /// 若用户选中了具体的备份文件，则为该文件解析出的结束时间（本地时间）；
        /// 否则为 null。
        /// </summary>
        public DateTime? SelectedTimeTo { get; private set; }

        private List<BackupFileInfo> _backupFiles = new List<BackupFileInfo>();

        public BackupConnectForm(string initialFolder = "")
        {
            InitializeComponent();
            this.Text             = LangHelper.Get("Backup_ConnectTitle");
            lblTitle.Values.Text  = LangHelper.Get("Backup_ConnectTitle");
            lblFolder.Values.Text = LangHelper.Get("Backup_FolderLabel");
            btnBrowse.Values.Text     = "...";
            btnBrowseFile.Values.Text = LangHelper.Get("Backup_BrowseFile");
            btnOK.Values.Text         = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text     = LangHelper.Get("Msg_Cancel");

            if (!string.IsNullOrWhiteSpace(initialFolder))
            {
                txtFolder.Text = initialFolder;
                LoadBackupFiles(initialFolder);
            }
        }

        /// <summary>
        /// 直接选择单个备份文件（.ldf / .mdf）。
        /// 选中后自动填入文件夹路径并在列表中定位到该文件，
        /// 用户确认时只连接该文件对应的时间范围。
        /// </summary>
        private void BtnBrowseFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title  = LangHelper.Get("Backup_BrowseFile");
                dlg.Filter = "WinCC 备份文件 (*.ldf;*.mdf)|*.ldf;*.mdf|所有文件 (*.*)|*.*";

                // 以当前文件夹为初始目录
                string current = txtFolder.Text.Trim();
                if (Directory.Exists(current))
                    dlg.InitialDirectory = current;

                if (dlg.ShowDialog() != DialogResult.OK) return;

                string filePath  = dlg.FileName;
                string folder    = Path.GetDirectoryName(filePath);
                string fileNameNoExt = Path.GetFileNameWithoutExtension(filePath);

                // 填入文件夹并重新加载列表
                txtFolder.Text = folder;
                LoadBackupFiles(folder);

                // 在列表中自动定位到用户选中的那个文件
                for (int i = 0; i < _backupFiles.Count; i++)
                {
                    if (string.Equals(_backupFiles[i].Name, fileNameNoExt,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        lstBackups.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        // 浏览文件夹路径
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

        // 加载指定文件夹下的备份文件，更新列表显示
        private void LoadBackupFiles(string folder)
        {
            _backupFiles = BackupManager.GetBackupFilesInFolder(folder);
            lstBackups.Items.Clear();
            foreach (var info in _backupFiles)
                lstBackups.Items.Add(info); // uses BackupFileInfo.ToString()

            // 若有文件则预选第一项，方便用户
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

            // 若用户选中了某个文件，提取其时间范围
            if (lstBackups.SelectedIndex >= 0 &&
                lstBackups.SelectedIndex < _backupFiles.Count)
            {
                var selected = _backupFiles[lstBackups.SelectedIndex];
                if (selected.TimeRange.HasValue)
                {
                    // 文件名中的时间已是 UTC（WinCC 备份文件名用 UTC），直接传给 Restore
                    SelectedTimeFrom = selected.TimeRange.Value.Start;
                    SelectedTimeTo   = selected.TimeRange.Value.End;
                }
            }
            // SelectedTimeFrom/To 为 null 时，ConnectBackup 会连接文件夹下所有归档

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
