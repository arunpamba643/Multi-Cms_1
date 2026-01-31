using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class Row
{
    public int RowId { get; set; }

    public int? PageId { get; set; }

    public int? OrderNumber { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdateOn { get; set; }

    public bool? IsSaved { get; set; }

    public virtual ICollection<Container> Containers { get; set; } = new List<Container>();
}
