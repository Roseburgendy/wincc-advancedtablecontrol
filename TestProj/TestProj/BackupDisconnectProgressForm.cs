using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINCCReporter
{
    /// <summary>
    /// 断开备份归档的进度与结果对话框，支持一次断开多个归档。
    /// </summary>
    public partial class BackupDisconnectProgressForm : Krypton.Toolkit.KryptonForm
    {
        private readonly List<ConnectedArchiveEntry> _entries;

        public BackupDisconnectProgressForm(List<ConnectedArchiveEntry> entries)
        {
            InitializeComponent();
            _entries = entries ?? new List<ConnectedArchiveEntry>();

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

            foreach (var entry in _entries)
            {
                int result = await RunOnStaThread(
                    () => BackupManager.DisconnectBackup(entry));

                if (result == 0)
                {
                    AppendLine("✓ ", Color.FromArgb(0, 140, 0));
                    AppendLine($"\"{entry.DisplayName}\" {LangHelper.Get("Backup_DisconnectSuccessDetail")}\n",
                               richTextResult.ForeColor);
                }
                else
                {
                    AppendLine("✗ ", Color.Red);
                    AppendLine($"\"{entry.DisplayName}\" {LangHelper.Get("Backup_DisconnectFailed")} (code={result})\n",
                               Color.Red);
                }
            }

            progressBar.Visible = false;
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
