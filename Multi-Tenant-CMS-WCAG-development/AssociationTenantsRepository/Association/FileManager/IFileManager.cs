using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Syncfusion.Blazor.FileManager;
using Microsoft.AspNetCore.Mvc;   



namespace AssociationRepository.Association
{

    /// <summary>
    /// Interface for managing file operations in the FileManager.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Method to get the root folder details.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="showHiddenItems"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileManagerResponse GetFiles(string path, bool showHiddenItems, params FileManagerDirectoryContent[] data);

        /// <summary>
        /// Method to create a new folder.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="names"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileManagerResponse Delete(string path, string[] names, params FileManagerDirectoryContent[] data);
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
        public FileManagerResponse Copy(string path, string targetPath, string[] names, string[] renameFiles, FileManagerDirectoryContent targetData, params FileManagerDirectoryContent[] data);
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
        public FileManagerResponse Move(string path, string targetPath, string[] names, string[] renameFiles, FileManagerDirectoryContent targetData, params FileManagerDirectoryContent[] data);
        /// <summary>
        /// Method to get the details of the selected files/folders.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="names"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileManagerResponse Details(string path, string[] names, params FileManagerDirectoryContent[] data);
        /// <summary>
        /// Method to create a new folder.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileManagerResponse Create(string path, string name, params FileManagerDirectoryContent[] data);
        /// <summary>
        /// Method to search for files/folders in the FileManager.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchString"></param>
        /// <param name="showHiddenItems"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileManagerResponse Search(string path, string searchString, bool showHiddenItems = false, bool caseSensitive = false, params FileManagerDirectoryContent[] data);
        /// <summary>
        /// Method to rename a file/folder.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="newName"></param>
        /// <param name="replace"></param>
        /// <param name="showFileExtension"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileManagerResponse Rename(string path, string name, string newName, bool replace = false, params FileManagerDirectoryContent[] data);
        /// <summary>
        /// Method to upload files to the target location.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="uploadFiles"></param>
        /// <param name="action"></param>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileManagerResponse Upload(string path, IList<IFormFile> uploadFiles, string action, FileManagerDirectoryContent[] data, long size = 0, int chunkIndex = 0, int totalChunk = 0);
        /// <summary>
        /// Method to download files/folders.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="names"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileStreamResult Download(string path, string[] names, params FileManagerDirectoryContent[] data);
        /// <summary>
        /// Method to get the image details.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <param name="allowCompress"></param>
        /// <param name="size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public FileStreamResult GetImage(string path, string parentId, string id, bool allowCompress, ImageSize size, params FileManagerDirectoryContent[] data);
    }

}
