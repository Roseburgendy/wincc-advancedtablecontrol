namespace TestProj
{
    partial class TagSelectionPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tagTreeView = new System.Windows.Forms.TreeView();
            this.confirmBtn = new System.Windows.Forms.Button();
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
            this.confirmBtn.Text = "保存选择";
            this.confirmBtn.UseVisualStyleBackColor = true;
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

        private System.Windows.Forms.TreeView tagTreeView;
        private System.Windows.Forms.Button confirmBtn;
    }
}