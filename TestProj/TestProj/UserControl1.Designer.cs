namespace TestProj
{
    partial class UserControl1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonManager = new Krypton.Toolkit.KryptonManager(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TimeSelectBtn = new Krypton.Toolkit.KryptonButton();
            this.lblTheme = new Krypton.Toolkit.KryptonLabel();
            this.cmbTheme = new Krypton.Toolkit.KryptonComboBox();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.cmbTemplate = new Krypton.Toolkit.KryptonComboBox();
            this.btnImportExcel = new Krypton.Toolkit.KryptonButton();
            this.PrintBtn = new Krypton.Toolkit.KryptonButton();
            this.btnConfig = new Krypton.Toolkit.KryptonButton();
            this.ExportExcelBtn = new Krypton.Toolkit.KryptonButton();
            this.dataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
            this.kryptonManager.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.TimeSelectBtn);
            this.panelTop.Controls.Add(this.lblTheme);
            this.panelTop.Controls.Add(this.cmbTheme);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.cmbTemplate);
            this.panelTop.Controls.Add(this.btnImportExcel);
            this.panelTop.Controls.Add(this.PrintBtn);
            this.panelTop.Controls.Add(this.btnConfig);
            this.panelTop.Controls.Add(this.ExportExcelBtn);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1940, 263);
            this.panelTop.TabIndex = 0;
            this.panelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTop_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(45, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(448, 212);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // TimeSelectBtn
            // 
            this.TimeSelectBtn.Location = new System.Drawing.Point(542, 95);
            this.TimeSelectBtn.Name = "TimeSelectBtn";
            this.TimeSelectBtn.Size = new System.Drawing.Size(266, 86);
            this.TimeSelectBtn.TabIndex = 33;
            this.TimeSelectBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.TimeSelectBtn.Values.Text = "时间选择";
            this.TimeSelectBtn.Click += new System.EventHandler(this.TimeSelectBtn_Click);
            // 
            // lblTheme
            // 
            this.lblTheme.Location = new System.Drawing.Point(542, 200);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(103, 42);
            this.lblTheme.TabIndex = 34;
            this.lblTheme.Values.Text = "主题：";
            // 
            // cmbTheme
            // 
            this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTheme.Location = new System.Drawing.Point(630, 197);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(280, 42);
            this.cmbTheme.TabIndex = 35;
            this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.CmbTheme_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(1313, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 42);
            this.label3.TabIndex = 10;
            this.label3.Values.Text = "选择模板";
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTemplate.Location = new System.Drawing.Point(1451, 34);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(439, 42);
            this.cmbTemplate.TabIndex = 18;
            this.cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.CmbTemplate_SelectedIndexChanged);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcel.Location = new System.Drawing.Point(856, 108);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(319, 60);
            this.btnImportExcel.TabIndex = 31;
            this.btnImportExcel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnImportExcel.Values.Text = "批量导入模板";
            this.btnImportExcel.Click += new System.EventHandler(this.BtnImportFromExcel_Click);
            // 
            // PrintBtn
            // 
            this.PrintBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintBtn.Location = new System.Drawing.Point(1404, 108);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(213, 60);
            this.PrintBtn.TabIndex = 32;
            this.PrintBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.PrintBtn.Values.Text = "打印";
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.Location = new System.Drawing.Point(1200, 108);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(176, 60);
            this.btnConfig.TabIndex = 16;
            this.btnConfig.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnConfig.Values.Text = "配置";
            this.btnConfig.Click += new System.EventHandler(this.BtnConfig_Click);
            // 
            // ExportExcelBtn
            // 
            this.ExportExcelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportExcelBtn.Location = new System.Drawing.Point(1658, 108);
            this.ExportExcelBtn.Name = "ExportExcelBtn";
            this.ExportExcelBtn.Size = new System.Drawing.Size(176, 60);
            this.ExportExcelBtn.TabIndex = 15;
            this.ExportExcelBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.ExportExcelBtn.Values.Text = "导出到EXCEL";
            this.ExportExcelBtn.Click += new System.EventHandler(this.ExportExcelBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 263);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1940, 1144);
            this.dataGridView1.TabIndex = 1;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1940, 1407);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Krypton.Toolkit.KryptonButton TimeSelectBtn;
        private Krypton.Toolkit.KryptonLabel lblTheme;
        private Krypton.Toolkit.KryptonComboBox cmbTheme;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonComboBox cmbTemplate;
        private Krypton.Toolkit.KryptonButton btnImportExcel;
        private Krypton.Toolkit.KryptonButton PrintBtn;
        private Krypton.Toolkit.KryptonButton btnConfig;
        private Krypton.Toolkit.KryptonButton ExportExcelBtn;
        private Krypton.Toolkit.KryptonDataGridView dataGridView1;
    }
}
