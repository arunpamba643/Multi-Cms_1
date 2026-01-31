using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;


namespace AssociationBusiness.Handlers
{
    public class CreateComponentRequest : IRequest<int>
    {

        
        public int? ContainerId { get; set; }

        public string? ComponentName { get; set; }

        public string? ComponentType { get; set; }

        public string? Width { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? OrderNumber { get; set; }

        public string? RefValue { get; set; }
        public string? RefDesc { get; set; }

    }

    public class CreateComponentHandler : IRequestHandler<CreateComponentRequest, int>
    {
        private readonly IComponentsRepository _componentRepository;
        private readonly IMapper _mapper;

        public CreateComponentHandler(IComponentsRepository componentRepository, IMapper mapper)
        {
            _componentRepository = componentRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateComponentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var component = _mapper.Map<Component>(request);
                return await _componentRepository.CreateComponent(component);
            }
            catch
            {
                throw;
            }
        }

      
    }
}
