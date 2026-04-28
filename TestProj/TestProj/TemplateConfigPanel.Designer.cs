namespace TestProj
{
    partial class TemplateConfigPanel
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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.lstTemplates = new System.Windows.Forms.ListBox();
            this.pnlLeftHeader = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblTemplatesHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnClone = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tlpFields = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtTagSummary = new System.Windows.Forms.TextBox();
            this.btnSelectTags = new System.Windows.Forms.Button();
            this.btnConfigTag = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeftHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.tlpFields.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitMain.Panel2.Controls.Add(this.panel1);
            this.splitMain.Size = new System.Drawing.Size(1764, 1049);
            this.splitMain.SplitterDistance = 417;
            this.splitMain.TabIndex = 0;
            // 
            // lstTemplates
            // 
            this.lstTemplates.Font = new System.Drawing.Font("SimSun", 14F);
            this.lstTemplates.FormattingEnabled = true;
            this.lstTemplates.ItemHeight = 42;
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
            this.pnlLeftHeader.Size = new System.Drawing.Size(417, 111);
            this.pnlLeftHeader.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnAdd.Location = new System.Drawing.Point(293, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 71);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // lblTemplatesHeader
            // 
            this.lblTemplatesHeader.AutoSize = true;
            this.lblTemplatesHeader.Font = new System.Drawing.Font("SimSun", 14F);
            this.lblTemplatesHeader.Location = new System.Drawing.Point(22, 32);
            this.lblTemplatesHeader.Name = "lblTemplatesHeader";
            this.lblTemplatesHeader.Size = new System.Drawing.Size(186, 42);
            this.lblTemplatesHeader.TabIndex = 0;
            this.lblTemplatesHeader.Text = "报表模板";
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
            this.pnlFooter.Size = new System.Drawing.Size(1343, 172);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnClone
            // 
            this.btnClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClone.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnClone.Location = new System.Drawing.Point(575, 50);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(385, 88);
            this.btnClone.TabIndex = 3;
            this.btnClone.Text = "复制并创建新模板";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.BtnClone_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnDelete.Location = new System.Drawing.Point(1014, 50);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(278, 88);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除模板";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnCancel.Location = new System.Drawing.Point(279, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(222, 88);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnSave.Location = new System.Drawing.Point(34, 50);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(210, 88);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 14F);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 42);
            this.label2.TabIndex = 0;
            this.label2.Text = "模板名称";
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
            // txtTagSummary
            // 
            this.txtTagSummary.Font = new System.Drawing.Font("SimSun", 14F);
            this.txtTagSummary.Location = new System.Drawing.Point(3, 3);
            this.txtTagSummary.Name = "txtTagSummary";
            this.txtTagSummary.Size = new System.Drawing.Size(446, 55);
            this.txtTagSummary.TabIndex = 0;
            // 
            // btnSelectTags
            // 
            this.btnSelectTags.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnSelectTags.Location = new System.Drawing.Point(471, 3);
            this.btnSelectTags.Name = "btnSelectTags";
            this.btnSelectTags.Size = new System.Drawing.Size(161, 55);
            this.btnSelectTags.TabIndex = 1;
            this.btnSelectTags.Text = "...";
            this.btnSelectTags.UseVisualStyleBackColor = true;
            this.btnSelectTags.Click += new System.EventHandler(this.BtnSelectTags_Click);
            // 
            // btnConfigTag
            // 
            this.btnConfigTag.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnConfigTag.Location = new System.Drawing.Point(735, 0);
            this.btnConfigTag.Name = "btnConfigTag";
            this.btnConfigTag.Size = new System.Drawing.Size(262, 71);
            this.btnConfigTag.TabIndex = 2;
            this.btnConfigTag.Text = "变量配置";
            this.btnConfigTag.UseVisualStyleBackColor = true;
            this.btnConfigTag.Click += new System.EventHandler(this.BtnConfigTag_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 14F);
            this.label4.Location = new System.Drawing.Point(3, 430);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 42);
            this.label4.TabIndex = 4;
            this.label4.Text = "选择变量";
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Font = new System.Drawing.Font("SimSun", 14F);
            this.txtDesc.Location = new System.Drawing.Point(195, 220);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(1222, 55);
            this.txtDesc.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 14F);
            this.label3.Location = new System.Drawing.Point(3, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 42);
            this.label3.TabIndex = 2;
            this.label3.Text = "备注说明";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("SimSun", 14F);
            this.txtName.Location = new System.Drawing.Point(195, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(1222, 55);
            this.txtName.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tlpFields);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1343, 1049);
            this.panel1.TabIndex = 9;
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
            this.tlpFields.ResumeLayout(false);
            this.tlpFields.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.ListBox lstTemplates;
        private System.Windows.Forms.Panel pnlLeftHeader;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblTemplatesHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnClone;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tlpFields;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnConfigTag;
        private System.Windows.Forms.Button btnSelectTags;
        private System.Windows.Forms.TextBox txtTagSummary;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}