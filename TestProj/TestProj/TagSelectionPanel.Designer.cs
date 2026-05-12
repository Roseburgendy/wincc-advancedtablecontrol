namespace TestProj
{
    partial class TagSelectionPanel
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
            this.tagTreeView = new Krypton.Toolkit.KryptonTreeView();
            this.confirmBtn  = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            //
            // tagTreeView
            //
            this.tagTreeView.CheckBoxes = true;
            this.tagTreeView.Location = new System.Drawing.Point(25, 12);
            this.tagTreeView.Name = "tagTreeView";
            this.tagTreeView.Size = new System.Drawing.Size(740, 766);
            this.tagTreeView.TabIndex = 0;
            this.tagTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TagTreeView_AfterCheck);
            //
            // confirmBtn
            //
            this.confirmBtn.Location = new System.Drawing.Point(290, 800);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(213, 57);
            this.confirmBtn.TabIndex = 1;
            this.confirmBtn.Values.Text = "保存选择";
            this.confirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            //
            // TagSelectionPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 886);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.tagTreeView);
            this.Name = "TagSelectionPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量选择";
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonTreeView tagTreeView;
        private Krypton.Toolkit.KryptonButton confirmBtn;
    }
}
