using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class BlogImage
{
    public int ImageId { get; set; }

    public int BlogId { get; set; }

    public int TenantId { get; set; }

    public string? ImageUrl { get; set; }

    public string? Text { get; set; }

    public int Position { get; set; }

    public string? Udf { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
