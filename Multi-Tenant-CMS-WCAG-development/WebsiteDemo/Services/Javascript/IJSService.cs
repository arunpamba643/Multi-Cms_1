namespace WebsiteDemo.Services
{
    public interface IJSService
    {
        Task DownloadFile(string fileName, string base64String);
    }
}
