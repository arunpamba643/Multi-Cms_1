using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using AssociationRepository.Data;
using Microsoft.EntityFrameworkCore;

namespace AssociationRepository.Association
{
    public class EventRepository : IEventRespository
    {


        public async Task<List<EventModel>> GetAllEvents(EventFilters eventFilters)
        {
            var data = EventData.eventList.AsQueryable();

            // Apply filters based on non-null parameters
            if (eventFilters.fromDate != null)
            {
                data = data.Where(e => e.PublishStartTime >= eventFilters.fromDate);
            }
            if (eventFilters.SearchFields != null && eventFilters.SearchFields.Any() && !string.IsNullOrEmpty(eventFilters.Keyword))
            {
                string filterExpression = string.Join(" OR ", eventFilters.SearchFields.Select(field => $"{field}.Contains(@0)"));
                data = data.Where(filterExpression, eventFilters.Keyword);
            }


            if (eventFilters.DivisionsIds != null && eventFilters.DivisionsIds.Any())
            {
                data = data.Where(e => eventFilters.DivisionsIds.Intersect(e.Divisions).Any());
            }

            if (eventFilters.DisciplinesIds != null && eventFilters.DisciplinesIds.Any())
            {
                data = data.Where(e => eventFilters.DisciplinesIds.Intersect(e.Disciplines).Any());
            }
            if (eventFilters.TagIds != null && eventFilters.TagIds.Any())
            {
                data = data.Where(e => eventFilters.TagIds.Intersect(e.TagElements).Any());
            }
            if (eventFilters.CreatorIds != null && eventFilters.CreatorIds.Any())
            {
                data = data.Where(e => eventFilters.CreatorIds.Contains(e.Creator.Id));
            }

            // Apply ordering and pagination
            if (eventFilters.OrderBy != null)
            {
                data = data.OrderBy(eventFilters.OrderBy);

            }
            if (eventFilters.Page != null && eventFilters.PageSize != null)
            {
                data = data.Skip((eventFilters.Page.Value - 1) * eventFilters.PageSize.Value).Take(eventFilters.PageSize.Value);
            }

            return data.ToList();
        }
    }
}
