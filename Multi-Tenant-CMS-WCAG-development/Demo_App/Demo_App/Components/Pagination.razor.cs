using Microsoft.AspNetCore.Components;
using Demo_App.Models;

namespace Demo_App.Components
{
    public partial class Pagination : ComponentBase
    {
        [Parameter]
        public PaginationModel PaginationModel { get; set; }

        [Parameter]
        public EventCallback<int> PageChanged { get; set; }

        private async Task ChangePage(int page)
        {
            if(PaginationModel.CurrentPage != page)
            {
                PaginationModel.CurrentPage = page;
                await PageChanged.InvokeAsync(page);
            }
        }

        private async Task PreviousPage()
        {
            if (PaginationModel.HasPreviousPage)
            {
                PaginationModel.CurrentPage--;
                await PageChanged.InvokeAsync(PaginationModel.CurrentPage--);
            }
        }

        private async Task NextPage()
        {
            if (PaginationModel.HasNextPage)
            {
                PaginationModel.CurrentPage++;
                await PageChanged.InvokeAsync(PaginationModel.CurrentPage++);
            }
        }
    }
}
