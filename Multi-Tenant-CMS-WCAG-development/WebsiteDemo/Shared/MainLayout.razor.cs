using AssociationEntities.Models.Api;
using AssociationEntities.Models;
using Microsoft.AspNetCore.Components;
using System.Text;
using WebsiteDemo.Services.Blogging;
using WebsiteDemo.Services.Pages;
using AssociationEntities.CustomModels;

namespace WebsiteDemo.Shared
{
    public class FooterLink
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
    /// <summary>
    /// Public Dynamic Page
    /// </summary>
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        public IBloggingService BloggingService { get; set; }
        [Inject]
        public ICustomMenuService customMenuService { get; set; }
        [Inject]
        public IPageService pageService { get; set; }

        private Blog blog = new Blog();
        //private MarkupString myHtmlContent;
        //private string htmlContentData;
        private string MenusBinding;
        private string ImagesBinding;
        private List<BlogImage> ImagesData;
        private List<DivModel> divs = new List<DivModel>();
        //public string TenantId { get; set; }
        private IEnumerable<WebsiteDemo.Models.PageModel> Pages = new List<WebsiteDemo.Models.PageModel>();
        [Parameter]
        public string SenantId { get; set; }
        public int tenantId = 0;
        public int blogId = 0;
        public bool isSpinnerLoad = false;
        private List<FooterLink> footerLinks = new List<FooterLink>();

        public int CurrentPage = 1;
        public int PageSize = 5;
        public int TotalItems = 0;
        public int TotalPages = 0;
        public bool HasPreviousPage = false;
        public bool HasNextPage = true;


        public NavigationManager NavigationManager { get; set; }
        public class ImagesModel
        {
            public string ImageUrl { get; set; }
            public string Text { get; set; }
        }

        private List<CustomMenuModel> MenuData = new List<CustomMenuModel>();




        protected override async void OnInitialized()
        {

            object id = null;
            if ((this.Body.Target as RouteView)?.RouteData.RouteValues?.TryGetValue("TenantId", out id) == true)
            {
                TenantId = id?.ToString();
            }

            if (!string.IsNullOrEmpty(TenantId))
            {
                tenantId = Convert.ToInt32(TenantId);
                //blogId = Convert.ToInt32(BlogId);
                blog = BloggingService.GetBlogById(tenantId, blogId);
                blogId = blog.BlogId;
            }
            else
            {
                tenantId = Convert.ToInt32(TenantId);
                blog = BloggingService.GetBlogById(tenantId, 0);
                blogId = blog.BlogId;
            }
            StringBuilder htmlBuilder = new StringBuilder();

            var data = await customMenuService.GetAllMenus(tenantId);
            MenuData = data.OrderBy(x => x.Position).ToList();
            MenuData.ForEach(x => x.ParentMenu = x.ParentMenuId.ToString());
            MenuData.ForEach(x => x.Menu = x.MenuId.ToString());
            // ImagesData = BloggingService.GetAllBlogImages(tenantId, blogId).ToList();
            MenuData.ForEach(x => x.PageUrl = $"/Content/{tenantId}/" + x.PageUrl);
            isSpinnerLoad = true;

            Pages = await pageService.GetAllPages(tenantId);

            footerLinks =
   (from Page in Pages
    join Menus in MenuData on Page.MenuId equals Menus.MenuId
    select new FooterLink
    {
        Name = Page.PageTitle,
        Link = Menus.PageUrl

    }).Distinct().ToList();

            StateHasChanged();

        }

        private string GridClass
        {
            get
            {
                if (divs.Count <= 3)
                {
                    return divs.Count switch
                    {
                        1 => "grid-cols-1 ",
                        2 => "grid-cols-2",
                        3 => "grid-cols-3 ",
                        _ => "grid-cols-1"
                    };
                }
                else
                {
                    return "grid-cols-3";
                }
            }
        }

        private class DivModel
        {
            public string Name { get; set; }
            public string Input1Value { get; set; }
            public string Input2Value { get; set; }
        }

    }

}

