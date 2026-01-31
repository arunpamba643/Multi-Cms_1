using AssociationEntities.CustomModels;
using WebsiteDemo.Models;
using WebsiteDemo.Services.Events;
using Microsoft.AspNetCore.Components;

namespace WebsiteDemo.Components
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
            //EventList = pageQuery.ToList();
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
