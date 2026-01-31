using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public interface ICustomMenuRepository
    {
        List<CustomMenu> GetAllMenus(int TenantId);
        CustomMenu GetMenuById(int Id);
        void DeleteMenuById(int Id);
        Task<int> CreateMenu(CustomMenu customMenu, int PageId);
        Task<int> UpdateMenu(CustomMenu customMenu, int PageId);
        Task<int> BulkUpdateMenu(List<CustomMenu> customMenu);
    }
}
