using System;
using System.Collections.Generic;

namespace AssociationEntities.Models.Db;

public partial class CustomMenu
{
    public int MenuId { get; set; }

    public string MenuName { get; set; } = null!;

    public int? ParentMenuId { get; set; }

    public string? PageUrl { get; set; }

    public int Position { get; set; }

    public int TenantId { get; set; }
}
