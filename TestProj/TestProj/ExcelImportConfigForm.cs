using System;
using System.Windows.Forms;

namespace TestProj
{
    public partial class ExcelImportConfigForm : Krypton.Toolkit.KryptonForm
    {
        public int DataStartRow => (int)numStartRow.Value;
        public int TagNameCol => (int)numTagCol.Value;
        public int DisplayNameCol => (int)numDisplayCol.Value;

        public ExcelImportConfigForm()
        {
            InitializeComponent();
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
