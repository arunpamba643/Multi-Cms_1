using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class CreateContainerRequest : IRequest<int>
    {

        public int? RowId { get; set; }

        public string? ContainerName { get; set; }

        public int NoofContainers { get; set; }

        public DateTime? CreatedOn { get; set; }


    }
    public class CreateContainerHandle : IRequestHandler<CreateContainerRequest, int>
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IMapper _mapper;
        public CreateContainerHandle(IContainerRepository containerRepository, IMapper mapper)
        {
            _containerRepository = containerRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateContainerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var container = _mapper.Map<Container>(request);
                return await _containerRepository.CreateContainer(container);
            }
            catch
            {
                throw;
            }
        }
    }
   
}
