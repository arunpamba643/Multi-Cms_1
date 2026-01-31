using AssociationBusiness.Handlers;
using Demo_App.Models;

namespace Demo_App.Services.Pages
{
    public interface IPageService
    {
        Task<IEnumerable<PageModel>> GetAllPages(int tenantId);
        Task<PageModel> GetPageById(int Id);
        Task DeletePageById(int Id);
        Task<int> CreatePage(CreatePageRequest blog);
        Task UpdatePageInfo(UpatePageRequest upatePageRequest);
        Task<PageModel> CheckIfPageMapped(int MenuId, int PageId);
        Task<bool> CheckIfResourcePathAvailable(int TenantId, string ResourcePath, int PageId);
    }
}