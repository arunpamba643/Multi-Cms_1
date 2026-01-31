using AssociationEntities.Models.Api;
using AssociationEntities.Models;
using Microsoft.AspNetCore.Components;
using System.Text;
using WebsiteDemo.Services.Blogging;

namespace WebsiteDemo.Pages
{
    public partial class Team : ComponentBase
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
        public List<TeamsDataModel> teamsData = new List<TeamsDataModel>() {
         new TeamsDataModel(){  Text="Arushi",Image="https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Jonah Nye",Image="https://img.freepik.com/free-photo/young-attractive-emotional-girl-business-style-clothes_78826-2303.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Michel",Image="https://img.freepik.com/free-photo/cheerful-curly-business-girl-wearing-glasses_176420-206.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Sandrey",Image="https://img.freepik.com/free-photo/smiley-businesswoman-posing-outdoors-with-arms-crossed-copy-space_23-2148767055.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Igbal",Image="https://img.freepik.com/free-photo/confident-young-businessman-suit-standing-with-arms-folded_171337-18599.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Amit Mistra",Image="https://img.freepik.com/free-photo/young-handsome-business-man-with-laptop-office_1303-21060.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
        };
        [Parameter]
        public string TenantId { get; set; }
        //[Parameter]
        //public string BlogId { get; set; }
        public int tenantId = 0;
        public int blogId = 0;

        public class TeamsDataModel
        {
            public string Image { get; set; }

            public string Text { get; set; }
        }

        private string GridClass
        {
            get
            {
                if (ImagesData.Count <= 3)
                {
                    return ImagesData.Count switch
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
        public class ImagesModel
        {
            public string ImageUrl { get; set; }
            public string Text { get; set; }
        }

        private List<CustomMenuModel> MenuData = new List<CustomMenuModel>();

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
            foreach (var item in MenuData)
            {
                htmlBuilder.Append($"<div><button onclick=\"onClick('{item.PageUrl}')\">{item.MenuName}</button></div>");
            }

            MenusBinding = htmlBuilder.ToString();

            StringBuilder htmlBuilderForImages = new StringBuilder();

            htmlBuilderForImages.Append("<div class=\"grid grid-cols-3  gap-4 h-auto\" id=\"container\" style=\"min-height:20vh\">");

            ImagesData = BloggingService.GetAllBlogImages(tenantId, blogId).ToList();
            foreach (var item in ImagesData)
            {
                htmlBuilderForImages.Append($"<div class=\"p-1 rounded\"><img src=\"{item.ImageUrl}\" class=\"w-full\"><p>{item.Text}</p></div>");
            }
            htmlBuilderForImages.Append("</div>");
            ImagesBinding = htmlBuilderForImages.ToString();
        }
    }
}
