using WebsiteDemo.Models;
using AssociationEntities.CustomModels;

namespace WebsiteDemo.Services.Events
{
    public interface IEventService
    {
        Task<List<Models.EventModel>> GetAllEvents();
        Task<List<AssociationEntities.CustomModels.EventModel>> GetAllEventsFromJSON(EventFilters eventFilters);
    }
}
