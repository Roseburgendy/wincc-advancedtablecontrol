namespace TestProj
{
    partial class GeneralConfigPanel
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
            // ── Navigator & Pages ──────────────────────────────────────────────
            this.kryptonNavigator1 = new Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1      = new Krypton.Navigator.KryptonPage();   // 模板配置
            this.kryptonPage2      = new Krypton.Navigator.KryptonPage();   // 通用设置
            this.kryptonPage3      = new Krypton.Navigator.KryptonPage();   // 导出设置

            // ── Tab 1: Template Config controls ───────────────────────────────
            this.splitMain          = new System.Windows.Forms.SplitContainer();
            this.lstTemplates       = new Krypton.Toolkit.KryptonListBox();
            this.pnlLeftHeader      = new Krypton.Toolkit.KryptonPanel();
            this.btnAdd             = new Krypton.Toolkit.KryptonButton();
            this.lblTemplatesHeader = new Krypton.Toolkit.KryptonLabel();
            this.pnlFooter          = new Krypton.Toolkit.KryptonPanel();
            this.btnSave            = new Krypton.Toolkit.KryptonButton();
            this.btnCancel          = new Krypton.Toolkit.KryptonButton();
            this.btnClone           = new Krypton.Toolkit.KryptonButton();
            this.btnDelete          = new Krypton.Toolkit.KryptonButton();
            this.pnlDetail          = new Krypton.Toolkit.KryptonPanel();
            this.tlpFields          = new System.Windows.Forms.TableLayoutPanel();
            this.label2             = new Krypton.Toolkit.KryptonLabel();
            this.txtName            = new Krypton.Toolkit.KryptonTextBox();
            this.labelDesc          = new Krypton.Toolkit.KryptonLabel();
            this.txtDesc            = new Krypton.Toolkit.KryptonTextBox();
            this.labelTags          = new Krypton.Toolkit.KryptonLabel();
            this.panel4             = new Krypton.Toolkit.KryptonPanel();
            this.txtTagSummary      = new Krypton.Toolkit.KryptonTextBox();
            this.btnSelectTags      = new Krypton.Toolkit.KryptonButton();
            this.btnConfigTag       = new Krypton.Toolkit.KryptonButton();

            // ── Tab 2: General Settings controls ──────────────────────────────
            this.tlpGeneral      = new System.Windows.Forms.TableLayoutPanel();
            this.lblCompanyName  = new Krypton.Toolkit.KryptonLabel();
            this.txtCompanyName  = new Krypton.Toolkit.KryptonTextBox();
            this.lblLanguage     = new Krypton.Toolkit.KryptonLabel();
            this.cmbLanguage     = new Krypton.Toolkit.KryptonComboBox();
            this.lblTheme        = new Krypton.Toolkit.KryptonLabel();
            this.cmbTheme        = new Krypton.Toolkit.KryptonThemeComboBox();
            this.pnlGeneralFooter = new Krypton.Toolkit.KryptonPanel();
            this.btnSaveGeneral  = new Krypton.Toolkit.KryptonButton();

            // ── Tab 3: Export Settings controls ───────────────────────────────
            this.tlpExport           = new System.Windows.Forms.TableLayoutPanel();
            this.lblExportPath       = new Krypton.Toolkit.KryptonLabel();
            this.pnlExportPath       = new Krypton.Toolkit.KryptonPanel();
            this.txtExportPath       = new Krypton.Toolkit.KryptonTextBox();
            this.btnBrowseExportPath = new Krypton.Toolkit.KryptonButton();
            this.lblFilePattern      = new Krypton.Toolkit.KryptonLabel();
            this.txtFilePattern      = new Krypton.Toolkit.KryptonTextBox();
            this.lblAutoExport       = new Krypton.Toolkit.KryptonLabel();
            this.chkAutoExport       = new Krypton.Toolkit.KryptonCheckBox();
            this.pnlExportFooter     = new Krypton.Toolkit.KryptonPanel();
            this.btnSaveExport       = new Krypton.Toolkit.KryptonButton();

            // ── Suspend / BeginInit ───────────────────────────────────────────
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            this.kryptonPage1.SuspendLayout();
            this.kryptonPage2.SuspendLayout();
            this.kryptonPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeftHeader)).BeginInit();
            this.pnlLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.tlpFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel4)).BeginInit();
            this.panel4.SuspendLayout();
            this.tlpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGeneralFooter)).BeginInit();
            this.pnlGeneralFooter.SuspendLayout();
            this.tlpExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlExportPath)).BeginInit();
            this.pnlExportPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlExportFooter)).BeginInit();
            this.pnlExportFooter.SuspendLayout();
            this.SuspendLayout();

            // ==================================================================
            // kryptonNavigator1
            // ==================================================================
            this.kryptonNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator1.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
                this.kryptonPage1,
                this.kryptonPage2,
                this.kryptonPage3
            });
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(1764, 1049);
            this.kryptonNavigator1.TabIndex = 0;

            // ==================================================================
            // kryptonPage1  —  模板配置
            // ==================================================================
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.splitMain);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(1762, 1010);
            this.kryptonPage1.Text = "模板配置";
            this.kryptonPage1.UniqueName = "page_templates_001";

            // splitMain
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.Controls.Add(this.lstTemplates);
            this.splitMain.Panel1.Controls.Add(this.pnlLeftHeader);
            this.splitMain.Panel2.Controls.Add(this.pnlFooter);
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Size = new System.Drawing.Size(1762, 1010);
            this.splitMain.SplitterDistance = 450;
            this.splitMain.TabIndex = 0;

            // lstTemplates
            this.lstTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTemplates.Location = new System.Drawing.Point(0, 90);
            this.lstTemplates.Name = "lstTemplates";
            this.lstTemplates.Size = new System.Drawing.Size(450, 920);
            this.lstTemplates.TabIndex = 1;
            this.lstTemplates.SelectedIndexChanged += new System.EventHandler(this.LstTemplates_SelectedIndexChanged);

            // pnlLeftHeader
            this.pnlLeftHeader.Controls.Add(this.btnAdd);
            this.pnlLeftHeader.Controls.Add(this.lblTemplatesHeader);
            this.pnlLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftHeader.Name = "pnlLeftHeader";
            this.pnlLeftHeader.Size = new System.Drawing.Size(450, 90);
            this.pnlLeftHeader.TabIndex = 0;

            // btnAdd
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(330, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 68);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAdd.Values.Text = "+ 新建";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            // lblTemplatesHeader
            this.lblTemplatesHeader.Location = new System.Drawing.Point(22, 22);
            this.lblTemplatesHeader.Name = "lblTemplatesHeader";
            this.lblTemplatesHeader.Size = new System.Drawing.Size(186, 42);
            this.lblTemplatesHeader.TabIndex = 0;
            this.lblTemplatesHeader.Values.Text = "报表模板";

            // pnlFooter
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnClone);
            this.pnlFooter.Controls.Add(this.btnDelete);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 890);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1308, 120);
            this.pnlFooter.TabIndex = 1;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(20, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 80);
            this.btnSave.TabIndex = 0;
            this.btnSave.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSave.Values.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(240, 18);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 80);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // btnClone
            this.btnClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClone.Location = new System.Drawing.Point(500, 18);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(360, 80);
            this.btnClone.TabIndex = 3;
            this.btnClone.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClone.Values.Text = "复制并创建新模板";
            this.btnClone.Click += new System.EventHandler(this.BtnClone_Click);

            // btnDelete
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(900, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(260, 80);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDelete.Values.Text = "删除模板";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);

            // pnlDetail
            this.pnlDetail.Controls.Add(this.tlpFields);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(1308, 1010);
            this.pnlDetail.TabIndex = 9;

            // tlpFields
            this.tlpFields.ColumnCount = 2;
            this.tlpFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tlpFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFields.Controls.Add(this.label2,    0, 0);
            this.tlpFields.Controls.Add(this.txtName,   1, 0);
            this.tlpFields.Controls.Add(this.labelDesc, 0, 1);
            this.tlpFields.Controls.Add(this.txtDesc,   1, 1);
            this.tlpFields.Controls.Add(this.panel4,    1, 2);
            this.tlpFields.Controls.Add(this.labelTags, 0, 2);
            this.tlpFields.BackColor = System.Drawing.Color.Transparent;
            this.tlpFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFields.Location = new System.Drawing.Point(0, 0);
            this.tlpFields.Name = "tlpFields";
            this.tlpFields.Padding = new System.Windows.Forms.Padding(12, 12, 12, 4);
            this.tlpFields.RowCount = 3;
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFields.Size = new System.Drawing.Size(1308, 1010);
            this.tlpFields.TabIndex = 8;

            // label2 (模板名称)
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(15, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 42);
            this.label2.TabIndex = 0;
            this.label2.Values.Text = "模板名称";

            // txtName
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(205, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(1090, 43);
            this.txtName.TabIndex = 1;

            // labelDesc (备注说明)
            this.labelDesc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDesc.Location = new System.Drawing.Point(15, 126);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(160, 42);
            this.labelDesc.TabIndex = 2;
            this.labelDesc.Values.Text = "备注说明";

            // txtDesc
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Location = new System.Drawing.Point(205, 105);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(1090, 43);
            this.txtDesc.TabIndex = 3;

            // labelTags (选择变量)
            this.labelTags.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTags.Location = new System.Drawing.Point(15, 597);
            this.labelTags.Name = "labelTags";
            this.labelTags.Size = new System.Drawing.Size(160, 42);
            this.labelTags.TabIndex = 4;
            this.labelTags.Values.Text = "选择变量";

            // panel4
            this.panel4.Controls.Add(this.txtTagSummary);
            this.panel4.Controls.Add(this.btnSelectTags);
            this.panel4.Controls.Add(this.btnConfigTag);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(205, 195);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1090, 810);
            this.panel4.TabIndex = 5;

            // txtTagSummary
            this.txtTagSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTagSummary.Location = new System.Drawing.Point(0, 6);
            this.txtTagSummary.Multiline = true;
            this.txtTagSummary.Name = "txtTagSummary";
            this.txtTagSummary.ReadOnly = true;
            this.txtTagSummary.Size = new System.Drawing.Size(620, 68);
            this.txtTagSummary.TabIndex = 0;

            // btnSelectTags
            this.btnSelectTags.Location = new System.Drawing.Point(35, 130);
            this.btnSelectTags.Name = "btnSelectTags";
            this.btnSelectTags.Size = new System.Drawing.Size(200, 68);
            this.btnSelectTags.TabIndex = 1;
            this.btnSelectTags.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSelectTags.Values.Text = "选择变量…";
            this.btnSelectTags.Click += new System.EventHandler(this.BtnSelectTags_Click);

            // btnConfigTag
            this.btnConfigTag.Location = new System.Drawing.Point(35, 233);
            this.btnConfigTag.Name = "btnConfigTag";
            this.btnConfigTag.Size = new System.Drawing.Size(200, 68);
            this.btnConfigTag.TabIndex = 2;
            this.btnConfigTag.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnConfigTag.Values.Text = "变量配置…";
            this.btnConfigTag.Click += new System.EventHandler(this.BtnConfigTag_Click);

            // ==================================================================
            // kryptonPage2  —  通用设置
            // ==================================================================
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.pnlGeneralFooter);
            this.kryptonPage2.Controls.Add(this.tlpGeneral);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(1762, 1010);
            this.kryptonPage2.Text = "通用设置";
            this.kryptonPage2.UniqueName = "page_general_001";

            // tlpGeneral
            this.tlpGeneral.ColumnCount = 2;
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGeneral.Controls.Add(this.lblCompanyName, 0, 0);
            this.tlpGeneral.Controls.Add(this.txtCompanyName, 1, 0);
            this.tlpGeneral.Controls.Add(this.lblLanguage,    0, 1);
            this.tlpGeneral.Controls.Add(this.cmbLanguage,    1, 1);
            this.tlpGeneral.Controls.Add(this.lblTheme,       0, 2);
            this.tlpGeneral.Controls.Add(this.cmbTheme,       1, 2);
            this.tlpGeneral.BackColor = System.Drawing.Color.Transparent;
            this.tlpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGeneral.Location = new System.Drawing.Point(0, 0);
            this.tlpGeneral.Name = "tlpGeneral";
            this.tlpGeneral.Padding = new System.Windows.Forms.Padding(30, 30, 30, 10);
            this.tlpGeneral.RowCount = 3;
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpGeneral.Size = new System.Drawing.Size(1762, 910);
            this.tlpGeneral.TabIndex = 0;

            // lblCompanyName
            this.lblCompanyName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCompanyName.Location = new System.Drawing.Point(33, 57);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(160, 42);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Values.Text = "公司名称：";

            // txtCompanyName
            this.txtCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompanyName.Location = new System.Drawing.Point(253, 40);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(600, 43);
            this.txtCompanyName.TabIndex = 1;

            // lblLanguage
            this.lblLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLanguage.Location = new System.Drawing.Point(33, 147);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(160, 42);
            this.lblLanguage.TabIndex = 2;
            this.lblLanguage.Values.Text = "显示语言：";

            // cmbLanguage
            this.cmbLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.Location = new System.Drawing.Point(253, 130);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(300, 42);
            this.cmbLanguage.TabIndex = 3;

            // lblTheme
            this.lblTheme.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTheme.Location = new System.Drawing.Point(33, 237);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(160, 42);
            this.lblTheme.TabIndex = 4;
            this.lblTheme.Values.Text = "界面主题：";

            // cmbTheme  (KryptonThemeComboBox)
            this.cmbTheme.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbTheme.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTheme.Location = new System.Drawing.Point(253, 220);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(400, 42);
            this.cmbTheme.TabIndex = 5;

            // pnlGeneralFooter
            this.pnlGeneralFooter.Controls.Add(this.btnSaveGeneral);
            this.pnlGeneralFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGeneralFooter.Location = new System.Drawing.Point(0, 910);
            this.pnlGeneralFooter.Name = "pnlGeneralFooter";
            this.pnlGeneralFooter.Size = new System.Drawing.Size(1762, 100);
            this.pnlGeneralFooter.TabIndex = 1;

            // btnSaveGeneral
            this.btnSaveGeneral.Location = new System.Drawing.Point(30, 14);
            this.btnSaveGeneral.Name = "btnSaveGeneral";
            this.btnSaveGeneral.Size = new System.Drawing.Size(200, 68);
            this.btnSaveGeneral.TabIndex = 0;
            this.btnSaveGeneral.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveGeneral.Values.Text = "保存";
            this.btnSaveGeneral.Click += new System.EventHandler(this.BtnSaveGeneral_Click);

            // ==================================================================
            // kryptonPage3  —  导出设置
            // ==================================================================
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Controls.Add(this.pnlExportFooter);
            this.kryptonPage3.Controls.Add(this.tlpExport);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(1762, 1010);
            this.kryptonPage3.Text = "导出设置";
            this.kryptonPage3.UniqueName = "page_export_001";

            // tlpExport
            this.tlpExport.ColumnCount = 2;
            this.tlpExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpExport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpExport.Controls.Add(this.lblExportPath, 0, 0);
            this.tlpExport.Controls.Add(this.pnlExportPath, 1, 0);
            this.tlpExport.Controls.Add(this.lblFilePattern, 0, 1);
            this.tlpExport.Controls.Add(this.txtFilePattern, 1, 1);
            this.tlpExport.Controls.Add(this.lblAutoExport,  0, 2);
            this.tlpExport.Controls.Add(this.chkAutoExport,  1, 2);
            this.tlpExport.BackColor = System.Drawing.Color.Transparent;
            this.tlpExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpExport.Location = new System.Drawing.Point(0, 0);
            this.tlpExport.Name = "tlpExport";
            this.tlpExport.Padding = new System.Windows.Forms.Padding(30, 30, 30, 10);
            this.tlpExport.RowCount = 3;
            this.tlpExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpExport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpExport.Size = new System.Drawing.Size(1762, 910);
            this.tlpExport.TabIndex = 0;

            // lblExportPath
            this.lblExportPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblExportPath.Location = new System.Drawing.Point(33, 62);
            this.lblExportPath.Name = "lblExportPath";
            this.lblExportPath.Size = new System.Drawing.Size(180, 42);
            this.lblExportPath.TabIndex = 0;
            this.lblExportPath.Values.Text = "默认导出路径：";

            // pnlExportPath  (textbox + browse button side by side)
            this.pnlExportPath.Controls.Add(this.txtExportPath);
            this.pnlExportPath.Controls.Add(this.btnBrowseExportPath);
            this.pnlExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlExportPath.Location = new System.Drawing.Point(253, 38);
            this.pnlExportPath.Name = "pnlExportPath";
            this.pnlExportPath.Size = new System.Drawing.Size(800, 58);
            this.pnlExportPath.TabIndex = 1;

            // txtExportPath
            this.txtExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExportPath.Location = new System.Drawing.Point(0, 8);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(600, 43);
            this.txtExportPath.TabIndex = 0;

            // btnBrowseExportPath
            this.btnBrowseExportPath.Location = new System.Drawing.Point(620, 8);
            this.btnBrowseExportPath.Name = "btnBrowseExportPath";
            this.btnBrowseExportPath.Size = new System.Drawing.Size(160, 43);
            this.btnBrowseExportPath.TabIndex = 1;
            this.btnBrowseExportPath.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnBrowseExportPath.Values.Text = "浏览…";
            this.btnBrowseExportPath.Click += new System.EventHandler(this.BtnBrowseExportPath_Click);

            // lblFilePattern
            this.lblFilePattern.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFilePattern.Location = new System.Drawing.Point(33, 152);
            this.lblFilePattern.Name = "lblFilePattern";
            this.lblFilePattern.Size = new System.Drawing.Size(180, 42);
            this.lblFilePattern.TabIndex = 2;
            this.lblFilePattern.Values.Text = "文件名格式：";

            // txtFilePattern
            this.txtFilePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));
            this.txtFilePattern.Location = new System.Drawing.Point(253, 135);
            this.txtFilePattern.Name = "txtFilePattern";
            this.txtFilePattern.Size = new System.Drawing.Size(600, 43);
            this.txtFilePattern.TabIndex = 3;

            // lblAutoExport
            this.lblAutoExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAutoExport.Location = new System.Drawing.Point(33, 242);
            this.lblAutoExport.Name = "lblAutoExport";
            this.lblAutoExport.Size = new System.Drawing.Size(180, 42);
            this.lblAutoExport.TabIndex = 4;
            this.lblAutoExport.Values.Text = "查询后自动导出：";

            // chkAutoExport
            this.chkAutoExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAutoExport.Location = new System.Drawing.Point(253, 231);
            this.chkAutoExport.Name = "chkAutoExport";
            this.chkAutoExport.Size = new System.Drawing.Size(40, 43);
            this.chkAutoExport.TabIndex = 5;
            this.chkAutoExport.Values.Text = "";

            // pnlExportFooter
            this.pnlExportFooter.Controls.Add(this.btnSaveExport);
            this.pnlExportFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlExportFooter.Location = new System.Drawing.Point(0, 910);
            this.pnlExportFooter.Name = "pnlExportFooter";
            this.pnlExportFooter.Size = new System.Drawing.Size(1762, 100);
            this.pnlExportFooter.TabIndex = 1;

            // btnSaveExport
            this.btnSaveExport.Location = new System.Drawing.Point(30, 14);
            this.btnSaveExport.Name = "btnSaveExport";
            this.btnSaveExport.Size = new System.Drawing.Size(200, 68);
            this.btnSaveExport.TabIndex = 0;
            this.btnSaveExport.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveExport.Values.Text = "保存";
            this.btnSaveExport.Click += new System.EventHandler(this.BtnSaveExport_Click);

            // ==================================================================
            // GeneralConfigPanel (Form)
            // ==================================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1764, 1049);
            this.Controls.Add(this.kryptonNavigator1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "GeneralConfigPanel";
            this.Text = "系统配置";

            // ── Resume / EndInit ──────────────────────────────────────────────
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage2.ResumeLayout(false);
            this.kryptonPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLeftHeader.ResumeLayout(false);
            this.pnlLeftHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeftHeader)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).EndInit();
            this.tlpFields.ResumeLayout(false);
            this.tlpFields.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel4)).EndInit();
            this.tlpGeneral.ResumeLayout(false);
            this.tlpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTheme)).EndInit();
            this.pnlGeneralFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGeneralFooter)).EndInit();
            this.tlpExport.ResumeLayout(false);
            this.tlpExport.PerformLayout();
            this.pnlExportPath.ResumeLayout(false);
            this.pnlExportPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlExportPath)).EndInit();
            this.pnlExportFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlExportFooter)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        // ── Navigator ─────────────────────────────────────────────────────────
        private Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private Krypton.Navigator.KryptonPage kryptonPage1;
        private Krypton.Navigator.KryptonPage kryptonPage2;
        private Krypton.Navigator.KryptonPage kryptonPage3;

        // ── Tab 1: Template Config ────────────────────────────────────────────
        private System.Windows.Forms.SplitContainer splitMain;
        private Krypton.Toolkit.KryptonListBox lstTemplates;
        private Krypton.Toolkit.KryptonPanel pnlLeftHeader;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonLabel lblTemplatesHeader;
        private Krypton.Toolkit.KryptonPanel pnlFooter;
        private Krypton.Toolkit.KryptonButton btnSave;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnClone;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private Krypton.Toolkit.KryptonPanel pnlDetail;
        private System.Windows.Forms.TableLayoutPanel tlpFields;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Krypton.Toolkit.KryptonLabel labelDesc;
        private Krypton.Toolkit.KryptonTextBox txtDesc;
        private Krypton.Toolkit.KryptonLabel labelTags;
        private Krypton.Toolkit.KryptonPanel panel4;
        private Krypton.Toolkit.KryptonTextBox txtTagSummary;
        private Krypton.Toolkit.KryptonButton btnSelectTags;
        private Krypton.Toolkit.KryptonButton btnConfigTag;

        // ── Tab 2: General Settings ───────────────────────────────────────────
        private System.Windows.Forms.TableLayoutPanel tlpGeneral;
        private Krypton.Toolkit.KryptonLabel lblCompanyName;
        private Krypton.Toolkit.KryptonTextBox txtCompanyName;
        private Krypton.Toolkit.KryptonLabel lblLanguage;
        private Krypton.Toolkit.KryptonComboBox cmbLanguage;
        private Krypton.Toolkit.KryptonLabel lblTheme;
        private Krypton.Toolkit.KryptonThemeComboBox cmbTheme;
        private Krypton.Toolkit.KryptonPanel pnlGeneralFooter;
        private Krypton.Toolkit.KryptonButton btnSaveGeneral;

        // ── Tab 3: Export Settings ────────────────────────────────────────────
        private System.Windows.Forms.TableLayoutPanel tlpExport;
        private Krypton.Toolkit.KryptonLabel lblExportPath;
        private Krypton.Toolkit.KryptonPanel pnlExportPath;
        private Krypton.Toolkit.KryptonTextBox txtExportPath;
        private Krypton.Toolkit.KryptonButton btnBrowseExportPath;
        private Krypton.Toolkit.KryptonLabel lblFilePattern;
        private Krypton.Toolkit.KryptonTextBox txtFilePattern;
        private Krypton.Toolkit.KryptonLabel lblAutoExport;
        private Krypton.Toolkit.KryptonCheckBox chkAutoExport;
        private Krypton.Toolkit.KryptonPanel pnlExportFooter;
        private Krypton.Toolkit.KryptonButton btnSaveExport;
    }
}
