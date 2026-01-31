using Microsoft.JSInterop;

namespace WebsiteDemo.Services
{
    public class JSService : IJSService
    {
        private readonly IJSRuntime _jsRuntime;


        public JSService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task DownloadFile(string fileName, string base64String)
        {
            await _jsRuntime.InvokeVoidAsync("myBlazorInterop.downloadFile", fileName, base64String);
        }
    }
}
