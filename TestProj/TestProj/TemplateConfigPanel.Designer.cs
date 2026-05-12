namespace TestProj
{
    partial class TemplateConfigPanel
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
            this.splitMain          = new System.Windows.Forms.SplitContainer();
            this.lstTemplates       = new Krypton.Toolkit.KryptonListBox();
            this.pnlLeftHeader      = new System.Windows.Forms.Panel();
            this.btnAdd             = new Krypton.Toolkit.KryptonButton();
            this.lblTemplatesHeader = new Krypton.Toolkit.KryptonLabel();
            this.pnlFooter          = new System.Windows.Forms.Panel();
            this.btnClone           = new Krypton.Toolkit.KryptonButton();
            this.btnDelete          = new Krypton.Toolkit.KryptonButton();
            this.btnCancel          = new Krypton.Toolkit.KryptonButton();
            this.btnSave            = new Krypton.Toolkit.KryptonButton();
            this.pnlDetail          = new System.Windows.Forms.Panel();
            this.tlpFields          = new System.Windows.Forms.TableLayoutPanel();
            this.txtName            = new Krypton.Toolkit.KryptonTextBox();
            this.label3             = new Krypton.Toolkit.KryptonLabel();
            this.txtDesc            = new Krypton.Toolkit.KryptonTextBox();
            this.label4             = new Krypton.Toolkit.KryptonLabel();
            this.panel4             = new System.Windows.Forms.Panel();
            this.btnConfigTag       = new Krypton.Toolkit.KryptonButton();
            this.btnSelectTags      = new Krypton.Toolkit.KryptonButton();
            this.txtTagSummary      = new Krypton.Toolkit.KryptonTextBox();
            this.label2             = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeftHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.tlpFields.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            //
            // splitMain
            //
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.Controls.Add(this.lstTemplates);
            this.splitMain.Panel1.Controls.Add(this.pnlLeftHeader);
            this.splitMain.Panel2.Controls.Add(this.pnlFooter);
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Size = new System.Drawing.Size(1764, 1049);
            this.splitMain.SplitterDistance = 416;
            this.splitMain.TabIndex = 0;
            //
            // lstTemplates
            //
            this.lstTemplates.Font = new System.Drawing.Font("SimSun", 14F);
            this.lstTemplates.Location = new System.Drawing.Point(12, 137);
            this.lstTemplates.Name = "lstTemplates";
            this.lstTemplates.Size = new System.Drawing.Size(402, 886);
            this.lstTemplates.TabIndex = 1;
            this.lstTemplates.SelectedIndexChanged += new System.EventHandler(this.LstTemplates_SelectedIndexChanged);
            //
            // pnlLeftHeader
            //
            this.pnlLeftHeader.Controls.Add(this.btnAdd);
            this.pnlLeftHeader.Controls.Add(this.lblTemplatesHeader);
            this.pnlLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftHeader.Name = "pnlLeftHeader";
            this.pnlLeftHeader.Size = new System.Drawing.Size(416, 111);
            this.pnlLeftHeader.TabIndex = 0;
            //
            // btnAdd
            //
            this.btnAdd.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnAdd.Location = new System.Drawing.Point(293, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 71);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Values.Text = "+";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            //
            // lblTemplatesHeader
            //
            this.lblTemplatesHeader.Font = new System.Drawing.Font("SimSun", 14F);
            this.lblTemplatesHeader.Location = new System.Drawing.Point(22, 32);
            this.lblTemplatesHeader.Name = "lblTemplatesHeader";
            this.lblTemplatesHeader.Size = new System.Drawing.Size(186, 42);
            this.lblTemplatesHeader.TabIndex = 0;
            this.lblTemplatesHeader.Values.Text = "报表模板";
            //
            // pnlFooter
            //
            this.pnlFooter.Controls.Add(this.btnClone);
            this.pnlFooter.Controls.Add(this.btnDelete);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 877);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1344, 172);
            this.pnlFooter.TabIndex = 1;
            //
            // btnClone
            //
            this.btnClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClone.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnClone.Location = new System.Drawing.Point(576, 50);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(385, 88);
            this.btnClone.TabIndex = 3;
            this.btnClone.Values.Text = "复制并创建新模板";
            this.btnClone.Click += new System.EventHandler(this.BtnClone_Click);
            //
            // btnDelete
            //
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnDelete.Location = new System.Drawing.Point(1015, 50);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(278, 88);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Values.Text = "删除模板";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnCancel.Location = new System.Drawing.Point(280, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(222, 88);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnSave.Location = new System.Drawing.Point(35, 50);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(210, 88);
            this.btnSave.TabIndex = 0;
            this.btnSave.Values.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            //
            // pnlDetail
            //
            this.pnlDetail.Controls.Add(this.tlpFields);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(1344, 1049);
            this.pnlDetail.TabIndex = 9;
            //
            // tlpFields
            //
            this.tlpFields.ColumnCount = 2;
            this.tlpFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFields.Controls.Add(this.txtName, 1, 0);
            this.tlpFields.Controls.Add(this.label3, 0, 1);
            this.tlpFields.Controls.Add(this.txtDesc, 1, 1);
            this.tlpFields.Controls.Add(this.label4, 0, 2);
            this.tlpFields.Controls.Add(this.panel4, 1, 2);
            this.tlpFields.Controls.Add(this.label2, 0, 0);
            this.tlpFields.Location = new System.Drawing.Point(6, 49);
            this.tlpFields.Name = "tlpFields";
            this.tlpFields.RowCount = 3;
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.42735F));
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.57265F));
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 381F));
            this.tlpFields.Size = new System.Drawing.Size(1350, 812);
            this.tlpFields.TabIndex = 8;
            //
            // txtName
            //
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("SimSun", 14F);
            this.txtName.Location = new System.Drawing.Point(195, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(1222, 55);
            this.txtName.TabIndex = 1;
            //
            // label3
            //
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("SimSun", 14F);
            this.label3.Location = new System.Drawing.Point(3, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 42);
            this.label3.TabIndex = 2;
            this.label3.Values.Text = "备注说明";
            //
            // txtDesc
            //
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Font = new System.Drawing.Font("SimSun", 14F);
            this.txtDesc.Location = new System.Drawing.Point(195, 220);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(1222, 55);
            this.txtDesc.TabIndex = 3;
            //
            // label4
            //
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("SimSun", 14F);
            this.label4.Location = new System.Drawing.Point(3, 430);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 42);
            this.label4.TabIndex = 4;
            this.label4.Values.Text = "选择变量";
            //
            // panel4
            //
            this.panel4.Controls.Add(this.btnConfigTag);
            this.panel4.Controls.Add(this.btnSelectTags);
            this.panel4.Controls.Add(this.txtTagSummary);
            this.panel4.Location = new System.Drawing.Point(195, 433);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1134, 137);
            this.panel4.TabIndex = 5;
            //
            // btnConfigTag
            //
            this.btnConfigTag.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnConfigTag.Location = new System.Drawing.Point(735, 0);
            this.btnConfigTag.Name = "btnConfigTag";
            this.btnConfigTag.Size = new System.Drawing.Size(262, 71);
            this.btnConfigTag.TabIndex = 2;
            this.btnConfigTag.Values.Text = "变量配置";
            this.btnConfigTag.Click += new System.EventHandler(this.BtnConfigTag_Click);
            //
            // btnSelectTags
            //
            this.btnSelectTags.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnSelectTags.Location = new System.Drawing.Point(471, 3);
            this.btnSelectTags.Name = "btnSelectTags";
            this.btnSelectTags.Size = new System.Drawing.Size(161, 55);
            this.btnSelectTags.TabIndex = 1;
            this.btnSelectTags.Values.Text = "...";
            this.btnSelectTags.Click += new System.EventHandler(this.BtnSelectTags_Click);
            //
            // txtTagSummary
            //
            this.txtTagSummary.Font = new System.Drawing.Font("SimSun", 14F);
            this.txtTagSummary.Location = new System.Drawing.Point(3, 3);
            this.txtTagSummary.Name = "txtTagSummary";
            this.txtTagSummary.Size = new System.Drawing.Size(446, 55);
            this.txtTagSummary.TabIndex = 0;
            //
            // label2
            //
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("SimSun", 14F);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 42);
            this.label2.TabIndex = 0;
            this.label2.Values.Text = "模板名称";
            //
            // TemplateConfigPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(216F, 216F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1764, 1049);
            this.Controls.Add(this.splitMain);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Name = "TemplateConfigPanel";
            this.Text = "模板配置";
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLeftHeader.ResumeLayout(false);
            this.pnlLeftHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.tlpFields.ResumeLayout(false);
            this.tlpFields.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private Krypton.Toolkit.KryptonListBox lstTemplates;
        private System.Windows.Forms.Panel pnlLeftHeader;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonLabel lblTemplatesHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private Krypton.Toolkit.KryptonButton btnClone;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tlpFields;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonTextBox txtDesc;
        private Krypton.Toolkit.KryptonLabel label4;
        private System.Windows.Forms.Panel panel4;
        private Krypton.Toolkit.KryptonButton btnConfigTag;
        private Krypton.Toolkit.KryptonButton btnSelectTags;
        private Krypton.Toolkit.KryptonTextBox txtTagSummary;
        private Krypton.Toolkit.KryptonLabel label2;
        private System.Windows.Forms.Panel pnlDetail;
    }
}
