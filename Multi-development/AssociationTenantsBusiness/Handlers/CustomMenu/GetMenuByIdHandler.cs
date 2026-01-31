using AssociationEntities.Models.Api;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class GetMenuById : IRequest<CustomMenuModel>
    {
        public int Id { get; set; } 
    }
    public class GetMenuByIdHandler : IRequestHandler<GetMenuById, CustomMenuModel>
    {
        private readonly ICustomMenuRepository _customMenuRepository;
        private readonly IMapper _mapper;
        public GetMenuByIdHandler(ICustomMenuRepository customMenuRepository, IMapper mapper)
        {
            _customMenuRepository = customMenuRepository;
            _mapper = mapper;
        }

        public Task<CustomMenuModel> Handle(GetMenuById request, CancellationToken cancellationToken)
        {
            var data = _customMenuRepository.GetMenuById(request.Id);
            var customMenu = _mapper.Map<CustomMenuModel>(data);
            return Task.FromResult(customMenu);
        }

    }
}
