using System;
using System.Collections.Generic;

namespace AssociationEntities.FileManagerModels;

public partial class MlTntFmuser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<MlTntActivityLog> MlTntActivityLogs { get; set; } = new List<MlTntActivityLog>();

    public virtual ICollection<MlTntFileFolderPermission> MlTntFileFolderPermissions { get; set; } = new List<MlTntFileFolderPermission>();

    public virtual ICollection<MlTntFile> MlTntFiles { get; set; } = new List<MlTntFile>();

    public virtual ICollection<MlTntFolder> MlTntFolders { get; set; } = new List<MlTntFolder>();
}
