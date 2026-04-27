namespace TestProj
{
    partial class UserControl1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.searchBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ExportExcelBtn = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 263);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1940, 1144);
            this.dataGridView1.TabIndex = 1;
            // 
            // searchBtn
            // 
            this.searchBtn.Font = new System.Drawing.Font("Arial", 9F);
            this.searchBtn.Location = new System.Drawing.Point(1613, 70);
            this.searchBtn.Margin = new System.Windows.Forms.Padding(4);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(241, 71);
            this.searchBtn.TabIndex = 6;
            this.searchBtn.Text = "搜索";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F);
            this.label3.Location = new System.Drawing.Point(92, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 41);
            this.label3.TabIndex = 10;
            this.label3.Text = "选择模板";
            // 
            // ExportExcelBtn
            // 
            this.ExportExcelBtn.Font = new System.Drawing.Font("SimSun", 9F);
            this.ExportExcelBtn.Location = new System.Drawing.Point(1019, 70);
            this.ExportExcelBtn.Name = "ExportExcelBtn";
            this.ExportExcelBtn.Size = new System.Drawing.Size(241, 71);
            this.ExportExcelBtn.TabIndex = 15;
            this.ExportExcelBtn.Text = "导出到EXCEL";
            this.ExportExcelBtn.UseVisualStyleBackColor = true;
            this.ExportExcelBtn.Click += new System.EventHandler(this.ExportExcelBtn_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(1306, 70);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(241, 71);
            this.btnConfig.TabIndex = 16;
            this.btnConfig.Text = "配置";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.BtnConfig_Click);
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Location = new System.Drawing.Point(84, 89);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(439, 35);
            this.cmbTemplate.TabIndex = 18;
            this.cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.CmbTemplate_SelectedIndexChanged);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbTemplate);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.ExportExcelBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1940, 1407);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ExportExcelBtn;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.ComboBox cmbTemplate;
    }
}
