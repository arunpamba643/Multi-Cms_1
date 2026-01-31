using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public int TenantId { get; set; }

    public string? BlogName { get; set; }

    public string? HeaderBackgroundColor { get; set; }

    public string? HeaderFontColor { get; set; }

    public string? BrandName { get; set; }

    public string? Logo { get; set; }

    public string? Banner { get; set; }

    public string? Heading { get; set; }

    public string? TextContent { get; set; }

    public string? HeaderFontStyle { get; set; }

    public string? ContentFontStyle { get; set; }

    public string? FooterFontStyle { get; set; }

    public string? ContentFontSize { get; set; }

    public string? ContentFontColor { get; set; }

    public string? MainMenuColor { get; set; }

    public string? MainMenuFontSize { get; set; }

    public string? MainMenuFontStyle { get; set; }

    public string? SubMenuColor { get; set; }

    public string? SubMenuFontSize { get; set; }

    public string? SubMenuFontStyle { get; set; }

    public string? SubChildMenuColor { get; set; }

    public string? SubChildMenuFontSize { get; set; }

    public string? SubChildFontStyle { get; set; }

    public string? FooterBackgroundColor { get; set; }

    public string? FooterText { get; set; }

    public string? FooterFontColor { get; set; }

    public string? Facebook { get; set; }

    public string? LinkedIn { get; set; }

    public string? Twitter { get; set; }

    public string? Instagram { get; set; }

    public string? WhatsApp { get; set; }

    public DateTime? PublishedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<BlogImage> BlogImages { get; set; } = new List<BlogImage>();
}
