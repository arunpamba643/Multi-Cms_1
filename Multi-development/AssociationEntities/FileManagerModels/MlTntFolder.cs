using System;
using System.Collections.Generic;

namespace AssociationEntities.FileManagerModels;

public partial class MlTntFolder
{
    public int FolderId { get; set; }

    public string FolderName { get; set; } = null!;

    public int? ParentFolderId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual MlTntFmuser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<MlTntFolder> InverseParentFolder { get; set; } = new List<MlTntFolder>();

    public virtual ICollection<MlTntActivityLog> MlTntActivityLogs { get; set; } = new List<MlTntActivityLog>();

    public virtual ICollection<MlTntFileFolderPermission> MlTntFileFolderPermissions { get; set; } = new List<MlTntFileFolderPermission>();

    public virtual ICollection<MlTntFile> MlTntFiles { get; set; } = new List<MlTntFile>();

    public virtual MlTntFolder? ParentFolder { get; set; }
}
