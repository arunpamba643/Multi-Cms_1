using AssociationRepository.Association;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{
    public class DeleteComponentPropertiesRequest : IRequest
    {
        public int ComponentId { get; set; }
    }
    public class DeleteComponentPropertiesByComponentId : IRequestHandler<DeleteComponentPropertiesRequest>
    {
        private readonly IComponentPropertiesRespository _componentPropertiesRespository;

        public DeleteComponentPropertiesByComponentId(IComponentPropertiesRespository componentPropertiesRespository)
        {
            _componentPropertiesRespository = componentPropertiesRespository;
        }

        public async Task Handle(DeleteComponentPropertiesRequest request, CancellationToken cancellationToken)
        {
           await _componentPropertiesRespository.DeleteComponentProperitesByComponentId(request.ComponentId);
        }
    }
}
