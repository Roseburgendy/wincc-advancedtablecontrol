using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Assert = NUnit.Framework.Legacy.ClassicAssert;

namespace TestProj.Tests
{
    /// <summary>
    /// Tests for BackupManager's pure-logic methods.
    /// No WinCC COM calls are made — ConnectBackup / DisconnectBackup are
    /// excluded because they require a live HMIRuntime instance.
    /// </summary>
    [TestFixture]
    public class BackupManagerTests
    {
        private string _tempFolder;

        // ── Reflection helpers to access private static state ──────────────

        private static List<ConnectedArchiveEntry> ArchiveList =>
            (List<ConnectedArchiveEntry>)typeof(BackupManager)
                .GetField("_connectedArchives", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);

        private static void AddEntry(DateTime from, DateTime to, string name = null)
        {
            ArchiveList.Add(new ConnectedArchiveEntry
            {
                DisplayName = name ?? $"archive [{from:yyyy-MM-dd} ~ {to:yyyy-MM-dd}]",
                TimeFrom    = from,
                TimeTo      = to
            });
        }

        private static void AddEntryNoRange(string name = "whole-folder")
        {
            ArchiveList.Add(new ConnectedArchiveEntry
            {
                DisplayName = name,
                TimeFrom    = null,
                TimeTo      = null
            });
        }

        // ── Fixture setup / teardown ────────────────────────────────────────

        [SetUp]
        public void SetUp()
        {
            ArchiveList.Clear();
            _tempFolder = Path.Combine(Path.GetTempPath(), "BkMgrTest_" + Guid.NewGuid().ToString("N").Substring(0, 8));
            Directory.CreateDirectory(_tempFolder);
        }

        [TearDown]
        public void TearDown()
        {
            ArchiveList.Clear();
            if (Directory.Exists(_tempFolder))
                Directory.Delete(_tempFolder, recursive: true);
        }

        // ════════════════════════════════════════════════════════════════════
        //  ParseBackupTimeRange
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void ParseBackupTimeRange_ValidName_ReturnsCorrectUtcRange()
        {
            // File name format produced by WinCC: SomeName_TLG_F_yyyyMMddHHmm_yyyyMMddHHmm
            var result = BackupManager.ParseBackupTimeRange("Archiv_TLG_F_202401010000_202402010000");

            Assert.IsNotNull(result);
            Assert.AreEqual(new DateTime(2024, 1,  1,  0, 0, 0, DateTimeKind.Utc), result.Value.Start);
            Assert.AreEqual(new DateTime(2024, 2,  1,  0, 0, 0, DateTimeKind.Utc), result.Value.End);
        }

        [Test]
        public void ParseBackupTimeRange_WithHourMinute_ParsedCorrectly()
        {
            var result = BackupManager.ParseBackupTimeRange("Fast_TLG_F_202306151430_202306152359");

            Assert.IsNotNull(result);
            Assert.AreEqual(new DateTime(2023, 6, 15, 14, 30, 0, DateTimeKind.Utc), result.Value.Start);
            Assert.AreEqual(new DateTime(2023, 6, 15, 23, 59, 0, DateTimeKind.Utc), result.Value.End);
        }

        [Test]
        public void ParseBackupTimeRange_NoTlgSuffix_ReturnsNull()
        {
            var result = BackupManager.ParseBackupTimeRange("SomeRandomFile");
            Assert.IsNull(result);
        }

        [Test]
        public void ParseBackupTimeRange_EmptyString_ReturnsNull()
        {
            var result = BackupManager.ParseBackupTimeRange(string.Empty);
            Assert.IsNull(result);
        }

        [Test]
        public void ParseBackupTimeRange_PartialSuffix_ReturnsNull()
        {
            // Missing second date group
            var result = BackupManager.ParseBackupTimeRange("Archiv_TLG_F_202401010000");
            Assert.IsNull(result);
        }

        [Test]
        public void ParseBackupTimeRange_InvalidDateDigits_ReturnsNull()
        {
            // 13th month is invalid
            var result = BackupManager.ParseBackupTimeRange("Archiv_TLG_F_202413010000_202414010000");
            Assert.IsNull(result);
        }

        [Test]
        public void ParseBackupTimeRange_ResultIsUtcKind()
        {
            var result = BackupManager.ParseBackupTimeRange("X_TLG_F_202301010000_202301310000");
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTimeKind.Utc, result.Value.Start.Kind);
            Assert.AreEqual(DateTimeKind.Utc, result.Value.End.Kind);
        }

