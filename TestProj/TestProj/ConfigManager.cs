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

        private static readonly string ConfigPath = @"C:\Users\Public\Documents\config.json";

        public static void LoadConfig()
        {
            // 如果不存在配置文件
            if (!File.Exists(ConfigPath))
            {
                _config = new ProjectConfig
                {
                    DataSource = new DataSourceConfig
                    {

                    },
                    TagMeta = new List<TagMeta>()
                };
            }
            // search for json under Document Folder
            // TODO : Specify config path on installation or first run
            string json = File.ReadAllText(ConfigPath, new UTF8Encoding(false));
            _config = JsonConvert.DeserializeObject<ProjectConfig>(json);
        }

        public static void SaveConfig()
        {
            string path = @"C:\Users\Public\Documents\config.json";
            string json = JsonConvert.SerializeObject(_config, Formatting.Indented);
            File.WriteAllText(path, json, new UTF8Encoding(false));
        }
    }
}
