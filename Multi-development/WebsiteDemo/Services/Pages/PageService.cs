using AssociationBusiness.Handlers;
using WebsiteDemo.Models;

namespace WebsiteDemo.Services.Pages
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
            customMenuModels =await _httpClient.GetFromJsonAsync<List<PageModel>>("api/Page/GetAllPages/" + tenantId);
            return customMenuModels;
        }

        
    }
}