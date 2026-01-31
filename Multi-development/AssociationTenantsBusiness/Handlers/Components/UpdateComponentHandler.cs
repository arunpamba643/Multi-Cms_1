using AssociationEntities.CustomModels;
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
    public class UpdateComponentRequest : IRequest
    {
        public int ComponentId { get; set; }
        public string? ComponentType { get; set; }
        public DateTime UpdateOn { get; set; }
    }
    public class UpdateComponentHandler : IRequestHandler<UpdateComponentRequest>
    {
        private readonly IComponentsRepository _componentsRepository;
        private readonly IMapper _mapper;

        public UpdateComponentHandler(IComponentsRepository componentsRepository, IMapper mapper)
        {
            _componentsRepository = componentsRepository;
            _mapper = mapper;

        }
        public async Task Handle(UpdateComponentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var component = _mapper.Map<UpdateComponent>(request);
                await _componentsRepository.UpdateComponent(component);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
