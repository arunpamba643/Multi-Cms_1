using AssociationEntities.Models.Api;
using AssociationEntities.Models;
using Microsoft.AspNetCore.Components;
using System.Text;
using WebsiteDemo.Services.Blogging;

namespace WebsiteDemo.Pages
{
    /// <summary>
    /// Public Dynamic Page
    /// </summary>
    public partial class PublicDynamic : ComponentBase
    {
        [Inject]
        public IBloggingService BloggingService { get; set; }
        [Inject]
        public ICustomMenuService customMenuService { get; set; }
        private Blog blog = new Blog();
        private MarkupString myHtmlContent;
        private string htmlContentData;
        private string MenusBinding;
        private string ImagesBinding;
        private List<BlogImage> ImagesData;
        private List<DivModel> divs = new List<DivModel>();
        [Parameter]
        public string TenantId { get; set; }
        public int tenantId = 0;
        public int blogId = 0;

        public DataModel Data = new DataModel()
        {
            Rows = new List<RowModel>()
                 {
                     new RowModel()
                     {
                          rowId = 1,
                          containers = new List<Containers>(){
                          new Containers(){  ContainerId = 1, PageComponent ="TeamWithRoundImage"},
                          new Containers(){  ContainerId = 2, PageComponent ="TeamWithRoundImage"},
                          }
                     },
                     new RowModel()
                     {
                          rowId = 2,
                          containers = new List<Containers>(){
                          new Containers(){  ContainerId = 1, PageComponent ="TeamWithRoundImage"},
                          new Containers(){  ContainerId = 2, PageComponent ="TeamWithRoundImage"},
                          new Containers(){  ContainerId = 3, PageComponent ="TeamWithRoundImage"},
                          }
                     },
                     new RowModel()
                     {
                          rowId = 3,
                          containers = new List<Containers>(){
                          new Containers(){  ContainerId = 1, PageComponent ="TeamWithRoundImage"},
                          new Containers(){  ContainerId = 2, PageComponent ="TeamWithRoundImage"},
                          new Containers(){  ContainerId = 3, PageComponent ="TeamWithRoundImage"},
                          new Containers(){  ContainerId = 4, PageComponent ="TeamWithRoundImage"},
                          }
                     }
                 }
        };


        public class ImagesModel
        {
            public string ImageUrl { get; set; }
            public string Text { get; set; }
        }

        private string GetGridClass(int numberOfColumns)
        {
            if (numberOfColumns <= 4)
            {
                return numberOfColumns switch
                {
                    1 => "grid-cols-1",
                    2 => "grid-cols-2",
                    3 => "grid-cols-3",
                    4 => "grid-cols-4",
                    _ => "grid-cols-1"
                };
            }
            else
            {
                return "grid-cols-2";
            }
        }

        private List<CustomMenuModel> MenuData = new List<CustomMenuModel>();

        /// <summary>
        /// On Page Load
        /// </summary>
        protected override async void OnInitialized()
        {
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
            //foreach (var item in MenuData)
            //{
            //    htmlBuilder.Append($"<div><a href=\"{item.PageUrl}/{tenantId}\">{item.MenuName}</a></div>");
            //}

            //MenusBinding = htmlBuilder.ToString();

            //StringBuilder htmlBuilderForImages = new StringBuilder();

            //htmlBuilderForImages.Append("<div class=\"grid grid-cols-3  gap-4 h-auto\" id=\"container\" style=\"min-height:20vh\">");

            ImagesData = BloggingService.GetAllBlogImages(tenantId, blogId).ToList();
            //foreach (var item in ImagesData)
            //{
            //    htmlBuilderForImages.Append($"<div class=\"p-1 rounded\"><img src=\"{item.ImageUrl}\" class=\"w-full\"><p>{item.Text}</p></div>");
            //}
            //htmlBuilderForImages.Append("</div>");
            //ImagesBinding = htmlBuilderForImages.ToString();
            // htmlContentData = blog.DynamicHtmltemplate;
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

        public class DataModel
        {
            public List<RowModel> Rows { get; set; }
        }

        public class RowModel
        {
            public int rowId { get; set; }
            public List<Containers> containers { get; set; }
        }

        public class Containers
        {
            public int ContainerId { get; set; }
            public string PageComponent { get; set; }
        }
    }
}
