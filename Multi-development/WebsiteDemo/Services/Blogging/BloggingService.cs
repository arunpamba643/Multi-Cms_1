using AssociationEntities.Models;

namespace WebsiteDemo.Services.Blogging
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

        public async Task<int> CreateBlog(Blog dataSendModel)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Blogging", dataSendModel);
            return 1;
        }

        public Blog GetBlogById(int tenantId, int blogId)
        {
            var blog = _httpClient.GetFromJsonAsync<Blog>($"api/blogging/GetBloggingById/{tenantId}").Result;
            return blog;
        }

        public IEnumerable<BlogImage> GetAllBlogImages(int TenantId, int BlogId)
        {
            var blogs = _httpClient.GetFromJsonAsync<BlogImage[]>($"api/blogging/GetAllBlogImages/{TenantId}/{BlogId}").Result;
            return blogs;
        }

        public async Task DeleteBlogMenuById(int Id)
        {
            var data = await _httpClient.DeleteAsync("/api/CustomMenu/DeleteMenuById/" + Id);
            if (data != null) { }
        }
    }
}
