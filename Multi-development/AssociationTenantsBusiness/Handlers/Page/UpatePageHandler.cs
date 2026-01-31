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
    public class UpatePageRequest : IRequest<int>
    {
        public int Id { get; set; }
        public int menuId { get; set; }
        public string PageTitle { get; set; } = null!;
        public string? ResourcePath { get; set; }
        public int? PaddingWidth { get; set; }
        public bool? IsHomePage { get; set; }
        public bool? IsLandingPage { get; set; }

    }
    public class UpdatePageRequestHandler : IRequestHandler<UpatePageRequest, int>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;

        public UpdatePageRequestHandler(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpatePageRequest request, CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Page>(request);
            return await _pageRepository.UpdatePage(menu, "");
        }
    }
}


