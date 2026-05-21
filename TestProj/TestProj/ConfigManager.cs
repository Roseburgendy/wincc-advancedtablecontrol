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
        /// 根据 WinCC 项目目录和项目名称初始化，配置文件位于
        /// {projectPath}\WinCCReporter\{projectName}.json。
        /// 路径完全由 WinCC 运行时标签决定，无需额外存储。
        /// </summary>
        public static void Init(string projectPath, string projectName)
        {
            string dir  = Path.Combine(projectPath, "WinCCReporter");
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
            Directory.CreateDirectory(Path.GetDirectoryName(_configPath));
            string json = JsonConvert.SerializeObject(_config, Formatting.Indented);
            File.WriteAllText(_configPath, json, new UTF8Encoding(false));
        }
    }
}