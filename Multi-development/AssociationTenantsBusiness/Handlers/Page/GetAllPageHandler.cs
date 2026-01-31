using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class GetAllPageRequest : IRequest<List<PageModel>>
    {
        public int TenantId { get; set; }
    }
    public class GetAllPageHandler : IRequestHandler<GetAllPageRequest, List<PageModel>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;
        public GetAllPageHandler(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public Task<List<PageModel>> Handle(GetAllPageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
       
        Task<List<PageModel>> IRequestHandler<GetAllPageRequest, List<PageModel>>.Handle(GetAllPageRequest request, CancellationToken cancellationToken)
        {
            var data = _pageRepository.GetAlPages(request.TenantId);
            var customMenus = _mapper.Map<List<PageModel>>(data);
            return Task.FromResult(customMenus);
        }
      
    }
}
