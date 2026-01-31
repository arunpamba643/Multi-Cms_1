using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class Container
{
    public int Id { get; set; }

    public int RowNumber { get; set; }

    public int Sequence { get; set; }

    public string? Name { get; set; }

    public int ComponentId { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Component Component { get; set; } = null!;
}
