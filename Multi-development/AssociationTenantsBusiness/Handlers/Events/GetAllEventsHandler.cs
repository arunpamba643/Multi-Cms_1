using AssociationEntities.CustomModels;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{
    public class GetEventsRequest : IRequest<IEnumerable<EventModel>>
    {
        public DateTime? fromDate { get; set; }
        public ScopeType? ScopeType { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public List<string>? SearchFields { get; set; }
        public string? Keyword { get; set; }
        public List<string>? DivisionsIds { get; set; } = new List<string>();
        public List<string>? DisciplinesIds { get; set; } = new List<string>();
        public List<string>? TagIds { get; set; } = new List<string>();
        public List<string>? CreatorIds { get; set; } = new List<string>();
    }
    public class GetAllEventsHandler : IRequestHandler<GetEventsRequest, IEnumerable<EventModel>>
    {
        private readonly IEventRespository _eventRepository;
        private readonly IMapper _mapper;
        public GetAllEventsHandler(IEventRespository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EventModel>> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
            var eventFilters = _mapper.Map<EventFilters>(request);
            var data = await _eventRepository.GetAllEvents(eventFilters);
            return data;
        }
    }
}
