using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{

    public class GetBlogPagesRequest : IRequest<List<BlogPost>>
    {
        [Required]
        public int TenantId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? SearchFields { get; set; }
        public string? Keyword { get; set; }
        public string? DivisionsIds { get; set; }
        public string? DisciplinesIds { get; set; }
        public string? TagIds { get; set; }
        public string? CreatorIds { get; set; }
    }
    /// <summary>
    /// Handler for retrieving a list of blog pages based on the provided request.
    /// </summary>
    public class GetAllBlogPagesHandler : IRequestHandler<GetBlogPagesRequest, List<BlogPost>>
    {
        private readonly IBlogPagesRepository _blogPagesRepository;
        private readonly IMapper _mapper;
        public GetAllBlogPagesHandler(IBlogPagesRepository blogPagesRepository, IMapper mapper)
        {
            _blogPagesRepository = blogPagesRepository;
            _mapper = mapper;
        }

        public async Task<List<BlogPost>> Handle(GetBlogPagesRequest request, CancellationToken cancellationToken)
        {
            var blogFilters = _mapper.Map<BlogFilters>(request);
            var data = await _blogPagesRepository.GetAllBlogPages(blogFilters);
            //var customBlogPlages = _mapper.Map<List<BlogPagesModel>>(data);
            return data;
        }
    }
}
