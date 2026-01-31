using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;
 

namespace AssociationBusiness.Handlers
{
    public class GetAllContainersByIdRequest : IRequest<List<Container>>
    {
        public int Id { get; set;}
    }
    public class GetAllContainersByIDHandle : IRequestHandler<GetAllContainersByIdRequest, List<Container>>
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IMapper _mapper;
        public GetAllContainersByIDHandle(IContainerRepository containerRepository, IMapper mapper)
        {
            _containerRepository = containerRepository;
            _mapper = mapper;
        }

        public Task<List<Container>> Handle(GetAllContainersByIdRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<List<Container>> IRequestHandler<GetAllContainersByIdRequest, List<Container>>.Handle(GetAllContainersByIdRequest request, CancellationToken cancellationToken)
        {
            var data = _containerRepository.GetAllContainersByID(request.Id);
            var containers = _mapper.Map<List<Container>>(data);
            return Task.FromResult(containers);
        }

    }
}