        // ════════════════════════════════════════════════════════════════════
        //  NeedsConnection — folder validation
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void NeedsConnection_NullFolder_ReturnsFalse()
        {
            var result = BackupManager.NeedsConnection(null,
                new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            Assert.IsFalse(result);
        }

        [Test]
        public void NeedsConnection_EmptyFolder_ReturnsFalse()
        {
            var result = BackupManager.NeedsConnection(string.Empty,
                new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            Assert.IsFalse(result);
        }

        [Test]
        public void NeedsConnection_FolderDoesNotExist_ReturnsFalse()
        {
            var result = BackupManager.NeedsConnection(
                @"Z:\DoesNotExist\AtAll",
                new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            Assert.IsFalse(result);
        }

        // ════════════════════════════════════════════════════════════════════
        //  NeedsConnection — archive-list logic (uses real temp folder)
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void NeedsConnection_EmptyArchiveList_ReturnsTrue()
        {
            // Nothing connected yet → must connect
            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            Assert.IsTrue(result);
        }

        [Test]
        public void NeedsConnection_SingleEntryFullyCovers_ReturnsFalse()
        {
            AddEntry(new DateTime(2023, 12, 1), new DateTime(2024, 4, 1));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            Assert.IsFalse(result);
        }

        [Test]
        public void NeedsConnection_SingleEntryExactMatch_ReturnsFalse()
        {
            AddEntry(new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            Assert.IsFalse(result);
        }

        [Test]
        public void NeedsConnection_LeftGapNotCovered_ReturnsTrue()
        {
            // Connected: Feb–Apr.  Query: Jan–Mar.  Jan is not covered.
            AddEntry(new DateTime(2024, 2, 1), new DateTime(2024, 4, 1));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            Assert.IsTrue(result);
        }

        [Test]
        public void NeedsConnection_RightGapNotCovered_ReturnsTrue()
        {
            // Connected: Jan–Feb.  Query: Jan–Apr.  Mar–Apr not covered.
            AddEntry(new DateTime(2024, 1, 1), new DateTime(2024, 2, 1));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 4, 1));
            Assert.IsTrue(result);
        }

        [Test]
        public void NeedsConnection_TwoAdjacentEntriesFullyCover_ReturnsFalse()
        {
            // Jan–Feb + Feb–Apr together cover Jan–Apr
            AddEntry(new DateTime(2024, 1, 1), new DateTime(2024, 2, 1));
            AddEntry(new DateTime(2024, 2, 1), new DateTime(2024, 4, 1));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 4, 1));
            Assert.IsFalse(result);
        }

