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
        /// <summary>该模板的默认导出目录；导出 Excel 或打印时作为对话框初始路径。</summary>
        public string ExportPath { get; set; } = "";
    }

    public class TemplateTagEntry
    {
        public long ValueID { get; set; }
        public int Order { get; set; }
    }
}
