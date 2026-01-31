using AssociationEntities.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationRepository.Association
{
    public interface IEventRespository
    {
        Task<List<EventModel>> GetAllEvents(EventFilters EventFilters);
    }
}
