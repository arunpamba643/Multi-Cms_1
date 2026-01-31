using AssociationBusiness.Handlers;
using Demo_App.Models;
namespace Demo_App.Services
{
    public interface IBlogPageService
    {
        Task<List<BlogPagesModel>> GetAllBlogPages(AssociationEntities.CustomModels.BlogFilters blogFilters);
        BlogPagesModel GetBlogPageById(int Id);
        Task DeleteBlogPageById(int Id);
        Task<int> CreateBlogPage(CreateBlogPageRequest blog);
        Task UpdateBlogPage(CreateBlogPageRequest updatedPost);
        Task<List<BlogList>> GetAllBlogsFromAPI();
        List<AssociationEntities.CustomModels.BlogFilterSuggestions> GetBlogFilterSuggestions(int TenantId);

    }
}
