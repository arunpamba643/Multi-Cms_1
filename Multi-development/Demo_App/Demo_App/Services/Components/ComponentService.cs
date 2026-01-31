using AssociationBusiness.Handlers;
using Demo_App.Models;
using Syncfusion.Blazor.RichTextEditor;



namespace Demo_App.Services
{
    public class ComponentService : IComponentService
    {
        private readonly HttpClient _httpClient;
        public ComponentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateComponents(CreateComponentsRequest Item)
        {
            HttpResponseMessage data = await _httpClient.PostAsJsonAsync("api/Component/CreateComponents", Item);
        }

        public async Task UpdateComponent(UpdateComponentRequest updateComponentRequest)
        {
            HttpResponseMessage data = await _httpClient.PostAsJsonAsync("api/Component/UpdateComponent", updateComponentRequest);
        }

        async Task<int> IComponentService.CreateComponent(CreateComponentRequest Item)
        {

            HttpResponseMessage data = await _httpClient.PostAsJsonAsync("api/Component/CreateComponent", Item);
            if (data.IsSuccessStatusCode)
            {
                string responseBody = await data.Content.ReadAsStringAsync();
                return Convert.ToInt32(responseBody);
            }
            return 0;
        }
 

        async Task IComponentService.DeleteComponentByComponentId(int Id)
        {
            var data = _httpClient.GetAsync("api/Component/DeleteComponentByComponentId/" + Id);
        }
    }
}
