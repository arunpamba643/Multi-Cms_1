namespace AssociationEntities.Models;

public partial class PageModel
{
    public int Id { get; set; }

    public int TenantId { get; set; }
    public bool? IsHomePage { get; set; }
    public string PageTitle { get; set; } = null!;
    public string? ResourcePath { get; set; }

    public int MenuId { get; set; }

    public int? PaddingWidth { get; set; }
    public bool IsLandingPage { get; set; }
    public DateTime? PublishStartDate { get; set; }

    public DateTime? PublishEndDate { get; set; }
    public string? PageScopeType { get; set; }
    public virtual CustomMenu Menu { get; set; } = null!;
}
