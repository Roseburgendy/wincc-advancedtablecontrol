using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using WINCCReporter.Models;

namespace WINCCReporter
{
    internal class ConfigManager
    {
        private static ProjectConfig _config;
        public static ProjectConfig Config => _config;

        private static string _configPath;

        /// <summary>当前配置文件的完整路径。</summary>
        public static string ConfigPath => _configPath;

        /// <summary>
        /// 用户手动选择的配置路径持久化到此文件，下次启动时优先读取。
        /// 存放在 AppData\Roaming\WINCCReporter\ 下，不随 WinCC 项目路径变化。
        /// </summary>
        private static readonly string _redirectFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WINCCReporter", "selectedconfig.txt");

        /// <summary>
        /// 根据 WinCC 项目目录和项目名称初始化。
        /// 若用户之前手动选过配置文件且该文件仍存在，则优先使用。
        /// </summary>
        public static void Init(string projectPath, string projectName)
        {
            string dir         = Path.Combine(projectPath, "GraCS", "WINCCReporterJSON");
            string defaultPath = Path.Combine(dir, $"{projectName}.json");

            // 优先使用用户上次手动选择的路径
            string saved = ReadRedirect();
            _configPath = (saved != null && File.Exists(saved)) ? saved : defaultPath;

            LoadConfig();
        }

        /// <summary>手动加载指定路径，并将该路径持久化为下次默认。</summary>
        public static void LoadFromPath(string path)
        {
            _configPath = path;
            WriteRedirect(path);
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

        // ── redirect helpers ──────────────────────────────────────────────

        private static string ReadRedirect()
        {
            try
            {
                if (File.Exists(_redirectFile))
                    return File.ReadAllText(_redirectFile, Encoding.UTF8).Trim();
            }
            catch { }
            return null;
        }

        private static void WriteRedirect(string path)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_redirectFile));
                File.WriteAllText(_redirectFile, path, Encoding.UTF8);
            }
            catch { }
        }
    }
}
