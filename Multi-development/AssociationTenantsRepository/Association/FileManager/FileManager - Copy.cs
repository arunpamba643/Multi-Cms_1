using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using AssociationEntities.Models;
using AssociationEntities.FileManagerModels;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.FileManager;
using AssociationEntities;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO.Compression;
using AssociationEntities.Common;
using System.Web.Mvc;
using System.IO;
using System.Text.RegularExpressions;
using Syncfusion.Blazor.Grids;

namespace AssociationRepository.Association
{
    public class FileManager : IFileManager
    {
        private readonly MlTntFileManangerContext _context;
        private readonly IConfiguration _configuration;
        private readonly string contentRootPath;

        public FileManager(MlTntFileManangerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            contentRootPath = _configuration["FileManager:RootPath"];
        }
        /// <summary>
        /// Method to get the root folder details.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="showHiddenItems"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual FileManagerResponse GetFiles(string path, bool showHiddenItems, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse readResponse = new FileManagerResponse();

            try
            {
                int? folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path);
                if (!folderId.HasValue)
                {
                    throw new Exception("Invalid path. Folder not found.");
                }

                var folder = _context.MlTntFolders.FirstOrDefault(f => f.FolderId == folderId.Value && f.IsDeleted == false);
                if (folder == null)
                {
                    throw new Exception("Folder not found or has been deleted.");
                }

                readResponse.CWD = new FileManagerDirectoryContent
                {
                    Name = folder.FolderName,
                    Size = 0,
                    IsFile = false,
                    DateModified = folder.CreatedAt ?? DateTime.Now,
                    DateCreated = folder.CreatedAt ?? DateTime.Now,
                    HasChild = FileManagerUtilities.CheckChild(_context, folder.FolderId),
                    Type = "folder",
                    FilterPath = path
                };

