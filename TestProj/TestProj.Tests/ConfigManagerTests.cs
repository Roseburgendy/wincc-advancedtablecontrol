using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using TestProj.Models;

namespace TestProj.Tests
{
    /// <summary>
    /// Tests for ConfigManager: JSON persistence round-trips and default creation.
    /// Uses a temporary directory so tests never touch the real project config.
    /// </summary>
    [TestFixture]
    public class ConfigManagerTests
    {
        private string _tempDir;

        [SetUp]
        public void SetUp()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "CfgTest_" + Guid.NewGuid().ToString("N").Substring(0, 8));
            Directory.CreateDirectory(_tempDir);

            // ConfigManager.Init creates: {_tempDir}\WinCCReporter\test.json
            ConfigManager.Init(_tempDir, "test");
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, recursive: true);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Init / Load default
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void Init_NoExistingFile_CreatesConfigWithDefaults()
        {
            Assert.IsNotNull(ConfigManager.Config);
            Assert.IsNotNull(ConfigManager.Config.TagMeta);
            Assert.IsNotNull(ConfigManager.Config.Template);
        }

        [Test]
        public void Init_NoExistingFile_CreatesJsonFile()
        {
            string expectedPath = Path.Combine(_tempDir, "WinCCReporter", "test.json");
            Assert.IsTrue(File.Exists(expectedPath), "Config file should be created on first Init");
        }

        [Test]
        public void Init_ExistingFile_LoadsCorrectly()
        {
            // Write a config value, save, re-init, verify it was read back
            ConfigManager.Config.CompanyName = "Acme Corp";
            ConfigManager.SaveConfig();

            ConfigManager.Init(_tempDir, "test");

            Assert.AreEqual("Acme Corp", ConfigManager.Config.CompanyName);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Round-trip: scalar fields
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void RoundTrip_ScalarFields_AllPreserved()
        {
            var cfg = ConfigManager.Config;
            cfg.CompanyName       = "Test Co.";
            cfg.Language          = "en-US";
            cfg.Theme             = "Microsoft365Black";
            cfg.AutoExport        = true;
            cfg.MaxQueryDays      = 90;
            cfg.MaxDataPoints     = 10000;
            cfg.AutoConnectBackup = true;
            cfg.BackupPath        = @"E:\Backup";

            ConfigManager.SaveConfig();
            ConfigManager.Init(_tempDir, "test");
            cfg = ConfigManager.Config;

            Assert.AreEqual("Test Co.",            cfg.CompanyName);
            Assert.AreEqual("en-US",               cfg.Language);
            Assert.AreEqual("Microsoft365Black",   cfg.Theme);
            Assert.IsTrue(cfg.AutoExport);
            Assert.AreEqual(90,                    cfg.MaxQueryDays);
            Assert.AreEqual(10000,                 cfg.MaxDataPoints);
            Assert.IsTrue(cfg.AutoConnectBackup);
            Assert.AreEqual(@"E:\Backup",          cfg.BackupPath);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Round-trip: TagMeta list
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void RoundTrip_TagMeta_PreservesAllFields()
        {
            ConfigManager.Config.TagMeta = new List<TagMeta>
            {
                new TagMeta
                {
                    ValueID       = 42L,
                    TagName       = "T1",
                    DisplayName   = "原料罐温度",
                    DisplayNameEn = "Raw Material Tank Temp",
                    DecimalPlaces = 3
                }
            };

            ConfigManager.SaveConfig();
            ConfigManager.Init(_tempDir, "test");

            var meta = ConfigManager.Config.TagMeta;
            Assert.AreEqual(1, meta.Count);
            Assert.AreEqual(42L,                    meta[0].ValueID);
            Assert.AreEqual("T1",                   meta[0].TagName);
            Assert.AreEqual("原料罐温度",            meta[0].DisplayName);
            Assert.AreEqual("Raw Material Tank Temp", meta[0].DisplayNameEn);
            Assert.AreEqual(3,                      meta[0].DecimalPlaces);
        }

        [Test]
        public void RoundTrip_TagMeta_NoNormalMinMax_InJson()
        {
            // After our cleanup, NormalMin/Max were removed. Old JSON that still
            // contains these fields should be tolerated (unknown fields ignored).
            string configDir  = Path.Combine(_tempDir, "WinCCReporter");
            string configFile = Path.Combine(configDir, "test.json");
            Directory.CreateDirectory(configDir);

            // Inject old-style JSON with NormalMin/Max
            File.WriteAllText(configFile, @"{
  ""TagMeta"": [
    {
      ""ValueID"": 1,
      ""TagName"": ""T1"",
      ""NormalMin"": 0.0,
      ""NormalMax"": 100.0,
      ""DecimalPlaces"": 2
    }
  ],
  ""Template"": []
}");

            ConfigManager.Init(_tempDir, "test");

            // Should load without error and simply ignore unknown fields
            Assert.IsNotNull(ConfigManager.Config);
            Assert.AreEqual(1, ConfigManager.Config.TagMeta.Count);
            Assert.AreEqual("T1", ConfigManager.Config.TagMeta[0].TagName);
        }

        [Test]
        public void RoundTrip_MultipleTagMeta_OrderPreserved()
        {
            var tags = new List<TagMeta>
            {
                new TagMeta { ValueID = 1, TagName = "T1", DecimalPlaces = 2 },
                new TagMeta { ValueID = 2, TagName = "T2", DecimalPlaces = 3 },
                new TagMeta { ValueID = 3, TagName = "T3", DecimalPlaces = 0 },
            };
            ConfigManager.Config.TagMeta = tags;

            ConfigManager.SaveConfig();
            ConfigManager.Init(_tempDir, "test");

            var loaded = ConfigManager.Config.TagMeta;
            Assert.AreEqual(3, loaded.Count);
            Assert.AreEqual(1L, loaded[0].ValueID);
            Assert.AreEqual(2L, loaded[1].ValueID);
            Assert.AreEqual(3L, loaded[2].ValueID);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Round-trip: ReportTemplate list
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void RoundTrip_Templates_PreservesAllFields()
        {
            ConfigManager.Config.Template = new List<ReportTemplate>
            {
                new ReportTemplate
                {
                    Id          = "tpl_001",
                    DisplayName = "温度报表",
                    Description = "所有温度变量",
                    ExportPath  = @"D:\Reports",
                    SelectedTags = new List<TemplateTagEntry>
                    {
                        new TemplateTagEntry { ValueID = 10L, Order = 1 },
                        new TemplateTagEntry { ValueID = 20L, Order = 2 }
                    }
                }
            };

            ConfigManager.SaveConfig();
            ConfigManager.Init(_tempDir, "test");

            var tpls = ConfigManager.Config.Template;
            Assert.AreEqual(1,        tpls.Count);
            Assert.AreEqual("tpl_001",  tpls[0].Id);
            Assert.AreEqual("温度报表", tpls[0].DisplayName);
            Assert.AreEqual("所有温度变量", tpls[0].Description);
            Assert.AreEqual(@"D:\Reports", tpls[0].ExportPath);
            Assert.AreEqual(2,        tpls[0].SelectedTags.Count);
            Assert.AreEqual(10L,      tpls[0].SelectedTags[0].ValueID);
            Assert.AreEqual(1,        tpls[0].SelectedTags[0].Order);
        }

        [Test]
        public void RoundTrip_MultipleTemplates_AllPreserved()
        {
            ConfigManager.Config.Template = new List<ReportTemplate>
            {
                new ReportTemplate { Id = "t1", DisplayName = "温度" },
                new ReportTemplate { Id = "t2", DisplayName = "压力" },
                new ReportTemplate { Id = "t3", DisplayName = "流量" }
            };

            ConfigManager.SaveConfig();
            ConfigManager.Init(_tempDir, "test");

            Assert.AreEqual(3, ConfigManager.Config.Template.Count);
            Assert.AreEqual("t1", ConfigManager.Config.Template[0].Id);
            Assert.AreEqual("t3", ConfigManager.Config.Template[2].Id);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Round-trip: DecimalRules
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void RoundTrip_DecimalRules_PreservesAll()
        {
            ConfigManager.Config.DecimalRules = new List<DecimalRule>
            {
                new DecimalRule { Prefix = "T",   DecimalPlaces = 2 },
                new DecimalRule { Prefix = "P",   DecimalPlaces = 3 },
                new DecimalRule { Prefix = "Flow", DecimalPlaces = 1 }
            };

            ConfigManager.SaveConfig();
            ConfigManager.Init(_tempDir, "test");

            var rules = ConfigManager.Config.DecimalRules;
            Assert.AreEqual(3,      rules.Count);
            Assert.AreEqual("T",    rules[0].Prefix);
            Assert.AreEqual(2,      rules[0].DecimalPlaces);
            Assert.AreEqual("Flow", rules[2].Prefix);
            Assert.AreEqual(1,      rules[2].DecimalPlaces);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Edge cases
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void SaveConfig_ProducesValidUtf8WithoutBom()
        {
            ConfigManager.Config.CompanyName = "中文名称";
            ConfigManager.SaveConfig();

            string configFile = Path.Combine(_tempDir, "WinCCReporter", "test.json");
            byte[] raw = File.ReadAllBytes(configFile);

            // UTF-8 without BOM: first byte must NOT be 0xEF (BOM start)
            Assert.AreNotEqual(0xEF, raw[0], "Config file must not have a UTF-8 BOM");
        }

        [Test]
        public void SaveConfig_JsonIsPrettyPrinted()
        {
            ConfigManager.SaveConfig();

            string json = File.ReadAllText(Path.Combine(_tempDir, "WinCCReporter", "test.json"));

            // Indented JSON will always have newline characters
            Assert.IsTrue(json.Contains(Environment.NewLine) || json.Contains("\n"),
                "Config JSON should be formatted with indentation");
        }

        [Test]
        public void ConfigPath_MatchesExpectedPattern()
        {
            string expected = Path.Combine(_tempDir, "WinCCReporter", "test.json");
            Assert.AreEqual(expected, ConfigManager.ConfigPath);
        }
    }
}
