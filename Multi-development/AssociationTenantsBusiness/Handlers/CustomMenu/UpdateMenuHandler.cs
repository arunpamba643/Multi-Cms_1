using AssociationEntities.CustomModels.Models;
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
    public class updateMenuRequest : IRequest<int>
    {
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public int? ParentMenuId { get; set; }
        public string? PageUrl { get; set; }
        public int? Position { get; set; }

        public int PageId { get; set; }
    }
    public class UpdateMenuHandler : IRequestHandler<updateMenuRequest, int>
    {
        private readonly ICustomMenuRepository _customMenuRepository;
        private readonly IMapper _mapper;
        public UpdateMenuHandler(ICustomMenuRepository customMenuRepository, IMapper mapper)
        {
            _customMenuRepository = customMenuRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(updateMenuRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customMenu = _mapper.Map<CustomMenu>(request);
                return await _customMenuRepository.UpdateMenu(customMenu, request.PageId);
            }
            catch
            {
                throw;
            }
        }
    }
}
