using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationEntities.Models
{
    public class Permission { public int PermissionID { get; set; } public int FileID { get; set; } public string TenantId { get; set; } public string PermissionType { get; set; } public DateTime? GrantedDate { get; set; } }
}
