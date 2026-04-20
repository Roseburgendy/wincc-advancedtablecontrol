namespace TestProj
{
    partial class TagConfigPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.txtTagName = new System.Windows.Forms.TextBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDataType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDecimalPlaces = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvTags = new System.Windows.Forms.DataGridView();
            this.saveConfigBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "变量名称";
            // 
            // txtTagName
            // 
            this.txtTagName.Location = new System.Drawing.Point(190, 63);
            this.txtTagName.Margin = new System.Windows.Forms.Padding(31, 3, 3, 3);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.ReadOnly = true;
            this.txtTagName.Size = new System.Drawing.Size(278, 38);
            this.txtTagName.TabIndex = 5;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(192, 123);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(278, 38);
            this.txtDisplayName.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "变量别名";
            // 
            // txtDataType
            // 
            this.txtDataType.Location = new System.Drawing.Point(192, 188);
            this.txtDataType.Name = "txtDataType";
            this.txtDataType.Size = new System.Drawing.Size(278, 38);
            this.txtDataType.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "数据类型";
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(673, 389);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(278, 38);
            this.txtMax.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(542, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 27);
            this.label6.TabIndex = 10;
            this.label6.Text = "上限值";
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(190, 387);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(278, 38);
            this.txtMin.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 389);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 27);
            this.label7.TabIndex = 12;
            this.label7.Text = "下限值";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDecimalPlaces);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtOrder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTagName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMin);
            this.groupBox1.Controls.Add(this.txtDisplayName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMax);
            this.groupBox1.Controls.Add(this.txtDataType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(53, 562);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1116, 465);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "变量配置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 27);
            this.label2.TabIndex = 16;
            this.label2.Text = "精确位数";
            // 
            // txtDecimalPlaces
            // 
            this.txtDecimalPlaces.Location = new System.Drawing.Point(192, 253);
            this.txtDecimalPlaces.Name = "txtDecimalPlaces";
            this.txtDecimalPlaces.Size = new System.Drawing.Size(278, 38);
            this.txtDecimalPlaces.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 27);
            this.label1.TabIndex = 14;
            this.label1.Text = "顺序";
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(192, 315);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(278, 38);
            this.txtOrder.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvTags);
            this.groupBox2.Location = new System.Drawing.Point(53, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1116, 483);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "变量列表";
            // 
            // dgvTags
            // 
            this.dgvTags.AllowUserToAddRows = false;
            this.dgvTags.AllowUserToDeleteRows = false;
            this.dgvTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTags.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTags.Location = new System.Drawing.Point(4, 33);
            this.dgvTags.Name = "dgvTags";
            this.dgvTags.RowHeadersWidth = 92;
            this.dgvTags.RowTemplate.Height = 40;
            this.dgvTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTags.Size = new System.Drawing.Size(1106, 449);
            this.dgvTags.TabIndex = 0;
            // 
            // saveConfigBtn
            // 
            this.saveConfigBtn.Location = new System.Drawing.Point(986, 1055);
            this.saveConfigBtn.Name = "saveConfigBtn";
            this.saveConfigBtn.Size = new System.Drawing.Size(177, 50);
            this.saveConfigBtn.TabIndex = 18;
            this.saveConfigBtn.Text = "保存配置";
            this.saveConfigBtn.UseVisualStyleBackColor = true;
            this.saveConfigBtn.Click += new System.EventHandler(this.SaveConfigBtn_Click_1);
            // 
            // TagConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1269, 1117);
            this.Controls.Add(this.saveConfigBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TagConfigPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量配置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTagName;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDataType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button saveConfigBtn;
        private System.Windows.Forms.DataGridView dgvTags;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDecimalPlaces;
    }
}
