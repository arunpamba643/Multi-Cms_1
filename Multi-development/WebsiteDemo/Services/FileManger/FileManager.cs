using WebsiteDemo.Models;

namespace WebsiteDemo.Services
{
    public class FileManager : IFileManager
    {
        private readonly HttpClient _httpClient;
        public FileManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Downloads a file asynchronously.
        /// </summary>
        /// <param name="Request">The file to download.</param>
        public async Task<FileDownloadResponse> DownloadFile(string request)
        {
            FileDownloadResponse fileDownloadResponse = new FileDownloadResponse();
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(request), "downloadInput");
                var response = await _httpClient.PostAsync("api/FileManager/DownloadBase64", content);
                response.EnsureSuccessStatusCode();

                // Assuming the API returns the ID of the newly created blog page
                fileDownloadResponse = await response.Content.ReadFromJsonAsync<FileDownloadResponse>() ?? new FileDownloadResponse();
                return fileDownloadResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

            }
            return fileDownloadResponse;
        }
    }
}