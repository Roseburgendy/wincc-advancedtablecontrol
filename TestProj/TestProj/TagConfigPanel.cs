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
    public partial class TagConfigPanel : UserControl
    {
        private List<string> _dbTags = new List<string>();
        // 存储用户改了但是没有写入JSON的变量配置（变量名做key)
        private Dictionary<string, Models.TagMeta> _pendingMeta = new Dictionary<string, Models.TagMeta>(); 

        public TagConfigPanel()
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
            //_dbTags = DbManager.FetchAllTags(archiveName);
            listBoxTag.Items.Clear();

            // display tags in listbox
            foreach (var dbTag in _dbTags)
            {
                listBoxTag.Items.Add(dbTag);
            }
        }
        private void listBoxTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = listBoxTag.SelectedItem.ToString().Replace("*","").Trim();
            
            string tagName = selected.Contains("\\") ? selected.Split('\\').Last() : selected;

            // look up in json
            var existConfig = ConfigManager.Config.TagMeta?.FirstOrDefault(t => t.TagName == tagName);

            // populate the fields
            txtTagName.Text = tagName;
            txtDisplayName.Text = existConfig?.DisplayName ?? tagName;
            txtMin.Text = existConfig?.NormalMin.ToString() ?? "0";
            txtMax.Text = existConfig?.NormalMax.ToString() ?? "100";
        }

        private void saveConfigBtn_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TagConfig_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
