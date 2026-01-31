namespace Demo_App.Services
{
    public interface IJSService
    {
        Task RenderFileTree();
        Task DownloadFile(string fileName, string base64String);
    }
}
