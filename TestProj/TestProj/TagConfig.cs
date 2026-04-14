using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProj
{
    public partial class TagConfig : UserControl
    {
        private List<string> _dbTags = new List<string>();
        public TagConfig()
        {
            InitializeComponent();
            LoadData(); // load automatically
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            string archiveName = ConfigManager.Config.DataSource.ArchiveName;

            // read all tags via dbmanager'
            _dbTags = DbManager.FetchAllTags(archiveName);
            listBoxTag.Items.Clear();

            // display tags in listbox
            foreach (var dbTag in _dbTags)
            {
                listBoxTag.Items.Add(dbTag);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
