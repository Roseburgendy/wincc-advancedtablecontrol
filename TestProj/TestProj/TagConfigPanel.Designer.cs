namespace WINCCReporter
{
    partial class TagConfigPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.txtTagName = new Krypton.Toolkit.KryptonTextBox();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.txtDisplayName = new Krypton.Toolkit.KryptonTextBox();
            this.label8 = new Krypton.Toolkit.KryptonLabel();
            this.txtDisplayNameEn = new Krypton.Toolkit.KryptonTextBox();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.txtDecimalPlaces = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.txtOrder = new Krypton.Toolkit.KryptonTextBox();
            this.groupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.groupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.dgvTags = new Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveConfigBtn = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1.Panel)).BeginInit();
            this.groupBox1.Panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2.Panel)).BeginInit();
            this.groupBox2.Panel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(28, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 42);
            this.label3.TabIndex = 0;
            this.label3.Values.Text = "变量名称";
            // 
            // txtTagName
            // 
            this.txtTagName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTagName.Location = new System.Drawing.Point(208, 54);
            this.txtTagName.Margin = new System.Windows.Forms.Padding(2);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.ReadOnly = true;
            this.txtTagName.Size = new System.Drawing.Size(743, 43);
            this.txtTagName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(28, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 42);
            this.label4.TabIndex = 2;
            this.label4.Values.Text = "中文别名";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayName.Location = new System.Drawing.Point(208, 106);
            this.txtDisplayName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(743, 43);
            this.txtDisplayName.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(28, 159);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 42);
            this.label8.TabIndex = 4;
            this.label8.Values.Text = "英文别名";
            // 
            // txtDisplayNameEn
            // 
            this.txtDisplayNameEn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayNameEn.Location = new System.Drawing.Point(208, 159);
            this.txtDisplayNameEn.Margin = new System.Windows.Forms.Padding(2);
            this.txtDisplayNameEn.Name = "txtDisplayNameEn";
            this.txtDisplayNameEn.Size = new System.Drawing.Size(743, 43);
            this.txtDisplayNameEn.TabIndex = 5;
            //
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 263);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 42);
            this.label2.TabIndex = 8;
            this.label2.Values.Text = "精确位数";
            // 
            // txtDecimalPlaces
            // 
            this.txtDecimalPlaces.Location = new System.Drawing.Point(208, 263);
            this.txtDecimalPlaces.Margin = new System.Windows.Forms.Padding(2);
            this.txtDecimalPlaces.Name = "txtDecimalPlaces";
            this.txtDecimalPlaces.Size = new System.Drawing.Size(229, 43);
            this.txtDecimalPlaces.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(72, 317);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 42);
            this.label1.TabIndex = 10;
            this.label1.Values.Text = "顺序";
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(208, 317);
            this.txtOrder.Margin = new System.Windows.Forms.Padding(2);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(229, 43);
            this.txtOrder.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(10, 584);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            // 
            // groupBox1.Panel
            // 
            this.groupBox1.Panel.Controls.Add(this.label3);
            this.groupBox1.Panel.Controls.Add(this.txtTagName);
            this.groupBox1.Panel.Controls.Add(this.label4);
            this.groupBox1.Panel.Controls.Add(this.txtDisplayName);
            this.groupBox1.Panel.Controls.Add(this.label8);
            this.groupBox1.Panel.Controls.Add(this.txtDisplayNameEn);
            this.groupBox1.Panel.Controls.Add(this.label2);
            this.groupBox1.Panel.Controls.Add(this.txtDecimalPlaces);
            this.groupBox1.Panel.Controls.Add(this.label1);
            this.groupBox1.Panel.Controls.Add(this.txtOrder);
            this.groupBox1.Size = new System.Drawing.Size(1551, 488);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.Values.Heading = "变量配置";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            // 
            // groupBox2.Panel
            // 
            this.groupBox2.Panel.Controls.Add(this.dgvTags);
            this.groupBox2.Size = new System.Drawing.Size(1551, 565);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.Values.Heading = "变量列表";
            // 
            // dgvTags
            // 
            this.dgvTags.AllowUserToAddRows = false;
            this.dgvTags.AllowUserToDeleteRows = false;
            this.dgvTags.AutoGenerateKryptonColumns = false;
            this.dgvTags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column8,
            this.Column4,
            this.Column5});
            this.dgvTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTags.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTags.Location = new System.Drawing.Point(0, 0);
            this.dgvTags.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTags.Name = "dgvTags";
            this.dgvTags.RowHeadersWidth = 92;
            this.dgvTags.RowTemplate.Height = 50;
            this.dgvTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTags.Size = new System.Drawing.Size(1547, 519);
            this.dgvTags.TabIndex = 0;
            this.dgvTags.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvTags_CellValueChanged);
            this.dgvTags.SelectionChanged += new System.EventHandler(this.DgvTags_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "TagName";
            this.Column1.FillWeight = 18F;
            this.Column1.HeaderText = "变量名称";
            this.Column1.MinimumWidth = 80;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "DisplayName";
            this.Column2.FillWeight = 18F;
            this.Column2.HeaderText = "中文别名";
            this.Column2.MinimumWidth = 80;
            this.Column2.Name = "Column2";
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.DataPropertyName = "DisplayNameEn";
            this.Column8.FillWeight = 18F;
            this.Column8.HeaderText = "英文别名";
            this.Column8.MinimumWidth = 80;
            this.Column8.Name = "Column8";
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "Order";
            this.Column4.FillWeight = 6F;
            this.Column4.HeaderText = "顺序";
            this.Column4.MinimumWidth = 50;
            this.Column4.Name = "Column4";
            //
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.DataPropertyName = "DecimalPlaces";
            this.Column5.FillWeight = 8F;
            this.Column5.HeaderText = "小数位数";
            this.Column5.MinimumWidth = 60;
            this.Column5.Name = "Column5";
            // 
            // saveConfigBtn
            // 
            this.saveConfigBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveConfigBtn.Location = new System.Drawing.Point(1251, 1095);
            this.saveConfigBtn.Margin = new System.Windows.Forms.Padding(2);
            this.saveConfigBtn.Name = "saveConfigBtn";
            this.saveConfigBtn.Size = new System.Drawing.Size(310, 72);
            this.saveConfigBtn.TabIndex = 18;
            this.saveConfigBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.saveConfigBtn.Values.Text = "保存配置";
            this.saveConfigBtn.Click += new System.EventHandler(this.SaveConfigBtn_Click_1);
            // 
            // TagConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1572, 1191);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.saveConfigBtn);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1572, 1000);
            this.Name = "TagConfigPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量配置";
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1.Panel)).EndInit();
            this.groupBox1.Panel.ResumeLayout(false);
            this.groupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2.Panel)).EndInit();
            this.groupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonTextBox txtTagName;
        private Krypton.Toolkit.KryptonLabel label4;
        private Krypton.Toolkit.KryptonTextBox txtDisplayName;
        private Krypton.Toolkit.KryptonLabel label8;
        private Krypton.Toolkit.KryptonTextBox txtDisplayNameEn;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonTextBox txtDecimalPlaces;
        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonTextBox txtOrder;
        private Krypton.Toolkit.KryptonGroupBox groupBox1;
        private Krypton.Toolkit.KryptonGroupBox groupBox2;
        private Krypton.Toolkit.KryptonButton saveConfigBtn;
        private Krypton.Toolkit.KryptonDataGridView dgvTags;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}
