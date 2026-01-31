using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class GellAllRowsRequest : IRequest<List<Row>>
    {
        public int PageId { get; set; }
    }
    public class GetAllRowsByPageIdHandler : IRequestHandler<GellAllRowsRequest, List<Row>>
    {

        private readonly IRowRepository _rowRepository;
        private readonly IMapper _mapper;
        public GetAllRowsByPageIdHandler(IRowRepository rowRepository, IMapper mapper)
        {
            _rowRepository = rowRepository;
            _mapper = mapper;
        }

        public Task<List<Row>> Handle(GellAllRowsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

         Task<List<Row>> IRequestHandler<GellAllRowsRequest, List<Row>>.Handle(GellAllRowsRequest request, CancellationToken cancellationToken)
        {
            var data =  _rowRepository.GetAllRowsByPageId(request.PageId);
            var rows =  _mapper.Map<List<Row>>(data);
            return Task.FromResult(rows);
        }


    }
}
