using Microsoft.AspNetCore.Http;
using AssociationEntities.FileManagerModels;
using Syncfusion.Blazor.FileManager;
using System.IO.Compression;
using System.Web.Mvc;
using Syncfusion.Blazor.Gantt;
using Syncfusion.Blazor.Kanban.Internal;
using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public static class FileManagerUtilities
    {



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int? GetFolderIdFromPath(MlTntFileManangerContext _context, string path)
        {
            if (string.IsNullOrWhiteSpace(path) || path == "/")
            {
                return _context.MlTntFolders.FirstOrDefault(f => f.ParentFolderId == null)?.FolderId;
                //return 1;
            }

            string[] pathSegments = path.Trim('/').Split('/');
            int? parentId = null;
            foreach (var segment in pathSegments)
            {
                if (pathSegments.Length == 1)
                {
                    parentId = 1;
                }

                var folder = _context.MlTntFolders
                    .FirstOrDefault(f => f.FolderName == segment && f.ParentFolderId == parentId && f.IsDeleted == false);

                if (folder == null)
                    return null;

                parentId = folder.FolderId;
            }
            return parentId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ConvertFileToBase64(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="base64Data"></param>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        /// <param name="folderId"></param>
        /// <param name="size"></param>
        public static void SaveFile(MlTntFileManangerContext _context, string base64Data, string fileName, string extension, int folderId, long size)
        {
            string fileGuid = Guid.NewGuid().ToString();
            string uniqueFileName = fileGuid + ".txt";
            string filePath = WriteFile("", uniqueFileName, base64Data);

            MlTntFile newFile = new MlTntFile
            {
                FileName = fileName,
                FileExtension = extension,
                FileSize = size,
                FilePath = filePath,
                FileData = "",
                FolderId = folderId,
                UploadedBy = 1,
                UploadDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                IsDeleted = false,
                StorageName = uniqueFileName,
                FileGuid = fileGuid
            };

            _context.MlTntFiles.Add(newFile);
            _context.SaveChanges();
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        /// <param name="FileContent"></param>
        /// <returns></returns>
        public static string WriteFile(string FilePath, string FileName, string FileContent)
        {
            FilePath = "C:\\Blazor Project\\2025-02-16\\Multi tenant\\From GitHub\\Multi-Tenant-CMS-WCAG-Development\\AssociationTenantsAPI\\wwwroot\\Files\\";
            try
            {
                System.IO.File.WriteAllText(FilePath + FileName, FileContent);
            }
            catch (Exception ex)
            {
                return "";
            }
            return FilePath + FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="file"></param>
        public static void RemoveFile(MlTntFileManangerContext _context, MlTntFile file)
        {
            file.IsDeleted = true;
            file.LastModified = DateTime.UtcNow;

            _context.MlTntFiles.Update(file);

            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="fileName"></param>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public static string GenerateUniqueFileName(MlTntFileManangerContext _context, string fileName, int folderId)
        {
            string baseName = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int count = 1;

            while (_context.MlTntFiles.Any(f => f.FileName == $"{baseName} ({count}){extension}" && f.FolderId == folderId))
            {
                count++;
            }

            return $"{baseName} ({count}){extension}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="fileId"></param>
        /// <param name="folderId"></param>
        /// <param name="userId"></param>
        /// <param name="action"></param>
        public static void LogActivity(MlTntFileManangerContext _context, int? fileId, int? folderId, int userId, string action)
        {
            var logEntry = new MlTntActivityLog
            {
                FileId = fileId,
                FolderId = folderId,
                UserId = userId,
                Action = action,
                Timestamp = DateTime.UtcNow
            };

            _context.MlTntActivityLogs.Add(logEntry);
            _context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MlTntFile GetFileDetailsFromDB(MlTntFileManangerContext _context, string fileName)
        {
            return _context.MlTntFiles.FirstOrDefault(f => f.FileName == fileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static FileStreamResult DownloadFile(MlTntFileManangerContext _context, string[] names)
        {
            if (names.Length == 1)
            {
                MlTntFile fileDetails = GetFileDetailsFromDB(_context, names[0]);

                if (fileDetails == null)
                {
                    throw new FileNotFoundException($"File {names[0]} not found in database.");
                }

                byte[] fileBytes = Convert.FromBase64String(fileDetails.FileData);
                MemoryStream stream = new MemoryStream(fileBytes);

                LogActivity(_context, fileDetails.FolderId, fileDetails.Folder.FolderId, 1, "Download");

                return new FileStreamResult(stream, "APPLICATION/octet-stream")
                {
                    FileDownloadName = fileDetails.FileName
                };
            }
            else
            {
                return CreateZipForFiles(_context, names);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static FileStreamResult CreateZipForFiles(MlTntFileManangerContext _context, string[] names)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".zip");
            using (FileStream zipFile = new FileStream(tempPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create, true))
                {
                    foreach (string name in names)
                    {
                        MlTntFile fileDetails = GetFileDetailsFromDB(_context, name);
                        if (fileDetails == null) continue;

                        byte[] fileBytes = Convert.FromBase64String(fileDetails.FileData);
                        ZipArchiveEntry zipEntry = archive.CreateEntry(fileDetails.FileName, CompressionLevel.Fastest);
                        using (Stream entryStream = zipEntry.Open())
                        {
                            entryStream.Write(fileBytes, 0, fileBytes.Length);
                        }
                        LogActivity(_context, fileDetails.FolderId, fileDetails.Folder.FolderId, 1, "BulkDownload");
                    }
                }
            }

            FileStream fileStreamInput = new FileStream(tempPath, FileMode.Open, FileAccess.Read, FileShare.Delete);
            return new FileStreamResult(fileStreamInput, "APPLICATION/octet-stream")
            {
                FileDownloadName = "files.zip"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentFolderId"></param>
        /// <param name="showHiddenItems"></param>
        /// <returns></returns>
        public static IEnumerable<FileManagerDirectoryContent> ReadFolders(MlTntFileManangerContext _context, int ParentFolderId, bool showHiddenItems)
        {
            return _context.MlTntFolders
                .Where(f => f.ParentFolderId == ParentFolderId && f.IsDeleted == false)
                .Select(folder => new FileManagerDirectoryContent
                {
                    Name = folder.FolderName,
                    Size = 0,
                    IsFile = false,
                    DateModified = folder.CreatedAt ?? DateTime.Now,
                    DateCreated = folder.CreatedAt ?? DateTime.Now,
                    HasChild = _context.MlTntFolders.Any(f => f.ParentFolderId == folder.FolderId && f.IsDeleted == false),
                    Type = "folder",
                    FilterPath = GetPathFromFolderId(_context, folder.ParentFolderId)
                }).ToList();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="FolderId"></param>
        /// <returns></returns>
        public static string GetPathFromFolderId(MlTntFileManangerContext _context, int? FolderId)
        {
            List<string> pathSegments = new List<string>();
            int? currentFolderId = FolderId;

            while (currentFolderId.HasValue)
            {
                var folder = _context.MlTntFolders.FirstOrDefault(f => f.FolderId == currentFolderId);
                if (folder == null)
                    break;

                pathSegments.Insert(0, folder.FolderName);
                currentFolderId = folder.ParentFolderId;
            }

            //if (pathSegments.Count() == 1 && pathSegments[0] == "/")
            //{
            //    return "\\";
            //}
            // pathSegments.RemoveAt(0);
            return "\\" + string.Join("\\", pathSegments) + "\\";
            //return "\\" + string.Join("\\", pathSegments).Replace("/", "\\");
        }

        public static bool CheckChild(MlTntFileManangerContext _context, int FolderId)
        {
            return _context.MlTntFolders.Any(f => f.ParentFolderId == FolderId && f.IsDeleted == false);
        }

        public static IEnumerable<FileManagerDirectoryContent> ReadFiles(MlTntFileManangerContext _context, int FolderId, bool showHiddenItems)
        {
            return _context.MlTntFiles
                .Where(f => f.FolderId == FolderId && f.IsDeleted == false)
                .Select(file => new FileManagerDirectoryContent
                {
                    Name = file.FileName,
                    Size = file.FileSize,
                    IsFile = true,
                    DateModified = file.LastModified ?? DateTime.Now,
                    DateCreated = file.UploadDate ?? DateTime.Now,
                    HasChild = false,
                    Type = file.FileExtension,
                    FilterPath = GetPathFromFolderId(_context, file.FolderId)
                }).ToList();
        }

        //public static string GenerateUniqueFileName(MlTntFileManangerContext dbContext, string path, string fileName)
        //{
        //    int count = 1;
        //    string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        //    string extension = Path.GetExtension(fileName);
        //    string newFileName = fileName;

        //    while (dbContext.MlTntFiles.Any(f => f.Path == path && f.Name == newFileName))
        //    {
        //        newFileName = $"{nameWithoutExt}({count}){extension}";
        //        count++;
        //    }
        //    return newFileName;
        //}
    }
}
