using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Create Custom Menu Request
    /// </summary>
    public class CreateMenuRequest : IRequest<int>
    {
        public int? MenuId { get; set; } = 0;
        public string MenuName { get; set; }
        public int? ParentMenuId { get; set; } = 0;
        public string? PageUrl { get; set; }
        public int Position { get; set; }
        public int TenantId { get; set; }
        public int PageId { get; set; }
    }

    /// <summary>
    /// Create Custom Menu Handler
    /// </summary>
    public class CreateMenuHandler : IRequestHandler<CreateMenuRequest, int>
    {
        private readonly ICustomMenuRepository _customMenuRepository;
        private readonly IMapper _mapper;
        public CreateMenuHandler(ICustomMenuRepository customMenuRepository, IMapper mapper)
        {
            _customMenuRepository = customMenuRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateMenuRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customMenu = _mapper.Map<CustomMenu>(request);

                if (customMenu.MenuId != 0)
                    return await _customMenuRepository.UpdateMenu(customMenu, request.PageId);
                else
                    return await _customMenuRepository.CreateMenu(customMenu, request.PageId);
            }
            catch
            {
                throw;
            }

        }
    }
}
