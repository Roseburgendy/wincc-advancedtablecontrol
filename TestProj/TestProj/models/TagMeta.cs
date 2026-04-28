using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestProj.Models
{
    // JSON 里 tagMeta 数组的单项
    public class TagMeta
    {

        public string TagName { get; set; }
        public string DisplayName { get; set; }
        [JsonIgnore] public string DataType { get; set; }
        public double? NormalMin { get; set; }
        public double? NormalMax { get; set; }
        public int DecimalPlaces { get; set; }=2;
    }


    // JSON 根节点
    public class ProjectConfig
    {
        public string CompanyName { get; set; } = "";
        public List<TagMeta> TagMeta { get; set; } = new List<TagMeta>();
        public List<ReportTemplate> Template { get; set; } = new List<ReportTemplate>();
    }
}