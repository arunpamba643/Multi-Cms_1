using WebsiteDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AssociationEntities.CustomModels;
using System.Net.Http.Json;
using Syncfusion.Blazor.RichTextEditor;

namespace WebsiteDemo.Services.Events
{

    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Models.EventModel>> GetAllEvents()
        {
            var data = await _httpClient.GetAsync("https://api.search.sportcloud.de/api/event?from=2024-03-19T08%3A52%3A10.000Z&page=1&pagesize=25&orderbytype=ascending&orderby=StartTime&keyword=&searchfields=Name");
            List<Models.EventModel> EventList = new List<Models.EventModel>();
            if (data.IsSuccessStatusCode)
            {
                string response = await data.Content.ReadAsStringAsync();
                var jsonResponse = (JObject)JsonConvert.DeserializeObject(response);
                var items = jsonResponse["items"];



                foreach (var item in items)
                {
                    Models.EventModel model = new Models.EventModel();
                    model.EventId = Convert.ToInt32(item["$id"]);
                    model.CreatorName = item["creator"]["name"].ToString();
                    model.EventName = item["name"].ToString();
                    model.EventStartTime = DateTime.Parse(item["startTime"].ToString());
                    model.EventType = item["eventType"].ToString();
                    model.EventDescription = item["description"].ToString();
                    EventList.Add(model);
                }
            }

            return EventList;
        }


        public async Task<List<AssociationEntities.CustomModels.EventModel>> GetAllEventsFromJSON(EventFilters eventFilters)
        {
            //var query = $"from={Uri.EscapeDataString(eventFilters.From.ToString("o"))}" +
            //   $"&page={eventFilters.Page}" +
            //   $"&pagesize={eventFilters.PageSize}" +
            //   $"&orderbytype={Uri.EscapeDataString(eventFilters.OrderByType)}" +
            //   $"&orderby={Uri.EscapeDataString(eventFilters.OrderBy)}" +
            //   $"&keyword={Uri.EscapeDataString(eventFilters.Keyword)}" +
            //   $"&searchfields={Uri.EscapeDataString(eventFilters.SearchFields)}" +
            //   $"&Divisions={Uri.EscapeDataString(string.Join(",", eventFilters.Divisions))}" +
            //   $"&Disciplines={Uri.EscapeDataString(string.Join(",", eventFilters.Disciplines))}" +
            //   $"&TagElements={Uri.EscapeDataString(string.Join(",", eventFilters.TagElements))}" +
            //   $"&ContactPersons={Uri.EscapeDataString(string.Join(",", eventFilters.ContactPersons))}";
            var rows = await _httpClient.GetFromJsonAsync<List<AssociationEntities.CustomModels.EventModel>>("api/Event/GetAllEvents?");// + query.ToString());
            return rows;
        }
    }
}

