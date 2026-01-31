using Demo_App.Models;
using AssociationEntities.CustomModels;

namespace Demo_App.Services.Events
{
    public interface IEventService
    {
        Task<List<Models.EventModel>> GetAllEvents();
        Task<List<AssociationEntities.CustomModels.EventModel>> GetAllEventsFromJSON(EventFilters eventFilters);
    }
}
