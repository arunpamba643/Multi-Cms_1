namespace Demo_App.Models
{
    /// <summary>
    /// Class for DataSend Model
    /// </summary>
    public class DataSendModel
    {
        public int BlogId { get; set; }

        public int TenantId { get; set; }

        public string? Htmltemplate { get; set; }

        public string? DynamicHtmltemplate { get; set; }

        public string? HeaderBackgroundColor { get; set; }

        public string? HeaderFontColor { get; set; }

        public string? BrandName { get; set; }

        public string? Logo { get; set; }

        public string? Banner { get; set; }

        public string? Heading { get; set; }

        public string? TextContent { get; set; }

        public string? ContentFontSize { get; set; }

        public string? ContentFontColor { get; set; }

        public string? FooterBackgroundColor { get; set; }

        public string? FooterText { get; set; }

        public DateTime? PublishedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }

    public class DivModel
    {
        public string Name { get; set; }
        public string Input1Value { get; set; }
        public string Input2Value { get; set; }
    }
    public class TeamsDataModel
    {
        public string Image { get; set; }

        public string Text { get; set; }
    }
}
