using AssociationEntities.CustomModels;
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

    public class UpdateBlogPageRequest : IRequest
    {
        public int BpId { get; set; } = 0;
        public int? TenantId { get; set; }
        public string Title { get; set; } = null!;
        public ScopeType? ScopeType { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
        public string? Division { get; set; }
        public string? Discipline { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? ValidFrom { get; set; }
        public string? HeaderImage { get; set; }
        public List<BlogAttribute> blogAttribute { get; set; }

    }
    /// <summary>
    /// Handler for updating a blog page.
    /// </summary>
    public class UpdateBlogPageHandler : IRequestHandler<UpdateBlogPageRequest>
    {
        private readonly IBlogPagesRepository _blogPagesRepository;
        private readonly IMapper _mapper;
        public UpdateBlogPageHandler(IBlogPagesRepository blogPagesRepository, IMapper mapper)
        {
            _blogPagesRepository = blogPagesRepository;
            _mapper = mapper;
        }
        public async Task Handle(UpdateBlogPageRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _mapper.Map<BlogPost>(request);
            await _blogPagesRepository.UpdateBlogPage(blogPost);

        }
    }
}
