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

        private static string _configPath;

        /// <summary>
        /// 根据项目名称初始化，每个项目对应一个独立的 config.json
        /// </summary>
        public static void Init(string projectName)
        {
            string dir = @"C:\Users\Public\Documents\WinCCReporter";
            _configPath = Path.Combine(dir, $"{projectName}.json");
            LoadConfig();
        }

        public static void LoadConfig()
        {
            if (!File.Exists(_configPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_configPath));
                _config = new ProjectConfig
                {
                    TagMeta = new List<TagMeta>()
                };
                SaveConfig();
                return;
            }

            string json = File.ReadAllText(_configPath, new UTF8Encoding(false));
            _config = JsonConvert.DeserializeObject<ProjectConfig>(json);

            if (_config.TagMeta == null)
                _config.TagMeta = new List<TagMeta>();
            if (_config.Template == null)
                _config.Template = new List<ReportTemplate>();

        }
        public static void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(_config, Formatting.Indented);
            File.WriteAllText(_configPath, json, new UTF8Encoding(false));
        }
    }
}