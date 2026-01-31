using AssociationEntities.CustomModels;
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
    public class GetBlogFilterSuggestions : IRequest<List<BlogFilterSuggestions>>
    {
        public int TenantId { get; set; }
    }
    public class GetBlogFilterSuggestionsHandler : IRequestHandler<GetBlogFilterSuggestions, List<BlogFilterSuggestions>>
    {
        private readonly IBlogPagesRepository _blogPagesRepository;
        private readonly IMapper _mapper;
        public GetBlogFilterSuggestionsHandler(IBlogPagesRepository blogPagesRepository, IMapper mapper)
        {
            _blogPagesRepository = blogPagesRepository;
            _mapper = mapper;
        }

        public async Task<List<BlogFilterSuggestions>> Handle(GetBlogFilterSuggestions request, CancellationToken cancellationToken)
        {
            return await _blogPagesRepository.GetBlogFilterSuggestions(request.TenantId);
        }
    }
}
