using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProj
{
    /// <summary>
    /// 断开备份归档的进度与结果对话框。
    /// 打开时自动在后台 STA 线程上调用 BackupManager.DisconnectBackup，
    /// 通过 Marquee 进度条指示断开中，完成后显示结果。
    /// </summary>
    public partial class BackupDisconnectProgressForm : Krypton.Toolkit.KryptonForm
    {
        private readonly ConnectedArchiveEntry _entry;

        public BackupDisconnectProgressForm(ConnectedArchiveEntry entry)
        {
            InitializeComponent();
            _entry = entry;

            this.Text         = LangHelper.Get("Backup_DisconnectProgressTitle");
            btnOK.Values.Text = LangHelper.Get("Msg_OK");
            btnOK.Enabled     = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BeginInvoke(new Action(async () => await RunDisconnectAsync()));
        }

        private async Task RunDisconnectAsync()
        {
            progressBar.Style   = ProgressBarStyle.Marquee;
            progressBar.Visible = true;
            richTextResult.Clear();

            int result = await RunOnStaThread(
                () => BackupManager.DisconnectBackup(_entry));

            progressBar.Visible = false;

            if (result == 0)
            {
                AppendLine("✓ ", Color.FromArgb(0, 140, 0));
                AppendLine($"\"{_entry.DisplayName}\" {LangHelper.Get("Backup_DisconnectSuccessDetail")}\n",
                           richTextResult.ForeColor);
            }
            else
            {
                AppendLine("✗ ", Color.Red);
                AppendLine($"{LangHelper.Get("Backup_DisconnectFailed")} (code={result})\n", Color.Red);
            }

            btnOK.Enabled = true;
            btnOK.Focus();
        }

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
