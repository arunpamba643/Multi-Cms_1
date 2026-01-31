using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public interface IBlogRepository
    {
        Blog GetBlogById(int Id);

        Task<bool> CheckTenantBlogExists(int tenantId, int blogId);
        List<Blog> GetAllBlogs();
        List<BlogImage> GetAllBlogImages(int BlogId, int TenantId);
        Task<int> CreateBlog(Blog blog);
        Task<int> UpdateBlog(Blog blog);
        Task<int> CreateBlogImage(BlogImage blog);
        Task<int> UpdateBlogImage(BlogImage blog);
        Task<int> CreateBlogImages(List<BlogImage> blog);
        Task DeleteBlogImageById(int Id);
    }
}
