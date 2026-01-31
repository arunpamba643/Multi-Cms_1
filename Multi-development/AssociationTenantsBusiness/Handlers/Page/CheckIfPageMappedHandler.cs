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
    public class CheckIfPageMappedRequest : IRequest<PageModel>
    {
        public int MenuId { get; set; }
        public int pageId { get; set; }
    }
    public class CheckIfPageMappedHandler : IRequestHandler<CheckIfPageMappedRequest, PageModel>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;
        public CheckIfPageMappedHandler(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public Task<PageModel> Handle(CheckIfPageMappedRequest request, CancellationToken cancellationToken)
        {
            var data = _pageRepository.CheckIfPageMapped(request.MenuId, request.pageId);
            var page = _mapper.Map<PageModel>(data);
            return Task.FromResult(page);
        }
    }
}
