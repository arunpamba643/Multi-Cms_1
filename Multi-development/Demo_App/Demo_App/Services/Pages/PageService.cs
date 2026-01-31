using AssociationBusiness.Handlers;
using Demo_App.Models;

namespace Demo_App.Services.Pages
{
    /// <summary>
    /// Page Service
    /// </summary>
    public class PageService : IPageService
    {
        private readonly HttpClient _httpClient;
        public PageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Method to Get All Pages
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PageModel>> GetAllPages(int tenantId)
        {
            List<PageModel> customMenuModels = new List<PageModel>();
            customMenuModels = await _httpClient.GetFromJsonAsync<List<PageModel>>("api/Page/GetAllPages/" + tenantId.ToString());
            return customMenuModels;
        }

        /// <summary>
        /// Method to Get Menu By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<PageModel> GetPageById(int Id)
        {
            PageModel customMenuModel = new PageModel();
            var menu = _httpClient.GetFromJsonAsync<AssociationEntities.Models.Page>("api/Page/GetPageById/" + Id).Result;
            if (menu != null)
            {
                customMenuModel = new PageModel()
                {
                    Id = menu.Id,
                    MenuId = menu.MenuId,
                    PageTitle = menu.PageTitle,
                    IsHomePage = menu.IsHomePage,
                    ResourcePath = menu.ResourcePath,
                    IsLandingPage = menu.IsLandingPage,
                    TenantId = menu.TenantId,
                    PageScopeType = menu.PageScopeType
                };
            }
            return customMenuModel;
        }

        /// <summary>
        /// Method to Create Page
        /// </summary>
        /// <param name="customMenuModel"></param>
        /// <returns></returns>
        public async Task<int> CreatePage(CreatePageRequest customMenuModel)
        {
            HttpResponseMessage data = await _httpClient.PostAsJsonAsync("api/Page", customMenuModel);
            if (data.IsSuccessStatusCode)
            {
                string responseBody = await data.Content.ReadAsStringAsync();
                return Convert.ToInt32(responseBody);
            }
            return 0;
        }

        /// <summary>
        /// Method to Delete Page by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeletePageById(int Id)
        {
            var data = await _httpClient.DeleteAsync("/api/Page/DeletePageById/" + Id);
            if (data != null) { }
        }


        public async Task UpdatePageInfo(UpatePageRequest upatePageRequest)
        {
            var data = await _httpClient.PostAsJsonAsync("/api/Page/UpdatePage", upatePageRequest);
        }

        /// <summary>
        /// Method to check if page is mapped to a menu
        /// </summary>
        /// <returns></returns>
        public Task<PageModel> CheckIfPageMapped(int MenuId, int PageId)
        {
            PageModel customMenuModel = new PageModel();
            var menu = _httpClient.GetFromJsonAsync<AssociationEntities.Models.Page>($"api/Page/CheckIfPageMappedToMenu/{MenuId}/{PageId}").Result;
            if (menu != null)
            {
                customMenuModel = new PageModel()
                {
                    Id = menu.Id,
                    MenuId = menu.MenuId,
                    PageTitle = menu.PageTitle,
                    IsHomePage = menu.IsHomePage,
                    ResourcePath = menu.ResourcePath,
                    IsLandingPage = menu.IsLandingPage,
                    TenantId = menu.TenantId,
                    PageScopeType = menu.PageScopeType
                };
            }
            return Task.FromResult(customMenuModel);
        }

        /// <summary>
        /// Method to check if resource path is available
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckIfResourcePathAvailable(int TenantId, string ResourcePath, int PageId)
        {
            var result = await _httpClient.GetFromJsonAsync<bool>($"api/Page/CheckIfResourcePathAvailable/{TenantId}/{ResourcePath}/{PageId}");
            return result;
        }

    }
}