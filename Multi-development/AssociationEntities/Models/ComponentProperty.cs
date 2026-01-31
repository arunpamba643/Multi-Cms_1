using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class ComponentProperty
{
    public int PropertyId { get; set; }

    public int? ComponentId { get; set; }

    public string? Key { get; set; }

    public string? Value { get; set; }

    public virtual Component? Component { get; set; }
}
