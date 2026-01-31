using AssociationBusiness.Handlers;
using Demo_App.Services.Pages;
using Microsoft.AspNetCore.Components;

namespace Demo_App.Pages
{
    public partial class PageList : ComponentBase
    {
        [Inject]
        public IPageService pageService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Parameter]
        public string TenantId { get; set; }

        public int tenantId = 0;

        public IEnumerable<Models.PageModel> controlPages = new List<Models.PageModel>();

        /// <summary>
        /// On Initialized Method
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            tenantId = !string.IsNullOrEmpty(TenantId) ? Convert.ToInt32(TenantId) : 0;
            await FetchPages();
        }

        /// <summary>
        /// Fetches all pages associated with the current tenant asynchronously.
        /// </summary>
        public async Task FetchPages()
        {
            controlPages = await pageService.GetAllPages(tenantId);

        }

        /// <summary>
        /// Navigates to the page creation route for the current tenant.
        /// </summary>
        private async Task CreateNewPage()
        {
            navigationManager.NavigateTo($"/ControlPage/{tenantId}/0");
        }

        /// <summary>
        /// Deletes a page with the specified ID asynchronously.
        /// </summary>
        /// <param name="pageId">The ID of the page to delete.</param>
        private async Task DeletePageById(int pageId)
        {
            await pageService.DeletePageById(pageId);
            await FetchPages();
        }
    }
}
