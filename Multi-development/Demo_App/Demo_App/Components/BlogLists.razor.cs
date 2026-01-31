//using AssociationEntities.CustomModels;
using Demo_App.Models;
using Demo_App.Services;
using Demo_App.Services.Events;
using Microsoft.AspNetCore.Components;

namespace Demo_App.Components
{
    public partial class BlogLists : ComponentBase
    {

        [Parameter]
        public AssociationEntities.CustomModels.BlogFilters BlogFilters { get; set; }
        [Inject]
        public IBlogPageService blogService { get; set; }

        public List<BlogPagesModel> ListItems = new List<BlogPagesModel>();
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
