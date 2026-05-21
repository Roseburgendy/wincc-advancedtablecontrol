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

        /// <summary>当前配置文件的完整路径。</summary>
        public static string ConfigPath => _configPath;

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
            Directory.CreateDirectory(Path.GetDirectoryName(_configPath));
            File.WriteAllText(_configPath, json, new UTF8Encoding(false));
        }

        /// <summary>
        /// 将配置文件迁移到新路径并更新 _configPath。
        /// 若新路径与当前相同则只做保存。
        /// </summary>
        public static void MoveConfigTo(string newPath)
        {
            if (string.IsNullOrWhiteSpace(newPath)) return;
            newPath = Path.GetFullPath(newPath);

            if (!string.Equals(newPath, _configPath, System.StringComparison.OrdinalIgnoreCase))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                // Save to new location first, then remove old file
                string json = JsonConvert.SerializeObject(_config, Formatting.Indented);
                File.WriteAllText(newPath, json, new UTF8Encoding(false));

                if (File.Exists(_configPath))
                    File.Delete(_configPath);

                _configPath = newPath;
            }
            else
            {
                SaveConfig();
            }
        }
    }
}