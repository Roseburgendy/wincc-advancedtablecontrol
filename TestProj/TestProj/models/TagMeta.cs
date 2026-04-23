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
        public int? Order { get; set; }
    }

    // JSON 里 dataSource 节点
    public class DataSourceConfig
    {
        public string ArchiveName { get; set; }
    }

    // JSON 根节点
    public class ProjectConfig
    {
        public string CompanyName { get; set; } = "";
        public DataSourceConfig DataSource { get; set; }
        public List<TagMeta> TagMeta { get; set; }
    }
}