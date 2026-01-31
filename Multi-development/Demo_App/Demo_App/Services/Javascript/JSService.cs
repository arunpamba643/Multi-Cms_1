using Microsoft.JSInterop;

namespace Demo_App.Services
{
    public class JSService : IJSService
    {
        private readonly IJSRuntime _jsRuntime;


        public JSService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task RenderFileTree()
        {
            await _jsRuntime.InvokeVoidAsync("myBlazorInterop.loadFileSharingTree");
        }

        public async Task DownloadFile(string fileName, string base64String)
        {
            await _jsRuntime.InvokeVoidAsync("myBlazorInterop.downloadFile", fileName, base64String);
        }
    }
}
