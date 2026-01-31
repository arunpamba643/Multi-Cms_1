using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class Component
{
    public int ComponentId { get; set; }

    public int? ContainerId { get; set; }

    public string? ComponentName { get; set; }

    public string? ComponentType { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? OrderNumber { get; set; }

    public virtual ICollection<ComponentProperty> ComponentProperties { get; set; } = new List<ComponentProperty>();

    public virtual Container? Container { get; set; }
}
