using AssociationBusiness.Handlers;

namespace Demo_App.Services
{
    public class ContainerService : IContainerService
    {
        private readonly HttpClient _httpClient;

        public ContainerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> CreateContainer(CreateContainerRequest createContainerRequest)
        {
            HttpResponseMessage data = await _httpClient.PostAsJsonAsync("api/Container/CreateContainer", createContainerRequest);
            if (data.IsSuccessStatusCode)
            {
                string responseBody = await data.Content.ReadAsStringAsync();
                return Convert.ToInt32(responseBody);
            }

            return 0;

        }
    }
}
