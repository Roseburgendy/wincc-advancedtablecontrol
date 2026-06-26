namespace WINCCReporter
{
    partial class WINCCReporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WINCCReporter));
            this.kryptonManager = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox4 = new Krypton.Toolkit.KryptonGroupBox();
            this.btnImportExcel = new Krypton.Toolkit.KryptonButton();
            this.btnConfig = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            this.btnConnectBackup = new Krypton.Toolkit.KryptonButton();
            this.btnDisconnectBackup = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.PrintBtn = new Krypton.Toolkit.KryptonButton();
            this.ExportExcelBtn = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.btnPrevDay = new Krypton.Toolkit.KryptonButton();
            this.btnTodayQuery = new Krypton.Toolkit.KryptonButton();
            this.btnNextDay = new Krypton.Toolkit.KryptonButton();
            this.TimeSelectBtn = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.lblQueryRange = new Krypton.Toolkit.KryptonLabel();
            this.lblRowCount = new Krypton.Toolkit.KryptonLabel();
            this.btnLogin = new Krypton.Toolkit.KryptonButton();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.cmbTemplate = new Krypton.Toolkit.KryptonComboBox();
            this.dataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4.Panel)).BeginInit();
            this.kryptonGroupBox4.Panel.SuspendLayout();
            this.kryptonGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            this.kryptonGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
            this.kryptonManager.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox4);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox3);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.lblQueryRange);
            this.kryptonPanel1.Controls.Add(this.lblRowCount);
            this.kryptonPanel1.Controls.Add(this.btnLogin);
            this.kryptonPanel1.Controls.Add(this.label3);
            this.kryptonPanel1.Controls.Add(this.cmbTemplate);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(2560, 204);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonGroupBox4
            // 
            this.kryptonGroupBox4.Location = new System.Drawing.Point(664, 12);
            // 
            // kryptonGroupBox4.Panel
            // 
            this.kryptonGroupBox4.Panel.Controls.Add(this.btnImportExcel);
            this.kryptonGroupBox4.Panel.Controls.Add(this.btnConfig);
            this.kryptonGroupBox4.Size = new System.Drawing.Size(311, 175);
            this.kryptonGroupBox4.TabIndex = 51;
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcel.Location = new System.Drawing.Point(10, 10);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(280, 51);
            this.btnImportExcel.TabIndex = 31;
            this.btnImportExcel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnImportExcel.Values.Text = "批量导入模板";
            this.btnImportExcel.Click += new System.EventHandler(this.BtnImportFromExcel_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.Location = new System.Drawing.Point(10, 73);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(280, 51);
            this.btnConfig.TabIndex = 16;
            this.btnConfig.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnConfig.Values.Text = "配置";
            this.btnConfig.Click += new System.EventHandler(this.BtnConfig_Click);
            // 
            // kryptonGroupBox3
            // 
            this.kryptonGroupBox3.Location = new System.Drawing.Point(345, 15);
            // 
            // kryptonGroupBox3.Panel
            // 
            this.kryptonGroupBox3.Panel.Controls.Add(this.btnConnectBackup);
            this.kryptonGroupBox3.Panel.Controls.Add(this.btnDisconnectBackup);
            this.kryptonGroupBox3.Size = new System.Drawing.Size(294, 172);
            this.kryptonGroupBox3.TabIndex = 50;
            // 
            // btnConnectBackup
            // 
            this.btnConnectBackup.Location = new System.Drawing.Point(12, 7);
            this.btnConnectBackup.Name = "btnConnectBackup";
            this.btnConnectBackup.Size = new System.Drawing.Size(253, 51);
            this.btnConnectBackup.TabIndex = 40;
            this.btnConnectBackup.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnConnectBackup.Values.Text = "连接备份";
            this.btnConnectBackup.Visible = false;
            this.btnConnectBackup.Click += new System.EventHandler(this.BtnConnectBackup_Click);
            // 
            // btnDisconnectBackup
            // 
            this.btnDisconnectBackup.Location = new System.Drawing.Point(12, 70);
            this.btnDisconnectBackup.Name = "btnDisconnectBackup";
            this.btnDisconnectBackup.Size = new System.Drawing.Size(253, 51);
            this.btnDisconnectBackup.TabIndex = 41;
            this.btnDisconnectBackup.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDisconnectBackup.Values.Text = "断开备份";
            this.btnDisconnectBackup.Visible = false;
            this.btnDisconnectBackup.Click += new System.EventHandler(this.BtnDisconnectBackup_Click);
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(1007, 9);
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.PrintBtn);
            this.kryptonGroupBox2.Panel.Controls.Add(this.ExportExcelBtn);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(304, 178);
            this.kryptonGroupBox2.TabIndex = 49;
            // 
            // PrintBtn
            // 
            this.PrintBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintBtn.Location = new System.Drawing.Point(13, 74);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(268, 51);
            this.PrintBtn.TabIndex = 32;
            this.PrintBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.PrintBtn.Values.Text = "打印";
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // ExportExcelBtn
            // 
            this.ExportExcelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportExcelBtn.Location = new System.Drawing.Point(13, 13);
            this.ExportExcelBtn.Name = "ExportExcelBtn";
            this.ExportExcelBtn.Size = new System.Drawing.Size(268, 51);
            this.ExportExcelBtn.TabIndex = 15;
            this.ExportExcelBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.ExportExcelBtn.Values.Text = "导出到EXCEL";
            this.ExportExcelBtn.Click += new System.EventHandler(this.ExportExcelBtn_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(1340, 9);
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnPrevDay);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnTodayQuery);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnNextDay);
            this.kryptonGroupBox1.Panel.Controls.Add(this.TimeSelectBtn);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(637, 178);
            this.kryptonGroupBox1.TabIndex = 48;
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Location = new System.Drawing.Point(18, 12);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(194, 51);
            this.btnPrevDay.TabIndex = 43;
            this.btnPrevDay.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnPrevDay.Values.Text = "前一天";
            this.btnPrevDay.Click += new System.EventHandler(this.BtnPrevDay_Click);
            // 
            // btnTodayQuery
            // 
            this.btnTodayQuery.Location = new System.Drawing.Point(218, 12);
            this.btnTodayQuery.Name = "btnTodayQuery";
            this.btnTodayQuery.Size = new System.Drawing.Size(194, 51);
            this.btnTodayQuery.TabIndex = 42;
            this.btnTodayQuery.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTodayQuery.Values.Text = "今日查询";
            this.btnTodayQuery.Click += new System.EventHandler(this.BtnTodayQuery_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.Location = new System.Drawing.Point(418, 12);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(194, 51);
            this.btnNextDay.TabIndex = 44;
            this.btnNextDay.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnNextDay.Values.Text = "后一天";
            this.btnNextDay.Click += new System.EventHandler(this.BtnNextDay_Click);
            // 
            // TimeSelectBtn
            // 
            this.TimeSelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeSelectBtn.Location = new System.Drawing.Point(18, 75);
            this.TimeSelectBtn.Name = "TimeSelectBtn";
            this.TimeSelectBtn.Size = new System.Drawing.Size(594, 51);
            this.TimeSelectBtn.TabIndex = 33;
            this.TimeSelectBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.TimeSelectBtn.Values.Text = "时间选择";
            this.TimeSelectBtn.Click += new System.EventHandler(this.TimeSelectBtn_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(18, 19);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(321, 102);
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("DengXian", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel1.TabIndex = 39;
            this.kryptonLabel1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonLabel1.Values.Image")));
            this.kryptonLabel1.Values.Text = "中源技术";
            // 
            // lblQueryRange
            // 
            this.lblQueryRange.Location = new System.Drawing.Point(2013, 82);
            this.lblQueryRange.Name = "lblQueryRange";
            this.lblQueryRange.Size = new System.Drawing.Size(400, 34);
            this.lblQueryRange.TabIndex = 46;
            this.lblQueryRange.Values.Text = "";
            // 
            // lblRowCount
            // 
            this.lblRowCount.Location = new System.Drawing.Point(2013, 121);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(400, 34);
            this.lblRowCount.TabIndex = 47;
            this.lblRowCount.Values.Text = "";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(39, 127);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(261, 51);
            this.btnLogin.TabIndex = 45;
            this.btnLogin.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnLogin.Values.Text = "管理员登录";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(2050, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 42);
            this.label3.TabIndex = 10;
            this.label3.Values.Text = "选择模板";
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTemplate.Location = new System.Drawing.Point(2205, 31);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(352, 42);
            this.cmbTemplate.TabIndex = 18;
            this.cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.CmbTemplate_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 210);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 48;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(2560, 1040);
            this.dataGridView1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 204);
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(2560, 6);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // WINCCReporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.kryptonPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WINCCReporter";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.Size = new System.Drawing.Size(2560, 1270);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4.Panel)).EndInit();
            this.kryptonGroupBox4.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).EndInit();
            this.kryptonGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            this.kryptonGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton btnConnectBackup;
        private Krypton.Toolkit.KryptonButton btnDisconnectBackup;
        private Krypton.Toolkit.KryptonButton btnTodayQuery;
        private Krypton.Toolkit.KryptonButton btnPrevDay;
        private Krypton.Toolkit.KryptonButton btnNextDay;
        private Krypton.Toolkit.KryptonButton btnLogin;
        private Krypton.Toolkit.KryptonButton TimeSelectBtn;
        private Krypton.Toolkit.KryptonLabel label3;
        private Krypton.Toolkit.KryptonComboBox cmbTemplate;
        private Krypton.Toolkit.KryptonButton btnImportExcel;
        private Krypton.Toolkit.KryptonButton PrintBtn;
        private Krypton.Toolkit.KryptonButton btnConfig;
        private Krypton.Toolkit.KryptonButton ExportExcelBtn;
        private Krypton.Toolkit.KryptonDataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel lblQueryRange;
        private Krypton.Toolkit.KryptonLabel lblRowCount;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox4;
    }
}
