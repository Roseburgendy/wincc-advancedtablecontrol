using System;
using System.Windows.Forms;

namespace TestProj
{
    public partial class ExcelImportConfigForm : Krypton.Toolkit.KryptonForm
    {
        public int DataStartRow    => (int)numStartRow.Value;
        public int TagNameCol      => (int)numTagCol.Value;
        public int DisplayNameCol  => (int)numDisplayCol.Value;
        /// <summary>0 means "not specified / skip"</summary>
        public int DisplayNameEnCol => (int)numDisplayEnCol.Value;

        public ExcelImportConfigForm()
        {
            InitializeComponent();
            ApplyLanguage();
        }

        /// <summary>刷新窗体所有 UI 文字为当前语言。</summary>
        public void ApplyLanguage()
        {
            this.Text         = LangHelper.Get("EIC_Title");
            label1.Values.Text = LangHelper.Get("EIC_StartRow");
            label2.Values.Text = LangHelper.Get("EIC_TagCol");
            label3.Values.Text = LangHelper.Get("EIC_DisplayCol");
            label4.Values.Text = LangHelper.Get("EIC_DisplayEnCol");
            btnOK.Values.Text  = LangHelper.Get("EIC_BtnOK");
            btnCancel.Values.Text = LangHelper.Get("EIC_BtnCancel");
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
