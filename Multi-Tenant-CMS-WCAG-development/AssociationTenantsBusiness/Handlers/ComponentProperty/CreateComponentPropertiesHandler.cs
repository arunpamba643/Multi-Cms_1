using AssociationEntities;
using AssociationEntities.Models;
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
    public class CreateComponentPropertiesRequest : IRequest<int>
    {
        public List<CreateComponentPropertyRequest> PropertyList { get; set; }
        
    }
    public class CreateComponentPropertiesHandler : IRequestHandler<CreateComponentPropertiesRequest, int>
    {
        private readonly IComponentPropertiesRespository _componentProperty;
        private readonly IMapper _mapper;
        public CreateComponentPropertiesHandler(IComponentPropertiesRespository componentProperty, IMapper mapper)
        {
            _componentProperty = componentProperty;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateComponentPropertiesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var componentProperty = _mapper.Map<ComponentProperitiesRequest>(request);
                await _componentProperty.CreateComponentProperties(componentProperty);
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
