using AssociationEntities.Models.Api;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class GetAllMenuRequest : IRequest<List<CustomMenuModel>>
    {
        public int TenantId { get; set; }
    }
    public class GetAllMenuHandler : IRequestHandler<GetAllMenuRequest, List<CustomMenuModel>>
    {
        private readonly ICustomMenuRepository _customMenuRepository;
        private readonly IMapper _mapper;
        public GetAllMenuHandler(ICustomMenuRepository customMenuRepository, IMapper mapper)
        {
            _customMenuRepository = customMenuRepository;
            _mapper = mapper;
        }
        Task<List<CustomMenuModel>> IRequestHandler<GetAllMenuRequest, List<CustomMenuModel>>.Handle(GetAllMenuRequest request, CancellationToken cancellationToken)
        {
            var data = _customMenuRepository.GetAllMenus(request.TenantId);
            var customMenus = _mapper.Map<List<CustomMenuModel>>(data);
            return Task.FromResult(customMenus);
        }
    }
}
