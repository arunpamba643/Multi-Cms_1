using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class GetComponentByContainerIdRequest : IRequest<List<Component>>
    {
        public int Id { get; set; }
    }
    public class GetComponentByContainerIdHandler : IRequestHandler<GetComponentByContainerIdRequest, List<Component>>
    {
        private readonly IComponentsRepository _componentsRepository;
        private readonly IMapper _mapper;

        public GetComponentByContainerIdHandler(IComponentsRepository componentsRepository, IMapper mapper)
        {
            _componentsRepository = componentsRepository;
            _mapper = mapper;

        }

  

        public Task<List<Component>> Handle(GetComponentByContainerIdRequest request, CancellationToken cancellationToken)
        {
            var data = _componentsRepository.GetComponentByContainerId(request.Id);
            var components = _mapper.Map<List<Component>>(data);
            return Task.FromResult(components);
        }
    }
}
