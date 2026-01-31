namespace WebsiteDemo.Models;

public partial class PageModel
{
    public int Id { get; set; }

    public int TenantId { get; set; }
    public string PageTitle { get; set; } = null!;

    public bool? IsHomePage { get; set; }
    public string? ResourcePath { get; set; }

    public int MenuId { get; set; }

    public int? PaddingWidth { get; set; }
    public bool? IsLandingPage { get; set; }
    public DateTime? PublishStartDate { get; set; }

    public DateTime? PublishEndDate { get; set; }
    public string MenuName { get; set; }
    public string pageScopeType { get; set; }
}
