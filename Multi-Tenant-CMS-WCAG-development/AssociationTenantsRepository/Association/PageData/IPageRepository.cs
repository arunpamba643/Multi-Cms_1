using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public interface IPageRepository
    {
        List<Page> GetAlPages(int TenantId);
        Page GetPageById(int Id);
        Task DeletePageById(int Id);
        Task<int> CreatePage(Page page, string pageUrl);
        Task<int> UpdatePage(Page page, string pageUrl);
        Page CheckIfPageMapped(int MenuId, int PageId);
        bool CheckIfResourcePathAvailable(string ResourcePath, int TenantId, int pageId);
    }
}
