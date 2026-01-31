using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class TenantComponent
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ComponentTypeId { get; set; }

    public int? Ordernumber { get; set; }

    public string? Text { get; set; }

    public int? Width { get; set; }
}
