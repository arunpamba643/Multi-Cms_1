using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syncfusion.Blazor.FileManager;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssociationEntities
{
    public class FileManagerResponse
    {
        public FileManagerDirectoryContent CWD { get; set; }

        public IEnumerable<FileManagerDirectoryContent> Files { get; set; }

        public ErrorDetails Error { get; set; }

        public FileDetails Details { get; set; }
    }

    public class ImageSize
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }


    public class CustomFileManagerDirectoryContent
    {
        public string Path { get; set; }

        public string Action { get; set; }

        public string NewName { get; set; }

        public string[] Names { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public string PreviousName { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public bool HasChild { get; set; }

        public bool IsFile { get; set; }

        public string Type { get; set; }

        public string Id { get; set; }

        public string FilterPath { get; set; }

        public string FilterId { get; set; }

        public string ParentId { get; set; }

        public string TargetPath { get; set; }

        public string[] RenameFiles { get; set; }

        public IList<IFormFile> UploadFiles { get; set; }

        public bool CaseSensitive { get; set; }

        public string SearchString { get; set; }

        public bool ShowHiddenItems { get; set; }

        public bool ShowFileExtension { get; set; }

        public Syncfusion.Blazor.FileManager.FileManagerDirectoryContent[] Data { get; set; }

        public Syncfusion.Blazor.FileManager.FileManagerDirectoryContent TargetData { get; set; }

        public Syncfusion.Blazor.FileManager.AccessPermission Permission { get; set; }
    }

}
