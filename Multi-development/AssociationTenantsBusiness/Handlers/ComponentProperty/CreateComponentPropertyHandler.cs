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
    public class CreateComponentPropertyRequest : IRequest
    {
        //public CreateComponentPropertyRequest(int ComponentId, string Key, string Value)
        //{
        //    this.ComponentId = ComponentId;
        //    this.Key = Key; 
        //    this.Value = Value;
        //}
        public int ComponentId { get; set; }
        public string? Key { get; set; } = "";
        public string? Value { get; set; } = "";
    }

    public class CreateComponentPropertyHandler : IRequestHandler<CreateComponentPropertyRequest>
    {
        private readonly IComponentPropertiesRespository _componentProperty;
        private readonly IMapper _mapper;

        public CreateComponentPropertyHandler(IComponentPropertiesRespository componentProperty, IMapper mapper)
        {
            _componentProperty = componentProperty;
            _mapper = mapper;
        }
        public async Task Handle(CreateComponentPropertyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var componentProperty = _mapper.Map<ComponentProperty>(request);
                await _componentProperty.CreateComponentProperty(componentProperty);
                //return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
