using Newtonsoft.Json;
using System.Collections.Generic;

namespace WINCCReporter.Models
{
    // JSON 里 tagMeta 数组的单项
    public class TagMeta
    {
        public long ValueID { get; set; }
        public string TagName { get; set; }
        public string DisplayName { get; set; }       // 中文别名
        public string DisplayNameEn { get; set; }     // 英文别名
        [JsonIgnore] public string DataType { get; set; }
        public int DecimalPlaces { get; set; } = 2;
    }


    // JSON 根节点
    public class ProjectConfig
    {
        public string CompanyName { get; set; } = "";
        public string Theme { get; set; } = "Microsoft365Blue";
        public string Language { get; set; } = "zh-CN";
        public bool AutoExport { get; set; } = false;
        public List<TagMeta> TagMeta { get; set; } = new List<TagMeta>();
        public List<ReportTemplate> Template { get; set; } = new List<ReportTemplate>();
        /// <summary>批量精确位数规则，按前缀匹配，顺序优先。</summary>
        public List<DecimalRule> DecimalRules { get; set; } = new List<DecimalRule>();

        /// <summary>单次查询允许的最大时间跨度（天）。0 表示不限制。默认 31 天。</summary>
        public int MaxQueryDays { get; set; } = 7;
        /// <summary>单次查询允许的最大数据行数。超出时截断并提示。0 表示不限制。默认 3000。</summary>
        public int MaxDataPoints { get; set; } = 3000;

        /// <summary>是否在查询时自动连接对应时间段的备份归档。</summary>
        public bool AutoConnectBackup { get; set; } = false;
        /// <summary>WinCC 备份归档文件所在文件夹路径。</summary>
        public string BackupPath { get; set; } = "";

        /// <summary>管理员密码的 SHA-256 哈希值；空字符串表示未设置密码。</summary>
        public string AdminPasswordHash { get; set; } = "";
    }
}