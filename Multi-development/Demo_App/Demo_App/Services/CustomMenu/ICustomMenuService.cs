using AssociationBusiness.Handlers;
using AssociationEntities.Models.Api;
using Demo_App.Models;

namespace Demo_App.Services.Blogging
{
    public interface ICustomMenuService
    {
        Task<IEnumerable<Models.CustomMenuModel>> GetAllMenus(int tenantId);
        Models.CustomMenuModel GetMenuById(int Id);
        Task DeleteMenuById(int Id);
        Task<int> CreateMenu(CreateMenuRequest blog);
        Task<int> BulkUpdateMenu(BulkUpdateMenuRequest customMenuModel);
        Task<int> UpdateMenu(updateMenuRequest updateMenu);

    }
}