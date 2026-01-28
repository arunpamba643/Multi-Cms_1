using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.FileManager;
namespace Demo_App.Pages
{
    public partial class FileManagerTest : ComponentBase
    {
        [Parameter] public string TenantId { get; set; }
        protected override async Task OnInitializedAsync()
        {
        }
    }
}
