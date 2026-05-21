using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestProj.Models
{
    // JSON 里 tagMeta 数组的单项
    public class TagMeta
    {
        public long ValueID { get; set; }
        public string TagName { get; set; }
        public string DisplayName { get; set; }       // 中文别名
        public string DisplayNameEn { get; set; }     // 英文别名
        [JsonIgnore] public string DataType { get; set; }
        public double? NormalMin { get; set; }
        public double? NormalMax { get; set; }
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
    }
}