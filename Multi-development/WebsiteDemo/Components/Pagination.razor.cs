using Microsoft.AspNetCore.Components;
using WebsiteDemo.Models;

namespace WebsiteDemo.Components
{
    public partial class Pagination : ComponentBase
    {
        [Parameter]
        public PaginationModel paginationModel { get; set; }

        [Parameter]
        public EventCallback<int> PageChanged { get; set; }

        private async Task ChangePage(int page)
        {
            if (paginationModel.CurrentPage != page)
            {
                paginationModel.CurrentPage = page;
                await PageChanged.InvokeAsync(page);
            }
        }

        private async Task PreviousPage()
        {
            if (paginationModel.HasPreviousPage)
            {
                paginationModel.CurrentPage--;
                await PageChanged.InvokeAsync(paginationModel.CurrentPage--);
            }
        }

        private async Task NextPage()
        {
            if (paginationModel.HasNextPage)
            {
                paginationModel.CurrentPage++;
                await PageChanged.InvokeAsync(paginationModel.CurrentPage++);
            }
        }
    }
}
