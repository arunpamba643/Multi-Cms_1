using WebsiteDemo.Models;

namespace WebsiteDemo.Services
{
    public interface IFileManager
    {
        public Task<FileDownloadResponse> DownloadFile(string Request);
    }
}
