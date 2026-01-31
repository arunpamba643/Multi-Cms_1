using System.Collections.Generic;
using System.Threading.Tasks;
using AssociationEntities.CustomModels;
using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public interface IBlogPagesRepository
    {
        Task<List<BlogPost>> GetAllBlogPages(BlogFilters blogFilters);
        BlogPost GetBlogPageById(int Id);
        void DeleteBlogPageById(int Id);
        Task<int> CreateBlogPage(BlogPost blogPost);
        Task<int> UpdateBlogPage(BlogPost blogPost);
        Task<List<BlogFilterSuggestions>> GetBlogFilterSuggestions(int TenantId);
    }
}