                readResponse.Files =
                    FileManagerUtilities.ReadFolders(_context, folder.FolderId, showHiddenItems).ToList()
                    .Concat(FileManagerUtilities.ReadFiles(_context, folder.FolderId, showHiddenItems)).ToList();
            }
            catch (Exception e)
            {
                readResponse.Error = new ErrorDetails { Message = e.Message };
            }
            return readResponse;
        }
        /// <summary>
        /// Method to upload files.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="uploadFiles"></param>
        /// <param name="action"></param>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual FileManagerResponse Upload(string path, IList<IFormFile> uploadFiles, string action, long size, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse uploadResponse = new FileManagerResponse();
            List<string> existFiles = new List<string>();

            try
            {
                int? folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path);
                if (!folderId.HasValue)
                {
                    throw new Exception("Invalid path. Folder not found.");
                }

                foreach (IFormFile file in uploadFiles)
                {
                    if (file == null) continue;

                    string originalFileName = file.FileName;
                    string fileExtension = Path.GetExtension(originalFileName);
                    string fileBase64 = FileManagerUtilities.ConvertFileToBase64(file);

                    var existingFile = _context.MlTntFiles.FirstOrDefault(f => f.FileName == originalFileName && f.FolderId == folderId);

                    switch (action.ToLower())
                    {
                        case "save":
                            if (existingFile == null)
                            {
                                FileManagerUtilities.SaveFile(_context, fileBase64, originalFileName, fileExtension, folderId.Value, file.Length);
                            }
                            else
                            {
                                existFiles.Add(originalFileName);
                            }
                            break;

                        case "replace":
                            if (existingFile != null)
                            {
                                FileManagerUtilities.RemoveFile(_context, existingFile);
                            }
                            FileManagerUtilities.SaveFile(_context, fileBase64, originalFileName, fileExtension, folderId.Value, file.Length);
                            break;

                        case "keepboth":
                            string newFileName = FileManagerUtilities.GenerateUniqueFileName(_context, originalFileName, folderId.Value);
                            FileManagerUtilities.SaveFile(_context, fileBase64, newFileName, fileExtension, folderId.Value, file.Length);
                            break;

                        default:
                            throw new Exception("Invalid action.");
                    }
                }

                if (existFiles.Count > 0)
                {
                    uploadResponse.Error = new ErrorDetails { Code = "400", Message = "File already exists.", FileExists = existFiles };
                }
            }
            catch (Exception e)
            {
                uploadResponse.Error = new ErrorDetails { Message = e.Message, Code = "417" };
            }
            return uploadResponse;
        }
        /// <summary>
        /// Method to download files.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="names"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual FileStreamResult Download(string path, string[] names, params FileManagerDirectoryContent[] data)
        {
            try
            {
                int userId = GetCurrentUserId();
                int folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path) ?? 0;
                List<int> downloadedFileIds = new List<int>();

                foreach (string name in names)
                {
                    MlTntFile fileDetails = FileManagerUtilities.GetFileDetailsFromDB(_context, name);
                    if (fileDetails == null)
                    {
                        throw new FileNotFoundException($"File {name} not found in database.");
                    }
                    downloadedFileIds.Add(fileDetails.FileId);
                }
                return FileManagerUtilities.DownloadFile(_context, names);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while downloading files", ex);
            }
        }
        /// <summary>
        /// Method to delete files/folders.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="names"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public FileManagerResponse Delete(string path, string[] names, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse deleteResponse = new FileManagerResponse();
            List<FileManagerDirectoryContent> removedFiles = new List<FileManagerDirectoryContent>();

            try
            {
                int? folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path);
                if (folderId == null)
                {
                    throw new FileNotFoundException($"Folder path '{path}' not found.");
                }

                foreach (var name in names)
                {
                    var file = _context.MlTntFiles.FirstOrDefault(f => f.FileName == name && f.FolderId == folderId && f.IsDeleted != null && f.IsDeleted == true);
                    if (file != null)
                    {
                        file.IsDeleted = true;
                        FileManagerUtilities.LogActivity(_context, file.FileId, file.FolderId, 1, "Deleted File");
                        removedFiles.Add(new FileManagerDirectoryContent { Name = file.FileName, IsFile = true });
                        continue;
                    }

                    var folder = _context.MlTntFolders.FirstOrDefault(f => f.FolderName == name && f.ParentFolderId == folderId && f.IsDeleted != null && f.IsDeleted == true);
                    if (folder != null)
                    {
                        folder.IsDeleted = true;
                        FileManagerUtilities.LogActivity(_context, null, folder.FolderId, 1, "Deleted Folder");
                        removedFiles.Add(new FileManagerDirectoryContent { Name = folder.FolderName, IsFile = false });
                    }
                }

                _context.SaveChanges();
                deleteResponse.Files = removedFiles;
            }
            catch (Exception e)
            {
                deleteResponse.Error = new ErrorDetails
                {
                    Message = e.Message,
                    Code = e is UnauthorizedAccessException ? "401" : "417"
                };
            }

            return deleteResponse;
        }
        /// <summary>
        /// Method to copy files/folders to the target location.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targetPath"></param>
        /// <param name="names"></param>
        /// <param name="renameFiles"></param>
        /// <param name="targetData"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual FileManagerResponse Copy(string path, string targetPath, string[] names, string[] renameFiles, FileManagerDirectoryContent targetData, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse copyResponse = new FileManagerResponse();
            List<FileManagerDirectoryContent> copiedFiles = new List<FileManagerDirectoryContent>();
            List<string> existFiles = new List<string>();
            List<string> missingFiles = new List<string>();
            try
            {
                foreach (var name in names)
                {
                    int? folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path);
                    if (!folderId.HasValue)
                    {
                        throw new Exception("Invalid path. Folder not found.");
                    }
                    MlTntFile file = _context.MlTntFiles.FirstOrDefault(f => f.FileName == name && f.FolderId == folderId);
                    if (file == null)
                    {
                        missingFiles.Add(name);
                        continue;
                    }
                    int? targetFolderId = FileManagerUtilities.GetFolderIdFromPath(_context, targetPath);
                    if (!targetFolderId.HasValue)
                    {
                        throw new Exception("Invalid target path. Folder not found.");
                    }
                    MlTntFile targetFile = _context.MlTntFiles.FirstOrDefault(f => f.FileName == name && f.FolderId == targetFolderId);
                    if (targetFile != null)
                    {
                        existFiles.Add(name);
                        continue;
                    }
                    MlTntFile newFile = new MlTntFile
                    {
                        FileName = name,
                        FileExtension = file.FileExtension,
                        FileSize = file.FileSize,
                        FilePath = file.FilePath,
                        FileData = file.FileData,
                        FolderId = targetFolderId.Value,
                        UploadedBy = GetCurrentUserId(),
                        UploadDate = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    _context.MlTntFiles.Add(newFile);
                    _context.SaveChanges();
                    copiedFiles.Add(new FileManagerDirectoryContent
                    {
                        Name = newFile.FileName,
                        Size = newFile.FileSize,
                        IsFile = true,
                        DateModified = newFile.LastModified ?? DateTime.Now,
                        DateCreated = newFile.UploadDate ?? DateTime.Now,
                        HasChild = false,
                        Type = "file",
                        FilterPath = targetPath
                    });
                }
                copyResponse.Files = copiedFiles;
                if (existFiles.Count > 0)
                {
                    ErrorDetails er = new ErrorDetails();
                    er.FileExists = existFiles;
                    er.Code = "400";
                    er.Message = "File Already Exists";
                    copyResponse.Error = er;
                }

                if (missingFiles.Count == 0)
                {
                    return copyResponse;
                }
                else
                {
                    throw new FileNotFoundException(String.Join(',', missingFiles) + " not found in given location.");
                }
            }
            catch (Exception e)
            {
                ErrorDetails er = new ErrorDetails();
                er.Message = e.Message.ToString();
                er.Code = "417";
                er.FileExists = copyResponse.Error?.FileExists;
                copyResponse.Error = er;
                return copyResponse;
            }

        }
        /// <summary>
        /// Method to move files/folders to the target location.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targetPath"></param>
        /// <param name="names"></param>
        /// <param name="renameFiles"></param>
        /// <param name="targetData"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public FileManagerResponse Move(string path, string targetPath, string[] names, string[] renameFiles, FileManagerDirectoryContent targetData, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse moveResponse = new FileManagerResponse();
            List<FileManagerDirectoryContent> movedFiles = new List<FileManagerDirectoryContent>();
            List<string> existFiles = new List<string>();
            List<string> missingFiles = new List<string>();
            try
            {
                foreach (var name in names)
                {
                    int? folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path);
                    if (!folderId.HasValue)
                    {
                        throw new Exception("Invalid path. Folder not found.");
                    }
                    MlTntFile file = _context.MlTntFiles.FirstOrDefault(f => f.FileName == name && f.FolderId == folderId);
                    if (file == null)
                    {
                        missingFiles.Add(name);
                        continue;
                    }
                    int? targetFolderId = FileManagerUtilities.GetFolderIdFromPath(_context, targetPath);
                    if (!targetFolderId.HasValue)
                    {
                        throw new Exception("Invalid target path. Folder not found.");
                    }
                    MlTntFile targetFile = _context.MlTntFiles.FirstOrDefault(f => f.FileName == name && f.FolderId == targetFolderId);
                    if (targetFile != null)
                    {
                        existFiles.Add(name);
                        continue;
                    }
                    file.FolderId = targetFolderId.Value;
                    _context.SaveChanges();
                    movedFiles.Add(new FileManagerDirectoryContent
                    {
                        Name = file.FileName,
                        Size = file.FileSize,
                        IsFile = true,
                        DateModified = file.LastModified ?? DateTime.Now,
                        DateCreated = file.UploadDate ?? DateTime.Now,
                        HasChild = false,
                        Type = "file",
                        FilterPath = targetPath
                    });
                }

                moveResponse.Files = movedFiles;
                if (existFiles.Count > 0)
                {
                    ErrorDetails er = new ErrorDetails();
                    er.FileExists = existFiles;
                    er.Code = "400";
                    er.Message = "File Already Exists";
                    moveResponse.Error = er;
                }

                if (missingFiles.Count == 0)
                {
                    return moveResponse;
                }
                else
                {
                    throw new FileNotFoundException(String.Join(',', missingFiles) + " not found in given location.");
                }

            }
            catch (Exception e)
            {
                ErrorDetails er = new ErrorDetails
                {
                    Message = e.Message.ToString(),
                    Code = "417",
                    FileExists = moveResponse.Error?.FileExists
                };
                moveResponse.Error = er;
                return moveResponse;
            }
        }

        /// <summary>
        /// Method to get the details of the selected files/folders.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="names"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        //public FileManagerResponse Details(string path, string[] names, params FileManagerDirectoryContent[] data)
        //{
        //    throw new NotImplementedException();
        //}
        public virtual FileManagerResponse Details(string path, string[] names, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse getDetailResponse = new FileManagerResponse();
            FileDetails detailFiles = new FileDetails();
            try
            {
                if (names == null || names.Length == 0)
                {
                    throw new ArgumentException("File or folder names must be provided.");
                }

                var folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path);
                var filesQuery = _context.MlTntFiles.Include(f => f.Folder).Where(f => names.Contains(f.FileName) && f.FolderId == folderId);
                var files = filesQuery.ToList();

                if (files.Count == 0)
                {
                    throw new FileNotFoundException("No files or folders found.");
                }

                if (files.Count == 1)
                {
                    var file = files.First();
                    detailFiles = new FileDetails
                    {
                        Name = file.FileName,
                        IsFile = true,//file.IsFile, 
                        Size = file.FileSize.ToString(),
                        Created = file.UploadDate.ToString(),
                        Modified = file.LastModified.ToString(),
                        Location = FileManagerUtilities.GetPathFromFolderId(_context, file.FolderId),
                        Permission = new AccessPermission()
                    };
                }
                else
                {
                    bool isVariousFolders = files.Select(f => f.Folder).Distinct().Count() > 1;
                    long FilesSize = 0;
                    files.ForEach(f => FilesSize = f.FileSize + Convert.ToInt32(FilesSize));
                    detailFiles = new FileDetails
                    {
                        Name = string.Join(", ", files.Select(f => f.FileName)),
                        Size = FilesSize.ToString(),
                        MultipleFiles = true,
                        Location = isVariousFolders ? "Multiple Locations" : files.First().Folder.FolderName
                    };
                }


                getDetailResponse.Details = detailFiles;
                return getDetailResponse;
            }
            catch (Exception e)
            {
                getDetailResponse.Error = new ErrorDetails
                {
                    Message = e.Message,
                    Code = "417"
                };
                return getDetailResponse;
            }
        }

        /// <summary>
        /// Method to create a new folder.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public FileManagerResponse Create(string path, string name, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse createResponse = new FileManagerResponse();
            try
            {
                string parentFolderId = FileManagerUtilities.GetFolderIdFromPath(_context, path).ToString();
                if (parentFolderId == null)
                {
                    throw new Exception("Invalid path. Folder not found.");
                }
                MlTntFolder directoryExits = _context.MlTntFolders.Where(f => f.FolderName == name && f.ParentFolderId == Convert.ToInt32(parentFolderId)).FirstOrDefault();
                if (directoryExits != null)
                {

                    ErrorDetails er = new ErrorDetails();
                    er.Code = "400";
                    er.Message = "A file or folder with the name " + (directoryExits.FolderName) + " already exists.";
                    createResponse.Error = er;

                    return createResponse;
                }

                MlTntFolder folder = new MlTntFolder
                {
                    FolderName = name,
                    ParentFolderId = Convert.ToInt32(parentFolderId),
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.MlTntFolders.Add(folder);
                _context.SaveChanges();

                createResponse.Files = new FileManagerDirectoryContent[] { new FileManagerDirectoryContent
                {
                    Name = folder.FolderName,
                    Size = 0,
                    IsFile = false,
                    DateModified = folder.CreatedAt ?? DateTime.Now,
                    DateCreated = folder.CreatedAt ?? DateTime.Now,
                    HasChild = false,
                    Type = "folder",
                    FilterPath = path
                } };
                return createResponse;
            }
            catch (Exception e)
            {
                ErrorDetails er = new ErrorDetails();
                er.Message = e.Message.ToString();
                er.Code = er.Message.Contains("is not accessible. You need permission") ? "401" : "417";
                createResponse.Error = er;
                return createResponse;
            }


        }
        /// <summary>
        /// Method to search for files/folders in the FileManager.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchString"></param>
        /// <param name="showHiddenItems"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public FileManagerResponse Search(string path, string searchString, bool showHiddenItems = false, bool caseSensitive = false, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse searchResponse = new FileManagerResponse();
            try
            {
                if (path == null) { path = string.Empty; };
                string searchWord = searchString;
                string searchPath = (this.contentRootPath + path);

                MlTntFolder cDirectory = _context.MlTntFolders.Where(x => x.FolderId == FileManagerUtilities.GetFolderIdFromPath(_context, path)).FirstOrDefault();

                if (cDirectory == null)
                {
                    throw new UnauthorizedAccessException("Access denied for Directory-traversal");
                }

                FileManagerDirectoryContent cwd = new FileManagerDirectoryContent();

                cwd.Name = cDirectory.FolderName;
                cwd.Size = 0;
                cwd.IsFile = false;
                cwd.DateModified = cDirectory.CreatedAt ?? DateTime.Now;
                cwd.DateCreated = cDirectory.CreatedAt ?? DateTime.Now;
                cwd.HasChild = FileManagerUtilities.CheckChild(_context, cDirectory.FolderId);
                cwd.Type = "folder";
                cwd.FilterPath = "";
                cwd.Permission = new AccessPermission();

                searchResponse.CWD = cwd;

                List<FileManagerDirectoryContent> foundedFiles = new List<FileManagerDirectoryContent>();
                string[] extensions;// this.allowedExtension;
                DirectoryInfo searchDirectory = new DirectoryInfo(searchPath);
                List<MlTntFile> files = new List<MlTntFile>();
                List<DirectoryInfo> directories = new List<DirectoryInfo>();


                IEnumerable<FileManagerDirectoryContent> filteredFileList = FileManagerUtilities.ReadFiles(_context, cDirectory.FolderId, showHiddenItems).
                   Where(item => new Regex(WildcardToRegex(searchString), (caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase)).IsMatch(item.Name));

                IEnumerable<FileManagerDirectoryContent> filteredDirectoryList = FileManagerUtilities.ReadFolders(_context, cDirectory.FolderId, showHiddenItems).
                   Where(item => new Regex(WildcardToRegex(searchString), (caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase)).IsMatch(item.Name));

                foundedFiles.Add((FileManagerDirectoryContent)filteredFileList);
                foundedFiles.Add((FileManagerDirectoryContent)filteredDirectoryList);




                searchResponse.Files = (IEnumerable<FileManagerDirectoryContent>)foundedFiles;
                return searchResponse;
            }
            catch (Exception e)
            {
                ErrorDetails er = new ErrorDetails();
                er.Message = e.Message.ToString();
                er.Code = "417";

                return searchResponse;
            }
        }

        protected virtual string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                              .Replace(@"\*", ".*")
                              .Replace(@"\?", ".")
                       + "$";
        }

        /// <summary>
        /// Method to download files.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="newName"></param>
        /// <param name="replace"></param>
        /// <param name="showFileExtension"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public FileManagerResponse Rename(string path, string name, string newName, bool replace = false, bool showFileExtension = true, params FileManagerDirectoryContent[] data)
        {
            FileManagerResponse renameResponse = new FileManagerResponse();
            try
            {
                int? folderId = FileManagerUtilities.GetFolderIdFromPath(_context, path);
                if (!folderId.HasValue)
                {
                    throw new Exception("Invalid path. Folder not found.");
                }
                MlTntFile file = _context.MlTntFiles.FirstOrDefault(f => f.FileName == name && f.FolderId == folderId);
                //if (file == null)
                //{
                //    missingFiles.Add(name);
                //    continue;
                //}

                return renameResponse;
            }
            catch (Exception e)
            {
                ErrorDetails er = new ErrorDetails();
                er.Message = e.Message.ToString();
                er.Code = "417";
                er.FileExists = renameResponse.Error?.FileExists;
                renameResponse.Error = er;
                return renameResponse;
            }
        }
        /// <summary>
        /// Method to download files.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <param name="allowCompress"></param>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public FileStreamResult GetImage(string path, string id, bool allowCompress, ImageSize size, params FileManagerDirectoryContent[] data)
        {
            throw new NotImplementedException();
        }

        #region FileMangerUtility
        /// <summary>
        /// Method to log activity.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="folderId"></param>
        /// <param name="fileIds"></param>
        /// <param name="action"></param>
        //private void LogActivity(int userId, int folderId, List<int> fileIds, string action)
        //{
        //    foreach (var fileId in fileIds)
        //    {
        //        _context.MlTntActivityLogs.Add(new MlTntActivityLog
        //        {
        //            UserId = userId,
        //            FolderId = folderId,
        //            FileId = fileId,
        //            Action = action,
        //            Timestamp = DateTime.UtcNow
        //        });
        //    }
        //    _context.SaveChanges();
        //}
        /// <summary>
        /// Method to get the current user id.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentUserId()
        {
            return 1; // Placeholder
        }
        #endregion FileMangerUtility
    }
}
