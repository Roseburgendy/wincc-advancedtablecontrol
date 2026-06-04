using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TestProj.Tests
{
    /// <summary>
    /// Tests for LangHelper: locale switching and key fallback.
    /// Requires a valid ConfigManager.Config (language field), so each test
    /// sets up a minimal config via ConfigManager.Init.
    /// </summary>
    [TestFixture]
    public class LangHelperTests
    {
        private string _tempDir;

        [SetUp]
        public void SetUp()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "LangTest_" + Guid.NewGuid().ToString("N").Substring(0, 8));
            Directory.CreateDirectory(_tempDir);
            ConfigManager.Init(_tempDir, "test");
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, recursive: true);
        }

        private void SetLanguage(string lang)
        {
            ConfigManager.Config.Language = lang;
        }

        // ════════════════════════════════════════════════════════════════════
        //  Chinese (zh-CN) — default / neutral culture
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void Get_ZhCN_ReturnsChineseString()
        {
            SetLanguage("zh-CN");

            // "Msg_OK" should be "确定" in Chinese
            string result = LangHelper.Get("Msg_OK");
            Assert.AreEqual("确定", result);
        }

        [Test]
        public void Get_ZhCN_InfoMessage_IsCorrect()
        {
            SetLanguage("zh-CN");
            string result = LangHelper.Get("Msg_Info");
            Assert.AreEqual("提示", result);
        }

        [Test]
        public void Get_ZhCN_CancelKey_IsCorrect()
        {
            SetLanguage("zh-CN");
            string result = LangHelper.Get("Msg_Cancel");
            Assert.AreEqual("取消", result);
        }

        // ════════════════════════════════════════════════════════════════════
        //  English (en-US) — satellite resource
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void Get_EnUS_ReturnsEnglishString()
        {
            SetLanguage("en-US");

            string result = LangHelper.Get("Msg_OK");
            Assert.AreEqual("OK", result);
        }

        [Test]
        public void Get_EnUS_InfoMessage_IsCorrect()
        {
            SetLanguage("en-US");
            string result = LangHelper.Get("Msg_Info");
            Assert.AreEqual("Info", result);
        }

        [Test]
        public void Get_EnUS_CancelKey_IsCorrect()
        {
            SetLanguage("en-US");
            string result = LangHelper.Get("Msg_Cancel");
            Assert.AreEqual("Cancel", result);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Language switching at runtime
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void Get_SwitchingLanguage_ReturnsCorrectStringEachTime()
        {
            SetLanguage("zh-CN");
            string chinese = LangHelper.Get("Msg_OK");

            SetLanguage("en-US");
            string english = LangHelper.Get("Msg_OK");

            Assert.AreEqual("确定", chinese);
            Assert.AreEqual("OK",   english);
        }

        // ════════════════════════════════════════════════════════════════════
        //  Missing / null keys
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void Get_MissingKey_ReturnsKeyItself()
        {
            SetLanguage("zh-CN");
            string result = LangHelper.Get("Key_That_Does_Not_Exist_XYZ");
            Assert.AreEqual("Key_That_Does_Not_Exist_XYZ", result);
        }

        [Test]
        public void Get_NullConfig_DoesNotThrow()
        {
            // LangHelper should gracefully handle Config being null (falls back to zh-CN)
            Assert.DoesNotThrow(() => LangHelper.Get("Msg_OK"));
        }

        // ════════════════════════════════════════════════════════════════════
        //  Key coverage smoke tests — verify keys used in UI exist in both locales
        // ════════════════════════════════════════════════════════════════════

        [TestCase("zh-CN")]
        [TestCase("en-US")]
        public void Get_CoreUIKeys_NeverReturnKeyItself(string lang)
        {
            SetLanguage(lang);

            string[] coreKeys =
            {
                "Msg_OK", "Msg_Cancel", "Msg_Info", "Msg_Warning",
                "UC_ConnectBackup", "UC_DisconnectBackup",
                "Backup_ConnectProgressTitle", "Backup_DisconnectProgressTitle",
                "Backup_ConnectSuccess", "Backup_ConnectFailed",
                "Backup_DisconnectSuccessDetail", "Backup_DisconnectFailed",
                "GCP_Title", "GCP_Tab_Templates", "GCP_Tab_General",
                "GCP_BtnSave", "GCP_BtnCancel", "GCP_BtnAdd", "GCP_BtnDelete",
                "TCP_Title", "TCP_BtnSave",
            };

            foreach (string key in coreKeys)
            {
                string value = LangHelper.Get(key);
                Assert.AreNotEqual(key, value,
                    $"[{lang}] Key '{key}' was not found in the resource — it returned the key itself.");
            }
        }
    }
}
