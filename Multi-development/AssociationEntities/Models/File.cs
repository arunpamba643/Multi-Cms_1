using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationEntities.Models
{
    public class File
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime? UploadDate { get; set; }
        public string TenantId
        {
            get; set;
        }
    }
}