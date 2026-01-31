
using AssociationBusiness.Handlers;
using Azure;

namespace Demo_App.Services
{
    public class ComponentPropertyService : IComponentPropertyService
    {
        private readonly HttpClient _httpClient;
        public ComponentPropertyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CreateComponentProperties(CreateComponentPropertiesRequest createComponentPropertiesRequest)
        {
            HttpResponseMessage data = await _httpClient.PostAsJsonAsync("api/ComponentProperty/CreateComponentProperties", createComponentPropertiesRequest);

            if (data.IsSuccessStatusCode)
            {
                string responseBody = await data.Content.ReadAsStringAsync();
                return Convert.ToInt32(responseBody);
            }

            return 0; 

        }

        public async Task<int> DeleteComponentPropertiesByComponentId(int ComponentId)
        {
            var data = await _httpClient.GetAsync("api/ComponentProperty/DeleteComponentPropertiesByComponentId/" + ComponentId);
            return 0;
        }
    }
}
