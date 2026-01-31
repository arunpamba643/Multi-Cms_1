using AssociationEntities.CustomModels;
using Demo_App.Models;
using Demo_App.Services.Events;
using Microsoft.AspNetCore.Components;

namespace Demo_App.Components
{
    public partial class EventList : ComponentBase
    {
        //[Parameter]
        public List<AssociationEntities.CustomModels.EventModel> ListItems = new List<AssociationEntities.CustomModels.EventModel>();

        [Parameter]
        public EventFilters EventFilters { get; set; }

        [Inject]
        public IEventService eventService { get; set; }

        public PaginationModel PaginationModel = new PaginationModel();
        public List<AssociationEntities.CustomModels.EventModel> PaginatedList = new List<AssociationEntities.CustomModels.EventModel>();
        private async Task LoadList(int Page)
        {
            ListItems = await eventService.GetAllEventsFromJSON(EventFilters);
            var pageQuery = ListItems.Skip((Page - 1) * PaginationModel.PageSize).Take(PaginationModel.PageSize);
            PaginatedList = pageQuery.ToList();
            PaginationModel.TotalItems = ListItems.Count();
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadList(1);
        }


    }
}
