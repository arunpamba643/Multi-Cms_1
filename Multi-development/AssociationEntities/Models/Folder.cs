using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationEntities.Models
{
    public class Folder { public int FolderID { get; set; } public string FolderName { get; set; } public int? ParentFolderID { get; set; } public DateTime? CreatedDate { get; set; } public string TenantId { get; set; } }
}
