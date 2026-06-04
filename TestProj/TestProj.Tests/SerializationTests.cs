using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TestProj.Models;
using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace TestProj.Tests
{
    /// <summary>
    /// JSON serialization round-trip tests for all model classes.
    /// Verifies that data survives a save → load cycle with no loss or corruption,
    /// and that backward-compatibility (old JSON fields we removed) is handled.
    /// </summary>
    [TestFixture]
    public class SerializationTests
    {
        private static T RoundTrip<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return JsonConvert.DeserializeObject<T>(json);
        }

        // ════════════════════════════════════════════════════════════════════
        //  TagMeta
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void TagMeta_RoundTrip_AllFieldsPreserved()
        {
            var original = new TagMeta
            {
                ValueID       = 12345L,
                TagName       = "T1",
                DisplayName   = "原料罐温度",
                DisplayNameEn = "Raw Material Tank Temp",
                DecimalPlaces = 3
            };

            var restored = RoundTrip(original);

            Assert.AreEqual(12345L,                    restored.ValueID);
            Assert.AreEqual("T1",                      restored.TagName);
            Assert.AreEqual("原料罐温度",              restored.DisplayName);
            Assert.AreEqual("Raw Material Tank Temp",  restored.DisplayNameEn);
            Assert.AreEqual(3,                         restored.DecimalPlaces);
        }

        [Test]
        public void TagMeta_DefaultDecimalPlaces_Is2()
        {
            var meta     = new TagMeta { ValueID = 1, TagName = "T1" };
            var restored = RoundTrip(meta);
            Assert.AreEqual(2, restored.DecimalPlaces);
        }

        [Test]
        public void TagMeta_DataType_IsIgnoredDuringSerialization()
        {
            // DataType is decorated with [JsonIgnore] — it must not appear in JSON
            var meta = new TagMeta { ValueID = 1, TagName = "T1", DataType = "Double" };
            string json = JsonConvert.SerializeObject(meta);
            Assert.IsFalse(json.Contains("DataType"),
                "DataType is [JsonIgnore] and must not be serialized");
        }

        [Test]
        public void TagMeta_OldJson_WithNormalMinMax_DeserializesWithoutError()
        {
            // Backward compatibility: old config files still contain NormalMin/NormalMax.
            // After our cleanup those properties no longer exist on the model,
            // but Newtonsoft.Json should silently ignore unknown fields.
            const string oldJson = @"{
                ""ValueID"": 99,
                ""TagName"": ""OldTag"",
                ""NormalMin"": 0.0,
                ""NormalMax"": 100.0,
                ""DecimalPlaces"": 2
            }";

            TagMeta meta = null;
            Assert.DoesNotThrow(() => meta = JsonConvert.DeserializeObject<TagMeta>(oldJson),
                "Deserializing JSON with removed fields should not throw");
            Assert.IsNotNull(meta);
            Assert.AreEqual(99L,       meta.ValueID);
            Assert.AreEqual("OldTag",  meta.TagName);
            Assert.AreEqual(2,         meta.DecimalPlaces);
        }

        // ════════════════════════════════════════════════════════════════════
        //  ReportTemplate
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void ReportTemplate_RoundTrip_AllFieldsPreserved()
        {
            var original = new ReportTemplate
            {
                Id          = "tpl_123456",
                DisplayName = "温度报表",
                Description = "所有温度变量",
                ExportPath  = @"D:\Reports\Temperature",
                SelectedTags = new List<TemplateTagEntry>
                {
                    new TemplateTagEntry { ValueID = 10L, Order = 1 },
                    new TemplateTagEntry { ValueID = 20L, Order = 2 },
                    new TemplateTagEntry { ValueID = 30L, Order = 3 }
                }
            };

            var restored = RoundTrip(original);

            Assert.AreEqual("tpl_123456",             restored.Id);
            Assert.AreEqual("温度报表",               restored.DisplayName);
            Assert.AreEqual("所有温度变量",           restored.Description);
            Assert.AreEqual(@"D:\Reports\Temperature", restored.ExportPath);
            Assert.AreEqual(3,                         restored.SelectedTags.Count);
            Assert.AreEqual(10L,                       restored.SelectedTags[0].ValueID);
            Assert.AreEqual(1,                         restored.SelectedTags[0].Order);
            Assert.AreEqual(30L,                       restored.SelectedTags[2].ValueID);
            Assert.AreEqual(3,                         restored.SelectedTags[2].Order);
        }

        [Test]
        public void ReportTemplate_EmptyTags_RoundTrip()
        {
            var original = new ReportTemplate { Id = "t1", DisplayName = "Empty" };
            var restored = RoundTrip(original);
            Assert.IsNotNull(restored.SelectedTags);
            Assert.AreEqual(0, restored.SelectedTags.Count);
        }

        [Test]
        public void ReportTemplate_ExportPath_DefaultIsEmpty()
        {
            var tpl = new ReportTemplate();
            Assert.AreEqual(string.Empty, tpl.ExportPath);
        }

        // ════════════════════════════════════════════════════════════════════
        //  TemplateTagEntry
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void TemplateTagEntry_RoundTrip_PreservesValueIdAndOrder()
        {
            var entry    = new TemplateTagEntry { ValueID = 9876543210L, Order = 42 };
            var restored = RoundTrip(entry);
            Assert.AreEqual(9876543210L, restored.ValueID);
            Assert.AreEqual(42,          restored.Order);
        }

        // ════════════════════════════════════════════════════════════════════
        //  DecimalRule
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void DecimalRule_RoundTrip_PreservesPrefixAndPlaces()
        {
            var rule     = new DecimalRule { Prefix = "TempSensor_", DecimalPlaces = 2 };
            var restored = RoundTrip(rule);
            Assert.AreEqual("TempSensor_", restored.Prefix);
            Assert.AreEqual(2,              restored.DecimalPlaces);
        }

        [Test]
        public void DecimalRule_Defaults_AreCorrect()
        {
            var rule = new DecimalRule();
            Assert.AreEqual(string.Empty, rule.Prefix);
            Assert.AreEqual(2,            rule.DecimalPlaces);
        }

        // ════════════════════════════════════════════════════════════════════
        //  ProjectConfig (full document round-trip)
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void ProjectConfig_RoundTrip_ComplexDocument()
        {
            var original = new ProjectConfig
            {
                CompanyName       = "中源技术",
                Theme             = "Microsoft365Blue",
                Language          = "zh-CN",
                AutoExport        = false,
                MaxQueryDays      = 31,
                MaxDataPoints     = 50000,
                AutoConnectBackup = true,
                BackupPath        = @"E:\WinCCBackup",
                TagMeta = new List<TagMeta>
                {
                    new TagMeta { ValueID = 1, TagName = "T1", DisplayName = "温度1", DecimalPlaces = 2 },
                    new TagMeta { ValueID = 2, TagName = "T2", DisplayName = "温度2", DecimalPlaces = 3 }
                },
                Template = new List<ReportTemplate>
                {
                    new ReportTemplate
                    {
                        Id          = "tpl_001",
                        DisplayName = "温度报表",
                        SelectedTags = new List<TemplateTagEntry>
                        {
                            new TemplateTagEntry { ValueID = 1, Order = 1 },
                            new TemplateTagEntry { ValueID = 2, Order = 2 }
                        }
                    }
                },
                DecimalRules = new List<DecimalRule>
                {
                    new DecimalRule { Prefix = "T", DecimalPlaces = 2 },
                    new DecimalRule { Prefix = "P", DecimalPlaces = 3 }
                }
            };

            var restored = RoundTrip(original);

            // Scalars
            Assert.AreEqual("中源技术",          restored.CompanyName);
            Assert.AreEqual("Microsoft365Blue",  restored.Theme);
            Assert.AreEqual("zh-CN",             restored.Language);
            Assert.IsFalse(restored.AutoExport);
            Assert.AreEqual(31,                  restored.MaxQueryDays);
            Assert.AreEqual(50000,               restored.MaxDataPoints);
            Assert.IsTrue(restored.AutoConnectBackup);
            Assert.AreEqual(@"E:\WinCCBackup",   restored.BackupPath);

            // TagMeta
            Assert.AreEqual(2, restored.TagMeta.Count);
            Assert.AreEqual("温度1", restored.TagMeta[0].DisplayName);
            Assert.AreEqual(3,       restored.TagMeta[1].DecimalPlaces);

            // Templates
            Assert.AreEqual(1,         restored.Template.Count);
            Assert.AreEqual("tpl_001", restored.Template[0].Id);
            Assert.AreEqual(2,         restored.Template[0].SelectedTags.Count);

            // DecimalRules
            Assert.AreEqual(2,   restored.DecimalRules.Count);
            Assert.AreEqual("P", restored.DecimalRules[1].Prefix);
            Assert.AreEqual(3,   restored.DecimalRules[1].DecimalPlaces);
        }

        [Test]
        public void ProjectConfig_Defaults_AreCorrect()
        {
            var cfg = new ProjectConfig();

            Assert.AreEqual(string.Empty,        cfg.CompanyName);
            Assert.AreEqual("Microsoft365Blue",  cfg.Theme);
            Assert.AreEqual("zh-CN",             cfg.Language);
            Assert.IsFalse(cfg.AutoExport);
            Assert.AreEqual(31,                  cfg.MaxQueryDays);
            Assert.AreEqual(50000,               cfg.MaxDataPoints);
            Assert.IsFalse(cfg.AutoConnectBackup);
            Assert.AreEqual(string.Empty,        cfg.BackupPath);
            Assert.IsNotNull(cfg.TagMeta);
            Assert.IsNotNull(cfg.Template);
            Assert.IsNotNull(cfg.DecimalRules);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Unicode / special characters
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void TagMeta_ChineseDisplayName_SurvivedRoundTrip()
        {
            string[] names = { "原料罐温度", "E-01冷进温度", "V-04料液温度" };

            foreach (string name in names)
            {
                var meta     = new TagMeta { DisplayName = name };
                var restored = RoundTrip(meta);
                Assert.AreEqual(name, restored.DisplayName, $"Chinese name '{name}' was corrupted");
            }
        }

        [Test]
        public void TagMeta_LargeValueId_PreservedExactly()
        {
            // WinCC ValueIDs can be large 64-bit integers
            long largeId = 9876543210123L;
            var  meta     = new TagMeta { ValueID = largeId };
            var  restored = RoundTrip(meta);
            Assert.AreEqual(largeId, restored.ValueID);
        }
    }
}
