using AssociationEntities.Models;
using AssociationEntities.Models.Api;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Create Custom Menu Request
    /// </summary>
    public class BulkUpdateMenuRequest : IRequest<int>
    {
        public List<CustomMenuModel> Menus { get; set; }
    }

    /// <summary>
    /// Create Custom Menu Handler
    /// </summary>
    public class BulkUpdateMenuHandler : IRequestHandler<BulkUpdateMenuRequest, int>
    {
        private readonly ICustomMenuRepository _customMenuRepository;
        private readonly IMapper _mapper;
        public BulkUpdateMenuHandler(ICustomMenuRepository customMenuRepository, IMapper mapper)
        {
            _customMenuRepository = customMenuRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(BulkUpdateMenuRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customMenu = _mapper.Map<List<CustomMenu>>(request.Menus);

                if (customMenu.Count >= 0)
                    return await _customMenuRepository.BulkUpdateMenu(customMenu);
                else
                    return 1;
            }
            catch
            {
                throw;
            }

        }
    }
}
