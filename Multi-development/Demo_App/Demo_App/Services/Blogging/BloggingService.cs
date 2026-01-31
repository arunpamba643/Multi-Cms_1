using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using Azure;
using Demo_App.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace Demo_App.Services.Blogging
{
    public class BloggingService : IBloggingService
    {
        private readonly HttpClient _httpClient;

        public BloggingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            var blogs = _httpClient.GetFromJsonAsync<Blog[]>("api/blogging/GetAllBloggings").Result;
            return blogs;
        }

        public IEnumerable<BlogImage> GetAllBlogImages(int TenantId, int BlogId)
        {
            var blogs = _httpClient.GetFromJsonAsync<BlogImage[]>($"api/blogging/GetAllBlogImages/{TenantId}/{BlogId}").Result;
            return blogs;
        }

        public async Task<int> CreateBlog(Blog dataSendModel)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Blogging", dataSendModel);
            if(data != null)
            {
                var responseContent = await data.Content.ReadAsStringAsync();
                if(!string.IsNullOrEmpty(responseContent))
                {
                    return Convert.ToInt32(responseContent);
                }
            }
            return 1;
        }

        public async Task<int> CreateBlogImages(List<BlogImage> dataSendModel)
        {
            List<BlogImageModel> blogImages = new List<BlogImageModel>();
            foreach (BlogImage image in dataSendModel)
            {
                blogImages.Add(new BlogImageModel()
                {
                    BlogId = image.BlogId,
                    TenantId = image.TenantId,
                    ImageId = image.ImageId,
                    ImageUrl = image.ImageUrl,
                    Position = image.Position,
                    Text = image.Text,
                    Udf = image.Udf
                });
            }

            var data = await _httpClient.PostAsJsonAsync("api/Blog/CreateBlogImages", blogImages);
            return 1;
        }

        public async Task<Blog> GetBlogById(int tenantId, int blogId)
        {
            try
            {
                var blog = new Blog();
                var responseContent = await _httpClient.GetStringAsync($"api/blogging/GetBloggingById/{tenantId}"); // Use a default value

                if (!string.IsNullOrEmpty(responseContent))
                {
                    blog = JsonConvert.DeserializeObject<Blog>(responseContent);
                }
                return blog;
            }
            catch
            {
                return new Blog();
            }
        }

        public async Task DeleteBlogImageById(int Id)
        {
            var data = await _httpClient.DeleteAsync("/api/Blogging/DeleteBlogImageById/" + Id);
            if (data != null) { }
        }
    }
}
