using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class GetPageById : IRequest<PageModel>
    {
        public int Id { get; set; } 
    }
    public class GetPageByIdHandler : IRequestHandler<GetPageById, PageModel>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;
        public GetPageByIdHandler(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public Task<PageModel> Handle(GetPageById request, CancellationToken cancellationToken)
        {
            var data = _pageRepository.GetPageById(request.Id);
            var page = _mapper.Map<PageModel>(data);
            return Task.FromResult(page);
        }

    }
}
