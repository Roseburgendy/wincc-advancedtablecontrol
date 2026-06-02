namespace TestProj
{
    partial class BackupProgressForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.richTextResult = new System.Windows.Forms.RichTextBox();
            this.btnOK = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(31, 27);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.progressBar.MarqueeAnimationSpeed = 25;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1123, 27);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // richTextResult
            // 
            this.richTextResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextResult.BackColor = System.Drawing.SystemColors.Window;
            this.richTextResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextResult.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextResult.Location = new System.Drawing.Point(31, 70);
            this.richTextResult.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.richTextResult.Name = "richTextResult";
            this.richTextResult.ReadOnly = true;
            this.richTextResult.Size = new System.Drawing.Size(1120, 458);
            this.richTextResult.TabIndex = 1;
            this.richTextResult.Text = "";
            this.richTextResult.WordWrap = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(936, 556);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(218, 68);
            this.btnOK.TabIndex = 2;
            this.btnOK.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOK.Values.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BackupProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 648);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.richTextResult);
            this.Controls.Add(this.btnOK);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MinimumSize = new System.Drawing.Size(856, 512);
            this.Name = "BackupProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "连接归档";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar  progressBar;
        private System.Windows.Forms.RichTextBox  richTextResult;
        private Krypton.Toolkit.KryptonButton     btnOK;
    }
}
