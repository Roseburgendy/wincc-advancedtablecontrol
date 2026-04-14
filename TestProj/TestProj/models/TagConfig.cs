using System.Collections.Generic;

namespace TestProj.Models
{
    // JSON 里 tagMeta 数组的单项
    public class TagConfig
    {
        public string TagName { get; set; }
        public string DisplayName { get; set; }
        public string Unit { get; set; }
        public double NormalMin { get; set; }
        public double NormalMax { get; set; }
    }

    // JSON 里 dataSource 节点
    public class DataSourceConfig
    {
        public string ArchiveName { get; set; }
    }

    // JSON 根节点
    public class ProjectConfig
    {
        public DataSourceConfig DataSource { get; set; }
        public List<TagConfig> TagMeta { get; set; }
    }

    // CheckedListBox 显示用，不对应 JSON
    public class TagItem
    {
        public string TagName { get; set; }
        public string DisplayName { get; set; }
        public override string ToString() => DisplayName;
    }
}