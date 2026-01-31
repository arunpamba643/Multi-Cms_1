using WebsiteDemo.Models;
namespace WebsiteDemo.Services
{
    public interface IBlogPageService
    {
        Task<IEnumerable<BlogPagesModel>> GetAllBlogPages(AssociationEntities.CustomModels.BlogFilters blogFilters );
        BlogPagesModel GetBlogPageById(int Id);
        Task<List<BlogList>> GetAllBlogsFromAPI();
    }
}
