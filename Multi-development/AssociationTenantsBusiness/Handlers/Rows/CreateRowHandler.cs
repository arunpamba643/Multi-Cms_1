using AssociationEntities.Models;
using AssociationRepository.Association;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;


namespace AssociationBusiness.Handlers
{
    public class CreateRowRequest : IRequest<int>
    {

        public int? PageId { get; set; }

        public int? OrderNumber { get; set; }

        public DateTime? CreatedOn { get; set; }

    }
    public class CreateRowHandler : IRequestHandler<CreateRowRequest, int>
    {
        private readonly IRowRepository _rowRepository;
        private readonly IMapper _mapper;

        public CreateRowHandler(IRowRepository rowRepository, IMapper mapper)
        {
            _rowRepository = rowRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateRowRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var row = _mapper.Map<Row>(request);
                return await _rowRepository.CreateRow(row);
            }
            catch
            {
                throw;
            }
        }

    }
}