        [Test]
        public void NeedsConnection_TwoOverlappingEntriesFullyCover_ReturnsFalse()
        {
            // Jan–Mar + Feb–Apr overlap; together cover Jan–Apr
            AddEntry(new DateTime(2024, 1, 1), new DateTime(2024, 3, 1));
            AddEntry(new DateTime(2024, 2, 1), new DateTime(2024, 4, 1));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 4, 1));
            Assert.IsFalse(result);
        }

        [Test]
        public void NeedsConnection_GapBetweenTwoEntries_ReturnsTrue()
        {
            // Jan–Feb + Mar–Apr leaves a gap in February
            AddEntry(new DateTime(2024, 1, 1), new DateTime(2024, 2, 1));
            AddEntry(new DateTime(2024, 3, 1), new DateTime(2024, 4, 1));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 4, 1));
            Assert.IsTrue(result);
        }

        [Test]
        public void NeedsConnection_NoTimeRangeEntry_TreatedAsFullCoverage_ReturnsFalse()
        {
            // An entry with null TimeFrom/TimeTo was connected as "whole folder" — always covers
            AddEntryNoRange("E:\\backup\\");

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));
            Assert.IsFalse(result);
        }

        [Test]
        public void NeedsConnection_NonOverlappingEntry_ReturnsTrue()
        {
            // Connected: 2023 only.  Query: 2024.  No overlap at all.
            AddEntry(new DateTime(2023, 1, 1), new DateTime(2023, 12, 31));

            bool result = BackupManager.NeedsConnection(_tempFolder,
                new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));
            Assert.IsTrue(result);
        }

        // ════════════════════════════════════════════════════════════════════
        //  GetBackupFilesInFolder
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void GetBackupFilesInFolder_EmptyFolder_ReturnsEmptyList()
        {
            var files = BackupManager.GetBackupFilesInFolder(_tempFolder);
            Assert.IsEmpty(files);
        }

        [Test]
        public void GetBackupFilesInFolder_NonExistentFolder_ReturnsEmptyList()
        {
            var files = BackupManager.GetBackupFilesInFolder(@"Z:\Nonexistent\Path");
            Assert.IsEmpty(files);
        }

        [Test]
        public void GetBackupFilesInFolder_LdfFiles_AllReturned()
        {
            File.WriteAllBytes(Path.Combine(_tempFolder, "Arch_TLG_F_202401010000_202402010000.ldf"), new byte[0]);
            File.WriteAllBytes(Path.Combine(_tempFolder, "Arch_TLG_F_202402010000_202403010000.ldf"), new byte[0]);

            var files = BackupManager.GetBackupFilesInFolder(_tempFolder);

            Assert.AreEqual(2, files.Count);
        }

        [Test]
        public void GetBackupFilesInFolder_LdfFiles_TimeRangeParsed()
        {
            File.WriteAllBytes(Path.Combine(_tempFolder, "Arch_TLG_F_202401010000_202402010000.ldf"), new byte[0]);

            var files = BackupManager.GetBackupFilesInFolder(_tempFolder);

            Assert.AreEqual(1, files.Count);
            Assert.IsTrue(files[0].TimeRange.HasValue);
            Assert.AreEqual(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), files[0].TimeRange.Value.Start);
        }

        [Test]
        public void GetBackupFilesInFolder_MdfFallback_WhenNoLdfFiles()
        {
            File.WriteAllBytes(Path.Combine(_tempFolder, "Arch_TLG_F_202401010000_202402010000.mdf"), new byte[0]);

            var files = BackupManager.GetBackupFilesInFolder(_tempFolder);

            Assert.AreEqual(1, files.Count);
        }

        [Test]
        public void GetBackupFilesInFolder_LdfTakesPriorityOverMdf()
        {
            // When both exist, only .ldf should be returned
            File.WriteAllBytes(Path.Combine(_tempFolder, "A_TLG_F_202401010000_202402010000.ldf"), new byte[0]);
            File.WriteAllBytes(Path.Combine(_tempFolder, "B_TLG_F_202402010000_202403010000.mdf"), new byte[0]);

            var files = BackupManager.GetBackupFilesInFolder(_tempFolder);

            Assert.AreEqual(1, files.Count);
            Assert.IsTrue(files[0].Name.StartsWith("A_"));
        }

        // ════════════════════════════════════════════════════════════════════
        //  GetMatchingFiles
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void GetMatchingFiles_NoTimeRange_ReturnsAllFiles()
        {
            File.WriteAllBytes(Path.Combine(_tempFolder, "A_TLG_F_202401010000_202402010000.ldf"), new byte[0]);
            File.WriteAllBytes(Path.Combine(_tempFolder, "B_TLG_F_202403010000_202404010000.ldf"), new byte[0]);

            var files = BackupManager.GetMatchingFiles(_tempFolder, null, null);

            Assert.AreEqual(2, files.Count);
        }

        [Test]
        public void GetMatchingFiles_WithTimeRange_ReturnsOnlyOverlapping()
        {
            // Jan file and Mar file; query Feb–Mar → only Mar overlaps
            File.WriteAllBytes(Path.Combine(_tempFolder, "A_TLG_F_202401010000_202402010000.ldf"), new byte[0]);
            File.WriteAllBytes(Path.Combine(_tempFolder, "B_TLG_F_202403010000_202404010000.ldf"), new byte[0]);

            var files = BackupManager.GetMatchingFiles(_tempFolder,
                new DateTime(2024, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc));

            Assert.AreEqual(1, files.Count);
            Assert.IsTrue(files[0].Name.StartsWith("B_"));
        }

        [Test]
        public void GetMatchingFiles_QueryExactlyMatchesFile_ReturnsFile()
        {
            File.WriteAllBytes(Path.Combine(_tempFolder, "A_TLG_F_202401010000_202402010000.ldf"), new byte[0]);

            var files = BackupManager.GetMatchingFiles(_tempFolder,
                new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc));

            Assert.AreEqual(1, files.Count);
        }

        [Test]
        public void GetMatchingFiles_QueryOutsideAllFiles_ReturnsEmpty()
        {
            File.WriteAllBytes(Path.Combine(_tempFolder, "A_TLG_F_202401010000_202402010000.ldf"), new byte[0]);

            var files = BackupManager.GetMatchingFiles(_tempFolder,
                new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc));

            Assert.IsEmpty(files);
        }

        // ════════════════════════════════════════════════════════════════════
        //  BackupFileInfo.ToString
        // ════════════════════════════════════════════════════════════════════

        [Test]
        public void BackupFileInfo_ToString_WithTimeRange_ContainsLocalTime()
        {
            var info = new BackupFileInfo
            {
                Name      = "Archiv_TLG_F_202401010000_202402010000",
                TimeRange = (new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                             new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc))
            };

            string display = info.ToString();

            Assert.IsTrue(display.Contains("Archiv_TLG_F_202401010000_202402010000"));
            Assert.IsTrue(display.Contains("~"));   // range separator present
        }

        [Test]
        public void BackupFileInfo_ToString_WithoutTimeRange_ReturnsName()
        {
            var info = new BackupFileInfo { Name = "UnknownFile", TimeRange = null };
            Assert.AreEqual("UnknownFile", info.ToString());
        }
    }
}
