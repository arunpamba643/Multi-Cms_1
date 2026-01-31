using System;
using System.Collections.Generic;

namespace AssociationEntities.Models;

public partial class BlogCreator
{
    public int Bcid { get; set; }

    public int? Bpid { get; set; }

    public string? Gender { get; set; }

    public string? Firstname { get; set; }

    public string? SecondName { get; set; }

    public int? Age { get; set; }

    public virtual BlogPost? Bp { get; set; }
}
