using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Create Custom Page Request
    /// </summary>
    public class CreatePageRequest : IRequest<int>
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public bool? IsHomePage { get; set; }
        [Required]
        public string PageTitle { get; set; } = null!;
        [Required]
        public string? ResourcePath { get; set; }
        public bool? IsLandingPage { get; set; }
        public int MenuId { get; set; }
        public string? PageScopeType { get; set; }
        public int? PaddingWidth { get; set; }
        public string? pageUrl { get; set; }
    }

    /// <summary>
    /// Create Custom Page Handler
    /// </summary>
    public class CreatePageHandler : IRequestHandler<CreatePageRequest, int>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;

        public CreatePageHandler(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var page = _mapper.Map<Page>(request);

                if (page.Id != 0)
                    return await _pageRepository.UpdatePage(page, request.pageUrl);
                else
                    return await _pageRepository.CreatePage(page, request.pageUrl);
            }
            catch
            {
                throw;
            }
        }
    }
}
