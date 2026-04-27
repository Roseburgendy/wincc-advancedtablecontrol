using System.Collections.Generic;
using System.IO;
using System.Text;
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
            if (!File.Exists(ConfigPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));
                _config = new ProjectConfig
                {
                    TagMeta = new List<TagMeta>()
                };
                SaveConfig();
                return;
            }

            string json = File.ReadAllText(ConfigPath, new UTF8Encoding(false));
            _config = JsonConvert.DeserializeObject<ProjectConfig>(json);
        }

        public static void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(_config, Formatting.Indented);
            File.WriteAllText(ConfigPath, json, new UTF8Encoding(false));
        }
    }
}