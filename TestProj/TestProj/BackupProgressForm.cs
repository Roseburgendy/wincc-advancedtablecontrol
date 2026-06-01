using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProj
{
    /// <summary>
    /// 连接备份归档的进度与结果对话框。
    /// 打开时自动在后台 STA 线程上调用 BackupManager.ConnectBackup，
    /// 通过 Marquee 进度条指示连接中，完成后逐行列出每个文件的连接结果。
    /// </summary>
    public partial class BackupProgressForm : Krypton.Toolkit.KryptonForm
    {
        private readonly string    _folder;
        private readonly DateTime? _timeFrom;
        private readonly DateTime? _timeTo;

        public BackupProgressForm(string folder, DateTime? timeFrom, DateTime? timeTo)
        {
            InitializeComponent();
            _folder   = folder;
            _timeFrom = timeFrom;
            _timeTo   = timeTo;

            this.Text         = LangHelper.Get("Backup_ConnectProgressTitle");
            btnOK.Values.Text = LangHelper.Get("Msg_OK");
            btnOK.Enabled     = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 窗体完全显示后再启动异步任务，确保进度条可见
            BeginInvoke(new Action(async () => await RunConnectAsync()));
        }

        // 后台连接 + UI 更新

        private async Task RunConnectAsync()
        {
            progressBar.Style   = ProgressBarStyle.Marquee;
            progressBar.Visible = true;
            richTextResult.Clear();

            // 预先扫描文件夹，获取将被连接的文件列表（用于结果显示）
            var files = BackupManager.GetMatchingFiles(_folder, _timeFrom, _timeTo);

            // 在专用 STA 线程上调用 COM API，避免冻结 UI
            int result = await RunOnStaThread(
                () => BackupManager.ConnectBackup(_folder, _timeFrom, _timeTo));

            progressBar.Visible = false;

            if (result == 0 || result == 1)
            {
                if (files.Count > 0)
                {
                    foreach (var f in files)
                    {
                        AppendLine("✓ ", Color.FromArgb(0, 140, 0));
                        AppendLine($"\"{f.Name}\" {LangHelper.Get("Backup_ConnectSuccessDetail")}\n",
                                   richTextResult.ForeColor);
                    }
                }
                else
                {
                    // 文件夹内无可识别文件名，显示通用成功信息
                    AppendLine("✓ ", Color.FromArgb(0, 140, 0));
                    AppendLine(LangHelper.Get("Backup_ConnectSuccess") + "\n",
                               richTextResult.ForeColor);
                }
            }
            else
            {
                AppendLine("✗ ", Color.Red);
                AppendLine($"{LangHelper.Get("Backup_ConnectFailed")} (code={result})\n", Color.Red);
            }

            btnOK.Enabled = true;
            btnOK.Focus();
        }

        /// <summary>
        /// 在新建的 STA 线程上执行 <paramref name="func"/>，以避免
        /// WinCC COM 对象（STA）在线程池（MTA）线程上调用时出现封送错误。
        /// </summary>
        private static Task<T> RunOnStaThread<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            var t   = new Thread(() =>
            {
                try   { tcs.SetResult(func()); }
                catch (Exception ex) { tcs.SetException(ex); }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
            return tcs.Task;
        }

        private void AppendLine(string text, Color color)
        {
            richTextResult.SelectionStart  = richTextResult.TextLength;
            richTextResult.SelectionLength = 0;
            richTextResult.SelectionColor  = color;
            richTextResult.AppendText(text);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
