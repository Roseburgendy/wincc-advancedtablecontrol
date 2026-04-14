using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestProj.Models;
namespace TestProj
{
    internal class ConfigManager
    {
        private static ProjectConfig _config;
        public static ProjectConfig Config => _config;

        public static void LoadConfig()
        {
            // search for json under Document Folder
            // TODO : Specify config path on installation or first run
            string path = @"C:\Users\Public\Documents\config.json";
            string json = File.ReadAllText(path, new UTF8Encoding(false));
            _config = JsonConvert.DeserializeObject<ProjectConfig>(json);
        }
    }
}
