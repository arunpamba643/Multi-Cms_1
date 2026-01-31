using AssociationEntities.Models;

namespace WebsiteDemo.Services.Blogging
{
    public interface IBloggingService
    {
        IEnumerable<Blog> GetAllBlogs();
        Blog GetBlogById(int tenantId, int blogId);
        Task<int> CreateBlog(Blog dataSendModel);
        IEnumerable<BlogImage> GetAllBlogImages(int BlogId, int TenantId);
        Task DeleteBlogMenuById(int Id);
    }
}