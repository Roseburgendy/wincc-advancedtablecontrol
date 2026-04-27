using System.Collections.Generic;

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
        public string TagName { get; set; }
        public int Order { get; set; }
    }
}
