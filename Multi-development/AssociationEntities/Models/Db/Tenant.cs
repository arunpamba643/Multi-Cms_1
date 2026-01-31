using System;
using System.Collections.Generic;

namespace AssociationEntities.Models.Db;

public partial class Tenant
{
    public int TenantId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<BlogImage> BlogImages { get; set; } = new List<BlogImage>();
}
