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
            this.confirmBtn = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // tagTreeView
            // 
            this.tagTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tagTreeView.CheckBoxes = true;
            this.tagTreeView.Location = new System.Drawing.Point(21, 10);
            this.tagTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.tagTreeView.MultiSelect = true;
            this.tagTreeView.Name = "tagTreeView";
            this.tagTreeView.Size = new System.Drawing.Size(586, 627);
            this.tagTreeView.TabIndex = 0;
            this.tagTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TagTreeView_AfterCheck);
            // 
            // confirmBtn
            // 
            this.confirmBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.confirmBtn.Location = new System.Drawing.Point(199, 655);
            this.confirmBtn.Margin = new System.Windows.Forms.Padding(2);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(266, 59);
            this.confirmBtn.TabIndex = 1;
            this.confirmBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.confirmBtn.Values.Text = "保存选择";
            this.confirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // TagSelectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 725);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.tagTreeView);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(665, 739);
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
