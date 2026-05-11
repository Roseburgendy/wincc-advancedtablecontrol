using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestProj.Models
{
    public class ReportTemplate
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<TemplateTagEntry> SelectedTags { get; set; } = new List<TemplateTagEntry>();
    }

    public class TemplateTagEntry
    {
        public long ValueID { get; set; }
        public int Order { get; set; }

        // 仅用于从旧 config 迁移，迁移完成后为 null，不写入新 JSON
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TagName { get; set; }
    }
}
