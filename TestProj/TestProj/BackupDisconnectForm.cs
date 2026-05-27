using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestProj
{
    /// <summary>
    /// 选择并断开 WinCC 备份归档的对话框（仿 WinCC Online Table Control 样式）。
    /// 使用 HMIRuntime.Logging.DataLogs.Remove 断开。
    /// 列表显示具体备份文件名及时间范围；断开操作以文件夹为粒度（WinCC Remove 接口限制）。
    /// </summary>
    public partial class BackupDisconnectForm : Krypton.Toolkit.KryptonForm
    {
        /// <summary>用户选中项所在的备份文件夹路径，OK 后有效（传给 BackupManager.DisconnectBackup）。</summary>
        public string SelectedFolder { get; private set; }

        // 列表项索引 → 对应文件夹路径（WinCC Remove 以文件夹为粒度）
        private readonly List<string> _folderForIndex = new List<string>();

        public BackupDisconnectForm()
        {
            InitializeComponent();
            this.Text             = LangHelper.Get("Backup_DisconnectTitle");
            lblTitle.Values.Text  = LangHelper.Get("Backup_DisconnectTitle");
            btnOK.Values.Text     = LangHelper.Get("Msg_OK");
            btnCancel.Values.Text = LangHelper.Get("Msg_Cancel");

            // 对每个已连接的文件夹，扫描其中的备份文件并逐项列出
            foreach (string folder in BackupManager.ConnectedFolders)
            {
                var files = BackupManager.GetBackupFilesInFolder(folder);
                if (files.Count > 0)
                {
                    foreach (var fi in files)
                    {
                        // BackupFileInfo.ToString() 已含文件名 + 本地时间范围
                        lstBackups.Items.Add(fi.ToString());
                        _folderForIndex.Add(folder);
                    }
                }
                else
                {
                    // 文件夹下无可识别的备份文件，退而显示文件夹路径
                    lstBackups.Items.Add(folder);
                    _folderForIndex.Add(folder);
                }
            }

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
            // 取该列表项对应的文件夹路径（而非文件名字符串）
            SelectedFolder = _folderForIndex[lstBackups.SelectedIndex];
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
