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
            this.DT_begin = new System.Windows.Forms.DateTimePicker();
            this.DT_end = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.searchBtn = new System.Windows.Forms.Button();
            this.TagConfigBtn = new System.Windows.Forms.Button();
            this.selectTagBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSelectedTags = new System.Windows.Forms.Label();
            this.BtnToday = new System.Windows.Forms.Button();
            this.BtnThisWeek = new System.Windows.Forms.Button();
            this.BtnThisMonth = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(66, 270);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1788, 954);
            this.dataGridView1.TabIndex = 1;
            // 
            // DT_begin
            // 
            this.DT_begin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DT_begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_begin.Location = new System.Drawing.Point(258, 125);
            this.DT_begin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DT_begin.Name = "DT_begin";
            this.DT_begin.Size = new System.Drawing.Size(365, 38);
            this.DT_begin.TabIndex = 2;
            // 
            // DT_end
            // 
            this.DT_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DT_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_end.Location = new System.Drawing.Point(258, 171);
            this.DT_end.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DT_end.Name = "DT_end";
            this.DT_end.Size = new System.Drawing.Size(365, 38);
            this.DT_end.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(112, 125);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(112, 172);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "结束时间";
            // 
            // searchBtn
            // 
            this.searchBtn.Font = new System.Drawing.Font("Arial", 9F);
            this.searchBtn.Location = new System.Drawing.Point(1613, 153);
            this.searchBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(241, 71);
            this.searchBtn.TabIndex = 6;
            this.searchBtn.Text = "搜索";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // TagConfigBtn
            // 
            this.TagConfigBtn.Location = new System.Drawing.Point(1300, 56);
            this.TagConfigBtn.Name = "TagConfigBtn";
            this.TagConfigBtn.Size = new System.Drawing.Size(241, 71);
            this.TagConfigBtn.TabIndex = 8;
            this.TagConfigBtn.Text = "变量配置";
            this.TagConfigBtn.UseVisualStyleBackColor = true;
            this.TagConfigBtn.Click += new System.EventHandler(this.TagConfigBtn_Click);
            // 
            // selectTagBtn
            // 
            this.selectTagBtn.Location = new System.Drawing.Point(1036, 56);
            this.selectTagBtn.Name = "selectTagBtn";
            this.selectTagBtn.Size = new System.Drawing.Size(241, 71);
            this.selectTagBtn.TabIndex = 9;
            this.selectTagBtn.Text = "选择变量";
            this.selectTagBtn.UseVisualStyleBackColor = true;
            this.selectTagBtn.Click += new System.EventHandler(this.SelectTagBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F);
            this.label3.Location = new System.Drawing.Point(102, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(480, 41);
            this.label3.TabIndex = 10;
            this.label3.Text = "AdvancedTableControl Demo";
            // 
            // lblSelectedTags
            // 
            this.lblSelectedTags.AutoSize = true;
            this.lblSelectedTags.Location = new System.Drawing.Point(722, 67);
            this.lblSelectedTags.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedTags.Name = "lblSelectedTags";
            this.lblSelectedTags.Size = new System.Drawing.Size(278, 27);
            this.lblSelectedTags.TabIndex = 11;
            this.lblSelectedTags.Text = "Choose a variable..";
            // 
            // BtnToday
            // 
            this.BtnToday.Location = new System.Drawing.Point(720, 171);
            this.BtnToday.Name = "BtnToday";
            this.BtnToday.Size = new System.Drawing.Size(174, 60);
            this.BtnToday.TabIndex = 12;
            this.BtnToday.Text = "今日";
            this.BtnToday.UseVisualStyleBackColor = true;
            this.BtnToday.Click += new System.EventHandler(this.BtnToday_Click);
            // 
            // BtnThisWeek
            // 
            this.BtnThisWeek.Location = new System.Drawing.Point(941, 171);
            this.BtnThisWeek.Name = "BtnThisWeek";
            this.BtnThisWeek.Size = new System.Drawing.Size(186, 60);
            this.BtnThisWeek.TabIndex = 13;
            this.BtnThisWeek.Text = "本周";
            this.BtnThisWeek.UseVisualStyleBackColor = true;
            this.BtnThisWeek.Click += new System.EventHandler(this.BtnThisWeek_Click);
            // 
            // BtnThisMonth
            // 
            this.BtnThisMonth.Location = new System.Drawing.Point(1178, 171);
            this.BtnThisMonth.Name = "BtnThisMonth";
            this.BtnThisMonth.Size = new System.Drawing.Size(166, 60);
            this.BtnThisMonth.TabIndex = 14;
            this.BtnThisMonth.Text = "本月";
            this.BtnThisMonth.UseVisualStyleBackColor = true;
            this.BtnThisMonth.Click += new System.EventHandler(this.BtnThisMonth_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnThisMonth);
            this.Controls.Add(this.BtnThisWeek);
            this.Controls.Add(this.BtnToday);
            this.Controls.Add(this.lblSelectedTags);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.selectTagBtn);
            this.Controls.Add(this.TagConfigBtn);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DT_end);
            this.Controls.Add(this.DT_begin);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1940, 1407);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker DT_begin;
        private System.Windows.Forms.DateTimePicker DT_end;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Button TagConfigBtn;
        private System.Windows.Forms.Button selectTagBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSelectedTags;
        private System.Windows.Forms.Button BtnToday;
        private System.Windows.Forms.Button BtnThisWeek;
        private System.Windows.Forms.Button BtnThisMonth;
    }
}
