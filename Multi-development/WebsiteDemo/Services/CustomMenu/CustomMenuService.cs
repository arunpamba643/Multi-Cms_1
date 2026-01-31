using AssociationBusiness.Handlers;
using AssociationEntities.Models.Api;
using AssociationEntities.Models;

namespace WebsiteDemo.Services.Blogging
{
    public class CustomMenuService : ICustomMenuService
    {
        private readonly HttpClient _httpClient;

        public CustomMenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CustomMenuModel>> GetAllMenus(int tenantId)
        {
            List<CustomMenuModel> customMenuModels = new List<CustomMenuModel>();
            var blogs = _httpClient.GetFromJsonAsync<CustomMenu[]>("api/CustomMenu/GetAllMenus/" + tenantId).Result;
            var menus = blogs.Select(x => new CustomMenuModel()
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName,
                PageUrl = x.PageUrl,
                ParentMenuId = x.ParentMenuId.HasValue ? x.ParentMenuId.Value : 0,
                Position = x.Position

            }).AsEnumerable();
            return menus;
        }

        public CustomMenuModel GetMenuById(int Id)
        {
            CustomMenuModel customMenuModel = new CustomMenuModel();
            var menu = _httpClient.GetFromJsonAsync<CustomMenu>("api/CustomMenu/" + Id).Result;
            if (menu != null)
            {
                customMenuModel = new CustomMenuModel()
                {
                    MenuId = menu.MenuId,
                    MenuName = menu.MenuName,
                    PageUrl = menu.PageUrl,
                    ParentMenuId = menu.ParentMenuId.HasValue ? menu.ParentMenuId.Value : 0,
                    Position = menu.Position
                };
            }
            return customMenuModel;
        }

        public async Task<int> CreateMenu(CreateMenuRequest customMenuModel)
        {
            var data = await _httpClient.PostAsJsonAsync("api/CustomMenu", customMenuModel);
            return 1;
        }

        public async Task DeleteMenuById(int Id)
        {
           var data =  await _httpClient.DeleteAsync("/api/CustomMenu/DeleteMenuById/" + Id);
            if(data != null) { }
        }

    }
}
