using Demo_App.Models;

namespace Demo_App.Services
{
    public interface IFileManager
    {
        public Task<FileDownloadResponse> DownloadFile(string Request);
    }
}
