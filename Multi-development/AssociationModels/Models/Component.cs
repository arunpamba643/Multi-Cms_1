using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class Component
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ComponentType { get; set; }

    public int Sequence { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<ComponentProperty> ComponentProperties { get; set; } = new List<ComponentProperty>();

    public virtual ICollection<Container> Containers { get; set; } = new List<Container>();
}
