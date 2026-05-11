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

            MigrateValueIDs();
        }

        /// <summary>
        /// 一次性迁移：将旧 config 里缺失的 ValueID 从数据库补全
        /// </summary>
        private static void MigrateValueIDs()
        {
            bool tagMetaNeedsMigration = _config.TagMeta.Any(t => t.ValueID == 0 && !string.IsNullOrEmpty(t.TagName));
            bool entriesNeedMigration  = _config.Template.SelectMany(t => t.SelectedTags).Any(e => e.ValueID == 0);

            if (!tagMetaNeedsMigration && !entriesNeedMigration)
                return;

            var dbTags = DbManager.FetchAllTags();
            var nameToId = dbTags.ToDictionary(t => t.ValueName, t => t.ValueID);

            if (tagMetaNeedsMigration)
            {
                foreach (var meta in _config.TagMeta)
                    if (meta.ValueID == 0 && nameToId.TryGetValue(meta.TagName, out long id))
                        meta.ValueID = id;
            }

            if (entriesNeedMigration)
            {
                var metaDict = _config.TagMeta
                    .Where(m => m.ValueID != 0)
                    .ToDictionary(m => m.TagName, m => m.ValueID);

                foreach (var entry in _config.Template.SelectMany(t => t.SelectedTags))
                {
                    if (entry.ValueID == 0 && metaDict.TryGetValue(entry.TagName ?? "", out long id))
                        entry.ValueID = id;
                    entry.TagName = null; // 清除迁移字段，不再写入 JSON
                }
            }

            SaveConfig();
        }

        public static void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(_config, Formatting.Indented);
            File.WriteAllText(_configPath, json, new UTF8Encoding(false));
        }
    }
}