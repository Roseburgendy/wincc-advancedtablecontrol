using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProj
{
    /// <summary>
    /// 连接备份归档的进度与结果对话框。
    /// 支持两种模式：
    ///   1. 手动多文件：传入 folder + List&lt;BackupFileInfo&gt;，逐文件调用 Restore
    ///   2. 自动连接：传入 folder + timeFrom + timeTo，单次调用 Restore
    /// </summary>
    public partial class BackupProgressForm : Krypton.Toolkit.KryptonForm
    {
        private readonly string               _folder;
        private readonly List<BackupFileInfo> _files;    // 非 null = 手动多文件模式
        private readonly DateTime?            _timeFrom; // 自动连接模式
        private readonly DateTime?            _timeTo;

        /// <summary>手动多文件连接模式（从 BackupConnectForm 调用）。</summary>
        public BackupProgressForm(string folder, List<BackupFileInfo> files)
        {
            InitializeComponent();
            _folder = folder;
            _files  = files ?? new List<BackupFileInfo>();
            Init();
        }

        /// <summary>自动连接 / 单时间段模式（查询时自动触发）。</summary>
        public BackupProgressForm(string folder, DateTime? timeFrom, DateTime? timeTo)
        {
            InitializeComponent();
            _folder   = folder;
            _files    = null;
            _timeFrom = timeFrom;
            _timeTo   = timeTo;
            Init();
        }

        private void Init()
        {
            this.Text         = LangHelper.Get("Backup_ConnectProgressTitle");
            btnOK.Values.Text = LangHelper.Get("Msg_OK");
            btnOK.Enabled     = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BeginInvoke(new Action(async () => await RunConnectAsync()));
        }

        private async Task RunConnectAsync()
        {
            progressBar.Style   = ProgressBarStyle.Marquee;
            progressBar.Visible = true;
            richTextResult.Clear();

            if (_files != null)
            {
                // ── 手动多文件模式 ──────────────────────────────────────────
                if (_files.Count == 0)
                {
                    // 未选择具体文件 → 连接文件夹下所有归档
                    int r = await RunOnStaThread(
                        () => BackupManager.ConnectBackup(_folder, null, null));
                    AppendGenericResult(r);
                }
                else
                {
                    foreach (var file in _files)
                    {
                        DateTime? from = file.TimeRange?.Start;
                        DateTime? to   = file.TimeRange?.End;
                        int r = await RunOnStaThread(
                            () => BackupManager.ConnectBackup(_folder, from, to));

                        if (r == 0 || r == 1)
                        {
                            AppendLine("✓ ", Color.FromArgb(0, 140, 0));
                            AppendLine($"\"{file.Name}\" {LangHelper.Get("Backup_ConnectSuccessDetail")}\n",
                                       richTextResult.ForeColor);
                        }
                        else
                        {
                            AppendLine("✗ ", Color.Red);
                            AppendLine($"\"{file.Name}\" {LangHelper.Get("Backup_ConnectFailed")} (code={r})\n",
                                       Color.Red);
                        }
                    }
                }
            }
            else
            {
                // ── 自动连接模式（单次 Restore）──────────────────────────────
                var matchingFiles = BackupManager.GetMatchingFiles(_folder, _timeFrom, _timeTo);
                int result = await RunOnStaThread(
                    () => BackupManager.ConnectBackup(_folder, _timeFrom, _timeTo));

                if (result == 0 || result == 1)
                {
                    if (matchingFiles.Count > 0)
                    {
                        foreach (var f in matchingFiles)
                        {
                            AppendLine("✓ ", Color.FromArgb(0, 140, 0));
                            AppendLine($"\"{f.Name}\" {LangHelper.Get("Backup_ConnectSuccessDetail")}\n",
                                       richTextResult.ForeColor);
                        }
                    }
                    else
                        AppendGenericResult(result);
                }
                else
                    AppendGenericResult(result);
            }

            progressBar.Visible = false;
            btnOK.Enabled = true;
            btnOK.Focus();
        }

        private void AppendGenericResult(int result)
        {
            if (result == 0 || result == 1)
            {
                AppendLine("✓ ", Color.FromArgb(0, 140, 0));
                AppendLine(LangHelper.Get("Backup_ConnectSuccess") + "\n", richTextResult.ForeColor);
            }
            else
            {
                AppendLine("✗ ", Color.Red);
                AppendLine($"{LangHelper.Get("Backup_ConnectFailed")} (code={result})\n", Color.Red);
            }
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
