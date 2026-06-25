namespace WINCCReporter
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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.lstTemplates = new Krypton.Toolkit.KryptonListBox();
            this.pnlLeftHeader = new System.Windows.Forms.Panel();
            this.btnAdd = new Krypton.Toolkit.KryptonButton();
            this.lblTemplatesHeader = new Krypton.Toolkit.KryptonLabel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnClone = new Krypton.Toolkit.KryptonButton();
            this.btnDelete = new Krypton.Toolkit.KryptonButton();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.tlpFields = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.txtName = new Krypton.Toolkit.KryptonTextBox();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.txtDesc = new Krypton.Toolkit.KryptonTextBox();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtTagSummary = new Krypton.Toolkit.KryptonTextBox();
            this.btnSelectTags = new Krypton.Toolkit.KryptonButton();
            this.btnConfigTag = new Krypton.Toolkit.KryptonButton();
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
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.lstTemplates);
            this.splitMain.Panel1.Controls.Add(this.pnlLeftHeader);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlFooter);
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Size = new System.Drawing.Size(1764, 1049);
            this.splitMain.SplitterDistance = 450;
            this.splitMain.TabIndex = 0;
            // 
            // lstTemplates
            // 
            this.lstTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTemplates.Location = new System.Drawing.Point(0, 90);
            this.lstTemplates.Name = "lstTemplates";
            this.lstTemplates.Size = new System.Drawing.Size(450, 959);
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
            this.pnlLeftHeader.Size = new System.Drawing.Size(450, 90);
            this.pnlLeftHeader.TabIndex = 0;
//            this.pnlLeftHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLeftHeader_Paint);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(330, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 68);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAdd.Values.Text = "+ 新建";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // lblTemplatesHeader
            // 
            this.lblTemplatesHeader.Location = new System.Drawing.Point(22, 22);
            this.lblTemplatesHeader.Name = "lblTemplatesHeader";
            this.lblTemplatesHeader.Size = new System.Drawing.Size(186, 42);
            this.lblTemplatesHeader.TabIndex = 0;
            this.lblTemplatesHeader.Values.Text = "报表模板";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnClone);
            this.pnlFooter.Controls.Add(this.btnDelete);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 929);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1310, 120);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(20, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 80);
            this.btnSave.TabIndex = 0;
            this.btnSave.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSave.Values.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(240, 18);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 80);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnClone
            // 
            this.btnClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClone.Location = new System.Drawing.Point(500, 18);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(360, 80);
            this.btnClone.TabIndex = 3;
            this.btnClone.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClone.Values.Text = "复制并创建新模板";
            this.btnClone.Click += new System.EventHandler(this.BtnClone_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(900, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(260, 80);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDelete.Values.Text = "删除模板";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.tlpFields);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(1310, 1049);
            this.pnlDetail.TabIndex = 9;
            // 
            // tlpFields
            // 
            this.tlpFields.ColumnCount = 2;
            this.tlpFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tlpFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFields.Controls.Add(this.label2, 0, 0);
            this.tlpFields.Controls.Add(this.txtName, 1, 0);
            this.tlpFields.Controls.Add(this.label3, 0, 1);
            this.tlpFields.Controls.Add(this.txtDesc, 1, 1);
            this.tlpFields.Controls.Add(this.panel4, 1, 2);
            this.tlpFields.Controls.Add(this.label4, 0, 2);
            this.tlpFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFields.Location = new System.Drawing.Point(0, 0);
            this.tlpFields.Name = "tlpFields";
            this.tlpFields.Padding = new System.Windows.Forms.Padding(12, 12, 12, 4);
            this.tlpFields.RowCount = 3;
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFields.Size = new System.Drawing.Size(1310, 1049);
            this.tlpFields.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(15, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 42);
            this.label2.TabIndex = 0;
            this.label2.Values.Text = "模板名称";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(205, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(1090, 43);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(15, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 42);
            this.label3.TabIndex = 2;
            this.label3.Values.Text = "备注说明";
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Location = new System.Drawing.Point(205, 105);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(1090, 43);
            this.txtDesc.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(15, 597);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 42);
            this.label4.TabIndex = 4;
            this.label4.Values.Text = "选择变量";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtTagSummary);
            this.panel4.Controls.Add(this.btnSelectTags);
            this.panel4.Controls.Add(this.btnConfigTag);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(205, 195);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1090, 847);
            this.panel4.TabIndex = 5;
            // 
            // txtTagSummary
            // 
            this.txtTagSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTagSummary.Location = new System.Drawing.Point(0, 6);
            this.txtTagSummary.Multiline = true;
            this.txtTagSummary.Name = "txtTagSummary";
            this.txtTagSummary.ReadOnly = true;
            this.txtTagSummary.Size = new System.Drawing.Size(620, 68);
            this.txtTagSummary.TabIndex = 0;
            // 
            // btnSelectTags
            // 
            this.btnSelectTags.Location = new System.Drawing.Point(35, 130);
            this.btnSelectTags.Name = "btnSelectTags";
            this.btnSelectTags.Size = new System.Drawing.Size(200, 68);
            this.btnSelectTags.TabIndex = 1;
            this.btnSelectTags.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSelectTags.Values.Text = "选择变量…";
            this.btnSelectTags.Click += new System.EventHandler(this.BtnSelectTags_Click);
            // 
            // btnConfigTag
            // 
            this.btnConfigTag.Location = new System.Drawing.Point(35, 233);
            this.btnConfigTag.Name = "btnConfigTag";
            this.btnConfigTag.Size = new System.Drawing.Size(200, 68);
            this.btnConfigTag.TabIndex = 2;
            this.btnConfigTag.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnConfigTag.Values.Text = "变量配置…";
            this.btnConfigTag.Click += new System.EventHandler(this.BtnConfigTag_Click);
            // 
            // TemplateConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1764, 1049);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Controls.Add(this.splitMain);
            this.MinimumSize = new System.Drawing.Size(900, 600);
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
