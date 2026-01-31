using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class Container
{
    public int ContainerId { get; set; }

    public int? RowId { get; set; }

    public string? ContainerName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int NoofContainers { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();

    public virtual Row? Row { get; set; }
}
