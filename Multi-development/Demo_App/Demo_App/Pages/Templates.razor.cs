using AssociationEntities.Models;
using Demo_App.Services.Blogging;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Navigations;

namespace Demo_App.Pages
{
    public partial class Templates : ComponentBase
    {
        [Inject]
        public IBloggingService BloggingService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        SfPager Page;

        [Parameter]
        public string TenantId { get; set; }

        //public List<Blog> blogs { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public int pageSize { get; set; } = 2;
        public int TotalCount { get; set; }
        public List<int> pagesizes = new List<int> { 1, 2, 3 };
        public int SkipValue;
        public int TakeValue;

        public String fontSize = "12px";

        protected override void OnInitialized()
        {
            LoadData();
            base.OnInitialized();
        }
        private void LoadData()
        {
            Blogs = BloggingService.GetAllBlogs();
            TotalCount = Blogs.Count();
            TakeValue = pageSize;
        }
        public void HandleNumericClick(PagerItemClickEventArgs args)
        {
            SkipValue = (args.CurrentPage * Page.PageSize) - Page.PageSize;
            TakeValue = Page.PageSize;
        }

        public void Change(PageSizeChangingArgs args)
        {
            if (args.SelectedPageSize == "All")
            {
                TakeValue = Page.TotalItemsCount;
            }
            else
            {
                TakeValue = int.Parse(args.SelectedPageSize);
            }
            SkipValue = 0;
        }

  //      public List<Blog> blogs = new List<Blog>() {
  //new Blog(){ BlogId = 1,
  //    HeaderBackgroundColor ="#983434",
  //    HeaderFontColor="#FEDA3B",
  //    ContentFontColor ="#cb4343",
  //    FooterBackgroundColor = "#983434",
  //},
  // new Blog(){ BlogId = 2,
  //    HeaderBackgroundColor ="#F95557",
  //    HeaderFontColor="#FEDA3B",
  //    ContentFontColor ="#cb4343",
  //    FooterBackgroundColor = "#F95557",
  //},
  // new Blog(){ BlogId = 3,
  //    HeaderBackgroundColor ="#F0F727",
  //    HeaderFontColor="#FEDA3B",
  //    ContentFontColor ="#cb4343",
  //    FooterBackgroundColor = "#F0F727",
  //},
  //new Blog(){ BlogId = 4,
  //    HeaderBackgroundColor ="#ABF727",
  //    HeaderFontColor="#FEDA3B",
  //    ContentFontColor ="#cb4343",
  //    FooterBackgroundColor = "#ABF727",
  //},
  // new Blog(){ BlogId = 5,
  //    HeaderBackgroundColor ="#27F789",
  //    HeaderFontColor="#FEDA3B",
  //    ContentFontColor ="#cb4343",
  //    FooterBackgroundColor = "#27F789",
  //},
  // new Blog(){ BlogId = 6,
  //    HeaderBackgroundColor ="#27F7F4",
  //    HeaderFontColor="#FEDA3B",
  //    ContentFontColor ="#cb4343",
  //    FooterBackgroundColor = "#27F7F4",
  //},
  //};
    }
}
