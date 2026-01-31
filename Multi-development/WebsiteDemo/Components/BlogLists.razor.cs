//using AssociationEntities.CustomModels;
using WebsiteDemo.Models;
using WebsiteDemo.Services;
//using WebsiteDemo.Services.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebsiteDemo.Components
{
    public partial class BlogLists : ComponentBase
    {

        [Parameter]
        public AssociationEntities.CustomModels.BlogFilters BlogFilters { get; set; }
        [Inject]
        public IBlogPageService blogService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<BlogPagesModel> ListItems = new List<BlogPagesModel>();
        public PaginationModel PaginationModel = new PaginationModel();
        public List<BlogPagesModel> PaginatedList = new List<BlogPagesModel>();
        private async Task LoadList(int Page)
        {
            ListItems = await blogService.GetAllBlogPages(BlogFilters);

            //EventList = pageQuery.ToList();
            var pageQuery = ListItems.Skip((Page - 1) * PaginationModel.PageSize).Take(PaginationModel.PageSize);
            PaginatedList = pageQuery.ToList();
            PaginationModel.TotalItems = ListItems.Count();
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadList(1);
        }

    
    }
}
