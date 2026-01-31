
using AssociationEntities.Models;

namespace WebsiteDemo.Services
{
    public class RowService : IRowService
    {
        private readonly HttpClient _httpClient;
        public RowService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Row>> GetAllRowsByPageId(int PageId)
        {
            var rows = await _httpClient.GetFromJsonAsync<List<Row>>("api/Row/GetAllRowsByPageId/" + PageId);
            return rows;
        }


    }
}
