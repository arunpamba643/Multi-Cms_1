using AssociationEntities.Common;
using AssociationEntities.Models;
using Demo_App.Models;
using Demo_App.Services.Blogging;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Demo_App.Pages
{
    public partial class Customized : ComponentBase
    {
        [Inject]
        public IBloggingService BloggingService { get; set; }
        [Inject]
        public ICustomMenuService customMenuService { get; set; }
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Parameter]
        public string TenantId { get; set; }
        [Parameter]
        public string BlogId { get; set; }
        public int tenantId = 0;
        public int blogId = 0;
        private Blog blog = new Blog();
        private bool isLoading = false;
        private string selectedL1Size = "";
        private string selectedL2Size = "";
        private string selectedL3Size = "";

        private List<CustomMenuModel> MenuData = new List<CustomMenuModel>();
        List<string> fontNames = new List<string> { "Arial", "Calibri", "Cambria", "Comic Sans MS", "Courier New", "Georgia", "Times New Roman", "Verdana", "Tahoma", "Palatino Linotype", "Lucida Sans Unicode", "Trebuchet MS", "MS Sans Serif", "MS Serif", "Symbol", "Wingdings" };

        //private string base64Html = "";
     
        //private string urlToOpen = "https://example.com";
        
        //private List<DivModel> divs = new List<DivModel>();
        //private int containerCount = 0;
        //private bool enabled = false;
       // private string setSelectedKey = "home";
        private string selectedValue = "";
       // private string selectedPixels = "";
        public List<TeamsDataModel> teamsData = new List<TeamsDataModel>() {
         new TeamsDataModel(){  Text="Arushi",Image="https://img.freepik.com/free-photo/smiling-young-male-professional-standing-with-arms-crossed-while-making-eye-contact-against-isolated-background_662251-838.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Jonah Nye",Image="https://img.freepik.com/free-photo/young-attractive-emotional-girl-business-style-clothes_78826-2303.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Michel",Image="https://img.freepik.com/free-photo/cheerful-curly-business-girl-wearing-glasses_176420-206.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Sandrey",Image="https://img.freepik.com/free-photo/smiley-businesswoman-posing-outdoors-with-arms-crossed-copy-space_23-2148767055.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Igbal",Image="https://img.freepik.com/free-photo/confident-young-businessman-suit-standing-with-arms-folded_171337-18599.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
         new TeamsDataModel(){  Text="Amit Mistra",Image="https://img.freepik.com/free-photo/young-handsome-business-man-with-laptop-office_1303-21060.jpg?size=626&ext=jpg&ga=GA1.1.2074308802.1693138254&semt=ais"},
        };
        public IEnumerable<Blog> Blogs { get; set; }

        /// <summary>
        /// On Page Load
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(TenantId) && !string.IsNullOrEmpty(BlogId))
            {
                tenantId = Convert.ToInt32(TenantId);
                blogId = Convert.ToInt32(BlogId);
                await GetBlogData(tenantId, blogId);
            }
            else
            {
                tenantId = Convert.ToInt32(TenantId);
                await GetBlogData(tenantId, 0);
            }

            await GetAllMenus();
        }
        
        /// <summary>
        /// Method to Publish the changes to tenant
        /// </summary>
        /// <returns></returns>
        public async Task toDisplay()
        {
            isLoading = true;
            await JSRuntime.InvokeVoidAsync("showSpinner");

            blog.TenantId = tenantId;
            blogId = await BloggingService.CreateBlog(blog);
            blog.BlogId = blogId;

            isLoading = false;

            await JSRuntime.InvokeVoidAsync("hideSpinner");
            Navigation.NavigateTo("/customize/" + tenantId);
        }

        /// <summary>
        /// Get Blog Data based on tenantId
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        private async Task GetBlogData(int tenantId, int blogId)
        {
            if (blogId != 0)
            {
                blogId = Convert.ToInt32(BlogId);

                Blogs = BloggingService.GetAllBlogs();

                blog = Blogs.FirstOrDefault(c => c.BlogId == blogId);

                blog.BrandName = Configurations.BrandName;
                blog.Logo = Configurations.Logo;
                blog.TextContent = Configurations.TextContent;
                blog.Heading = Configurations.Heading;
                blog.FooterText = Configurations.FooterText;
                return;
            }
            blog = await BloggingService.GetBlogById(tenantId, blogId);
            if (blog.BlogId != 0)
            {
                blogId = blog.BlogId;
                blog.HeaderBackgroundColor = !string.IsNullOrEmpty(blog.HeaderBackgroundColor) ? blog.HeaderBackgroundColor : Configurations.HeaderBackgroundColor;
                blog.HeaderFontColor = !string.IsNullOrEmpty(blog.HeaderFontColor) ? blog.HeaderFontColor : Configurations.HeaderFontColor;
                blog.BrandName = !string.IsNullOrEmpty(blog.BrandName) ? blog.BrandName : Configurations.BrandName;
                blog.Logo = !string.IsNullOrEmpty(blog.Logo) ? blog.Logo : Configurations.Logo;
                blog.Banner = !string.IsNullOrEmpty(blog.Banner) ? blog.Banner : Configurations.Banner;
                blog.Heading = !string.IsNullOrEmpty(blog.Heading) ? blog.Heading : Configurations.Heading;
                blog.TextContent = !string.IsNullOrEmpty(blog.TextContent) ? blog.TextContent : Configurations.TextContent;
                blog.ContentFontSize = !string.IsNullOrEmpty(blog.ContentFontSize) ? blog.ContentFontSize : Configurations.ContentFontSize;
                blog.ContentFontColor = !string.IsNullOrEmpty(blog.ContentFontColor) ? blog.ContentFontColor : Configurations.ContentFontColor;
                blog.FooterBackgroundColor = !string.IsNullOrEmpty(blog.FooterBackgroundColor) ? blog.FooterBackgroundColor : Configurations.FooterBackgroundColor;
                blog.FooterText = !string.IsNullOrEmpty(blog.FooterText) ? blog.FooterText : Configurations.FooterText;

            }
            else
            {
                Navigation.NavigateTo("/Templates/" + tenantId);
            }

        }

        /// <summary>
        /// Get All Blogs
        /// </summary>
        /// <returns></returns>
        protected async Task GetAllBlogs()
        {
            Blogs = (BloggingService.GetAllBlogs()).ToList();
        }

        /// <summary>
        /// Get All Menus to show on the preview
        /// </summary>
        /// <returns></returns>
        protected async Task GetAllMenus()
        {
            var data = await customMenuService.GetAllMenus(tenantId);
            MenuData = data.OrderBy(x => x.Position).ToList();
        }

        #region Update preview data copy values to the model

        private void updateHeaderColor(ChangeEventArgs e)
        {
            blog.HeaderBackgroundColor = e.Value.ToString();

        }
        private void updateHeaderFontColor(ChangeEventArgs e)
        {
            blog.HeaderFontColor = e.Value.ToString();

        }
        private void updateFooterFontColor(ChangeEventArgs e)
        {
            blog.FooterFontColor = e.Value.ToString();

        }
        private void updateFooterColor(ChangeEventArgs e)
        {
            blog.FooterBackgroundColor = e.Value.ToString();
        }
        private void updateContentColor(ChangeEventArgs e)
        {
            blog.ContentFontColor = e.Value.ToString();
        }
        private void updateFontSize(ChangeEventArgs e)
        {
            blog.ContentFontSize = e.Value.ToString();
        }
        private void updateHeaderLogo(ChangeEventArgs e)
        {
            blog.Logo = e.Value.ToString();
        }
        private void updateBanner(ChangeEventArgs e)
        {
            blog.Banner = e.Value.ToString();
        }
        private void updateBrandName(ChangeEventArgs e)
        {
            blog.BrandName = e.Value.ToString();
        }
        private void updateHeading(ChangeEventArgs e)
        {
            blog.Heading = e.Value.ToString();
        }
        private void updateBlogContent(ChangeEventArgs e)
        {
            blog.TextContent = e.Value.ToString();
        }
        private void updateFacebookLink(ChangeEventArgs e)
        {
            blog.Facebook = e.Value.ToString();
        }
        private void updateTwitterLink(ChangeEventArgs e)
        {
            blog.Twitter = e.Value.ToString();
        }
        private void updateWhatsappLink(ChangeEventArgs e)
        {
            blog.WhatsApp = e.Value.ToString();
        }
        private void updateLinkedInLink(ChangeEventArgs e)

        {

            blog.LinkedIn = e.Value.ToString();

        }
        private void updateInstagramLink(ChangeEventArgs e)

        {

            blog.Instagram = e.Value.ToString();

        }
        private void updateFooterText(ChangeEventArgs e)
        {
            blog.FooterText = e.Value.ToString();
        }

        #endregion

        #region Preview details on Change Javascript related changes for color picker , font styles and font sizes

        private void HandleSelectChange(ChangeEventArgs e)
        {
            selectedValue = e.Value.ToString();
            blog.ContentFontSize = selectedValue + "px";
        }
        public async Task toShowHeaderColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowHeaderColorPicker");
        }
        public async Task toShowHeaderFontColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowHeaderFontColorPicker");
        }
        public async Task toShowFooterFontColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowFooterFontColorPicker");
        }
        public async Task toShowContentColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowContentColorPicker");
        }
        public async Task toShowFooterColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowFooterColorPicker");
        }
        public async Task toShowL1ColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowL1ColorPicker");
        }
        public async Task toShowL2ColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowL2ColorPicker");
        }
        public async Task toShowL3ColorPicker()
        {
            await JSRuntime.InvokeVoidAsync("toShowL3ColorPicker");
        }
        private void upadteL1Color(ChangeEventArgs e)
        {
            blog.MainMenuColor = e.Value.ToString();
        }
        private void upadteL1Size(ChangeEventArgs e)
        {
            blog.MainMenuFontSize = e.Value.ToString();
        }
        private void HandleMainMenuChange(ChangeEventArgs e)
        {
            selectedValue = e.Value.ToString();
            blog.MainMenuFontSize = selectedValue + "px";
        }
        private void updateL1Style(ChangeEventArgs e)
        {
            blog.MainMenuFontStyle = e.Value.ToString();
        }
        private void upadteL2Color(ChangeEventArgs e)
        {
            blog.SubMenuColor = e.Value.ToString();
        }
        private void upadteL2Size(ChangeEventArgs e)
        {
            selectedL2Size = e.Value.ToString();
            blog.SubMenuFontSize = selectedL2Size + "px";
        }
        private void updateL2Style(ChangeEventArgs e)
        {
            blog.SubMenuFontStyle = e.Value.ToString();
        }
        private void upadteL3Color(ChangeEventArgs e)
        {
            blog.SubChildMenuColor = e.Value.ToString();
        }
        private void upadteL3Size(ChangeEventArgs e)
        {
            selectedL3Size = e.Value.ToString();
            blog.SubChildMenuFontSize = selectedL3Size + "px";
        }
        private void updateL3Style(ChangeEventArgs e)
        {
            blog.SubChildFontStyle = e.Value.ToString();
        }
        private void updateHeaderFontStyle(ChangeEventArgs e)
        {
            blog.HeaderFontStyle = e.Value.ToString();
        }
        private void updateContentFontStyle(ChangeEventArgs e)
        {
            blog.ContentFontStyle = e.Value.ToString();
        }
        private void updateFooterFontStyle(ChangeEventArgs e)
        {
            blog.FooterFontStyle = e.Value.ToString();
        }

        #endregion
       
        public class TeamsDataModel
        {
            public string Image { get; set; }
            public string Text { get; set; }
        }

    }
}
