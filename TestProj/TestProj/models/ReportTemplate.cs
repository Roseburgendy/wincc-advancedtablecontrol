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
    }
}
