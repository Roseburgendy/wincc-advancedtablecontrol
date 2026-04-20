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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(52, 210);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1405, 742);
            this.dataGridView1.TabIndex = 1;
            // 
            // DT_begin
            // 
            this.DT_begin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DT_begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_begin.Location = new System.Drawing.Point(203, 97);
            this.DT_begin.Name = "DT_begin";
            this.DT_begin.Size = new System.Drawing.Size(288, 31);
            this.DT_begin.TabIndex = 2;
            // 
            // DT_end
            // 
            this.DT_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DT_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DT_end.Location = new System.Drawing.Point(203, 133);
            this.DT_end.Name = "DT_end";
            this.DT_end.Size = new System.Drawing.Size(288, 31);
            this.DT_end.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(88, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(88, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "结束时间";
            // 
            // searchBtn
            // 
            this.searchBtn.Font = new System.Drawing.Font("Arial", 9F);
            this.searchBtn.Location = new System.Drawing.Point(1092, 82);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(189, 55);
            this.searchBtn.TabIndex = 6;
            this.searchBtn.Text = "搜索";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // TagConfigBtn
            // 
            this.TagConfigBtn.Location = new System.Drawing.Point(818, 123);
            this.TagConfigBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TagConfigBtn.Name = "TagConfigBtn";
            this.TagConfigBtn.Size = new System.Drawing.Size(189, 55);
            this.TagConfigBtn.TabIndex = 8;
            this.TagConfigBtn.Text = "变量配置";
            this.TagConfigBtn.UseVisualStyleBackColor = true;
            this.TagConfigBtn.Click += new System.EventHandler(this.TagConfigBtn_Click);
            // 
            // selectTagBtn
            // 
            this.selectTagBtn.Location = new System.Drawing.Point(818, 44);
            this.selectTagBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selectTagBtn.Name = "selectTagBtn";
            this.selectTagBtn.Size = new System.Drawing.Size(189, 55);
            this.selectTagBtn.TabIndex = 9;
            this.selectTagBtn.Text = "选择变量";
            this.selectTagBtn.UseVisualStyleBackColor = true;
            this.selectTagBtn.Click += new System.EventHandler(this.SelectTagBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F);
            this.label3.Location = new System.Drawing.Point(80, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 32);
            this.label3.TabIndex = 10;
            this.label3.Text = "AdvancedTableControl Demo";
            // 
            // lblSelectedTags
            // 
            this.lblSelectedTags.AutoSize = true;
            this.lblSelectedTags.Location = new System.Drawing.Point(567, 53);
            this.lblSelectedTags.Name = "lblSelectedTags";
            this.lblSelectedTags.Size = new System.Drawing.Size(219, 21);
            this.lblSelectedTags.TabIndex = 11;
            this.lblSelectedTags.Text = "Choose a variable..";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1524, 1094);
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
    }
}
