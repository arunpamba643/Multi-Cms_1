using System;
using System.Collections.Generic;

namespace AssociationEntities.FileManagerModels;

public partial class MlTntActivityLog
{
    public int LogId { get; set; }

    public int? FileId { get; set; }

    public int? FolderId { get; set; }

    public int UserId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public virtual MlTntFile? File { get; set; }

    public virtual MlTntFolder? Folder { get; set; }

    public virtual MlTntFmuser User { get; set; } = null!;
}
