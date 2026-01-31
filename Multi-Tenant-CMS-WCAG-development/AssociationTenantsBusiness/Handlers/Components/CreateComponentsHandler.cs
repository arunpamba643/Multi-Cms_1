using AssociationRepository.Association;
using AssociationEntities.CustomModels;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{
    public class CreateComponentsRequest : IRequest<int>
    {
        public List<CreateComponentRequest> ComponentList { get; set; }
    }
    public class CreateComponentsHandler : IRequestHandler<CreateComponentsRequest, int>
    {
        private readonly IComponentsRepository _componentsRepository;
        private readonly IMapper _mapper;

        public CreateComponentsHandler(IComponentsRepository componentsRepository, IMapper mapper)
        {
            _componentsRepository = componentsRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateComponentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var components = _mapper.Map<CreateComponents>(request);
                await _componentsRepository.CreateComponents(components);
                return 1;
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
