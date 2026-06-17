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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.kryptonManager = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.btnConnectBackup = new Krypton.Toolkit.KryptonButton();
            this.btnDisconnectBackup = new Krypton.Toolkit.KryptonButton();
            this.btnTodayQuery = new Krypton.Toolkit.KryptonButton();
            this.btnPrevDay = new Krypton.Toolkit.KryptonButton();
            this.btnNextDay = new Krypton.Toolkit.KryptonButton();
            this.btnLogin = new Krypton.Toolkit.KryptonButton();
            this.TimeSelectBtn = new Krypton.Toolkit.KryptonButton();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.cmbTemplate = new Krypton.Toolkit.KryptonComboBox();
            this.btnImportExcel = new Krypton.Toolkit.KryptonButton();
            this.PrintBtn = new Krypton.Toolkit.KryptonButton();
            this.btnConfig = new Krypton.Toolkit.KryptonButton();
            this.ExportExcelBtn = new Krypton.Toolkit.KryptonButton();
            this.dataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
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
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.btnConnectBackup);
            this.kryptonPanel1.Controls.Add(this.btnDisconnectBackup);
            this.kryptonPanel1.Controls.Add(this.btnTodayQuery);
            this.kryptonPanel1.Controls.Add(this.btnPrevDay);
            this.kryptonPanel1.Controls.Add(this.btnNextDay);
            this.kryptonPanel1.Controls.Add(this.btnLogin);
            this.kryptonPanel1.Controls.Add(this.TimeSelectBtn);
            this.kryptonPanel1.Controls.Add(this.label3);
            this.kryptonPanel1.Controls.Add(this.cmbTemplate);
            this.kryptonPanel1.Controls.Add(this.btnImportExcel);
            this.kryptonPanel1.Controls.Add(this.PrintBtn);
            this.kryptonPanel1.Controls.Add(this.btnConfig);
            this.kryptonPanel1.Controls.Add(this.ExportExcelBtn);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(2493, 244);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(58, 63);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(428, 128);
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("DengXian", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.kryptonLabel1.TabIndex = 39;
            this.kryptonLabel1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonLabel1.Values.Image")));
            this.kryptonLabel1.Values.Text = "中源技术";
            // 
            // btnConnectBackup
            // 
            this.btnConnectBackup.Location = new System.Drawing.Point(500, 125);
            this.btnConnectBackup.Name = "btnConnectBackup";
            this.btnConnectBackup.Size = new System.Drawing.Size(234, 76);
            this.btnConnectBackup.TabIndex = 40;
            this.btnConnectBackup.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnConnectBackup.Values.Text = "连接备份";
            this.btnConnectBackup.Visible = false;
            this.btnConnectBackup.Click += new System.EventHandler(this.BtnConnectBackup_Click);
            // 
            // btnDisconnectBackup
            // 
            this.btnDisconnectBackup.Location = new System.Drawing.Point(757, 125);
            this.btnDisconnectBackup.Name = "btnDisconnectBackup";
            this.btnDisconnectBackup.Size = new System.Drawing.Size(234, 76);
            this.btnDisconnectBackup.TabIndex = 41;
            this.btnDisconnectBackup.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDisconnectBackup.Values.Text = "断开备份";
            this.btnDisconnectBackup.Visible = false;
            this.btnDisconnectBackup.Click += new System.EventHandler(this.BtnDisconnectBackup_Click);
            // 
            // btnTodayQuery
            // 
            this.btnTodayQuery.Location = new System.Drawing.Point(1371, 32);
            this.btnTodayQuery.Name = "btnTodayQuery";
            this.btnTodayQuery.Size = new System.Drawing.Size(260, 76);
            this.btnTodayQuery.TabIndex = 42;
            this.btnTodayQuery.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTodayQuery.Values.Text = "今日查询";
            this.btnTodayQuery.Click += new System.EventHandler(this.BtnTodayQuery_Click);
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Location = new System.Drawing.Point(1142, 32);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(186, 76);
            this.btnPrevDay.TabIndex = 43;
            this.btnPrevDay.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnPrevDay.Values.Text = "前一天";
            this.btnPrevDay.Click += new System.EventHandler(this.BtnPrevDay_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.Location = new System.Drawing.Point(1668, 32);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(186, 76);
            this.btnNextDay.TabIndex = 44;
            this.btnNextDay.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnNextDay.Values.Text = "后一天";
            this.btnNextDay.Click += new System.EventHandler(this.BtnNextDay_Click);
            //
            // btnLogin
            //
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogin.Location = new System.Drawing.Point(1201, 32);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(220, 76);
            this.btnLogin.TabIndex = 45;
            this.btnLogin.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnLogin.Values.Text = "管理员登录";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            //
            // TimeSelectBtn
            //
            this.TimeSelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeSelectBtn.Location = new System.Drawing.Point(1032, 125);
            this.TimeSelectBtn.Name = "TimeSelectBtn";
            this.TimeSelectBtn.Size = new System.Drawing.Size(296, 76);
            this.TimeSelectBtn.TabIndex = 33;
            this.TimeSelectBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.TimeSelectBtn.Values.Text = "时间选择";
            this.TimeSelectBtn.Click += new System.EventHandler(this.TimeSelectBtn_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(1890, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 42);
            this.label3.TabIndex = 10;
            this.label3.Values.Text = "选择模板";
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTemplate.Location = new System.Drawing.Point(2028, 50);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(439, 42);
            this.cmbTemplate.TabIndex = 18;
            this.cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.CmbTemplate_SelectedIndexChanged);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportExcel.Location = new System.Drawing.Point(1371, 125);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(324, 76);
            this.btnImportExcel.TabIndex = 31;
            this.btnImportExcel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnImportExcel.Values.Text = "批量导入模板";
            this.btnImportExcel.Click += new System.EventHandler(this.BtnImportFromExcel_Click);
            // 
            // PrintBtn
            // 
            this.PrintBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintBtn.Location = new System.Drawing.Point(1935, 125);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(213, 76);
            this.PrintBtn.TabIndex = 32;
            this.PrintBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.PrintBtn.Values.Text = "打印";
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfig.Location = new System.Drawing.Point(1732, 125);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(176, 76);
            this.btnConfig.TabIndex = 16;
            this.btnConfig.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnConfig.Values.Text = "配置";
            this.btnConfig.Click += new System.EventHandler(this.BtnConfig_Click);
            // 
            // ExportExcelBtn
            // 
            this.ExportExcelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportExcelBtn.Location = new System.Drawing.Point(2180, 125);
            this.ExportExcelBtn.Name = "ExportExcelBtn";
            this.ExportExcelBtn.Size = new System.Drawing.Size(290, 76);
            this.ExportExcelBtn.TabIndex = 15;
            this.ExportExcelBtn.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.ExportExcelBtn.Values.Text = "导出到EXCEL";
            this.ExportExcelBtn.Click += new System.EventHandler(this.ExportExcelBtn_Click);
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 252);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.Size = new System.Drawing.Size(2493, 1205);
            this.dataGridView1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 244);
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(2493, 8);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 45F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.kryptonPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(2493, 1457);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
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
    }
}
