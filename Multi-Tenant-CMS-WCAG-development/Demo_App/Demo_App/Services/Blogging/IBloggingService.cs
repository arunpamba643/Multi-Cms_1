using AssociationEntities.Models;
using Demo_App.Models;
using static Demo_App.Services.Blogging.BloggingService;

namespace Demo_App.Services.Blogging
{
    public interface IBloggingService
    {
        IEnumerable<Blog> GetAllBlogs();
        Task<Blog> GetBlogById(int tenantId, int blogId);
        Task<int> CreateBlog(Blog dataSendModel);
        Task<int> CreateBlogImages(List<BlogImage> dataSendModel);
        IEnumerable<BlogImage> GetAllBlogImages(int BlogId, int TenantId);
        Task DeleteBlogImageById(int Id);
    }
}