using AssociationBusiness.Handlers;
using AssociationEntities.Models;

namespace Demo_App.Services.Blogging
{
    /// <summary>
    /// Custom Menu Service
    /// </summary>
    public class CustomMenuService : ICustomMenuService
    {
        private readonly HttpClient _httpClient;
        public CustomMenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Method to Get All Menus
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Models.CustomMenuModel>> GetAllMenus(int tenantId)
        {
            List<Models.CustomMenuModel> customMenuModels = new List<Models.CustomMenuModel>();
            customMenuModels = await _httpClient.GetFromJsonAsync<List<Models.CustomMenuModel>>("api/CustomMenu/GetAllMenus/" + tenantId);
            return customMenuModels;
        }

        /// <summary>
        /// Method to Get Menu By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Models.CustomMenuModel GetMenuById(int Id)
        {
            Models.CustomMenuModel customMenuModel = new Models.CustomMenuModel();
            var menu = _httpClient.GetFromJsonAsync<CustomMenu>("api/CustomMenu/" + Id).Result;
            if (menu != null)
            {
                customMenuModel = new Models.CustomMenuModel()
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

        /// <summary>
        /// Method to Create Menu
        /// </summary>
        /// <param name="customMenuModel"></param>
        /// <returns></returns>
        public async Task<int> CreateMenu(CreateMenuRequest customMenuModel)
        {
            var data = await _httpClient.PostAsJsonAsync("api/CustomMenu", customMenuModel);
            return 1;
        }

        /// <summary>
        /// Method to Bulk update menu
        /// </summary>
        /// <param name="customMenuModel"></param>
        /// <returns></returns>
        public async Task<int> BulkUpdateMenu(BulkUpdateMenuRequest customMenuModel)
        {
            var data = await _httpClient.PostAsJsonAsync("/api/CustomMenu/BulkUpdateMenu", customMenuModel);
            return 1;
        }

        /// <summary>
        /// Method to Delete Menu by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteMenuById(int Id)
        {
            var data = await _httpClient.DeleteAsync("/api/CustomMenu/DeleteMenuById/" + Id);
            if (data != null) { }
        }

        /// <summary>
        /// Method to Update Menu
        /// </summary>
        /// <param updateMenuRequest="UpdateMenu"></param>
        /// <returns></returns>
        public async Task<int> UpdateMenu(updateMenuRequest updateMenu)
        {
            var data = await _httpClient.PostAsJsonAsync("api/CustomMenu/UpdateMenu", updateMenu);
            return 1;
        }
    }
}
