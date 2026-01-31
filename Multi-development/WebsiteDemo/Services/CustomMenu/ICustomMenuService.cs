using AssociationBusiness.Handlers;
using AssociationEntities.Models.Api;

namespace WebsiteDemo.Services.Blogging
{
    public interface ICustomMenuService
    {
        Task<IEnumerable<CustomMenuModel>> GetAllMenus(int tenantId);
        CustomMenuModel GetMenuById(int Id);
        Task DeleteMenuById(int Id);
        Task<int> CreateMenu(CreateMenuRequest blog);

    }
}