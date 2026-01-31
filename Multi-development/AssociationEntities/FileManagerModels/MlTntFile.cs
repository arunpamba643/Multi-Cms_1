using System;
using System.Collections.Generic;

namespace AssociationEntities.FileManagerModels;

public partial class MlTntFile
{
    public int FileId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileExtension { get; set; } = null!;

    public long FileSize { get; set; }

    public string FilePath { get; set; } = null!;

    public string FileData { get; set; } = null!;

    public int? FolderId { get; set; }

    public int UploadedBy { get; set; }

    public DateTime? UploadDate { get; set; }

    public DateTime? LastModified { get; set; }

    public bool? IsDeleted { get; set; }

    public string? StorageName { get; set; }

    public string? FileGuid { get; set; }

    public virtual MlTntFolder? Folder { get; set; }

    public virtual ICollection<MlTntActivityLog> MlTntActivityLogs { get; set; } = new List<MlTntActivityLog>();

    public virtual ICollection<MlTntFileFolderPermission> MlTntFileFolderPermissions { get; set; } = new List<MlTntFileFolderPermission>();

    public virtual MlTntFmuser UploadedByNavigation { get; set; } = null!;
}
