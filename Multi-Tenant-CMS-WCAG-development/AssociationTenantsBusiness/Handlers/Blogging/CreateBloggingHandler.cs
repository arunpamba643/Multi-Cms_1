using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Create Blogging Request
    /// </summary>
    public class CreateBloggingRequest : IRequest<int>
    {
        public int BlogId { get; set; }
        public int TenantId { get; set; }

        public string? Htmltemplate { get; set; }

        public string? DynamicHtmltemplate { get; set; }

        public string? HeaderBackgroundColor { get; set; }

        public string? HeaderFontColor { get; set; }

        public string? BrandName { get; set; }

        public string? Logo { get; set; }

        public string? Banner { get; set; }

        public string? Heading { get; set; }

        public string? TextContent { get; set; }

        public string? ContentFontSize { get; set; }

        public string? ContentFontColor { get; set; }

        public string? FooterBackgroundColor { get; set; }

        public string? HeaderFontStyle { get; set; }

        public string? ContentFontStyle { get; set; }

        public string? FooterFontStyle { get; set; }

        public string? FooterText { get; set; }

        public string? MainMenuColor { get; set; }

        public string? MainMenuFontSize { get; set; }

        public string? MainMenuFontStyle { get; set; }

        public string? SubMenuColor { get; set; }

        public string? SubMenuFontSize { get; set; }

        public string? SubMenuFontStyle { get; set; }

        public string? SubChildMenuColor { get; set; }

        public string? SubChildMenuFontSize { get; set; }

        public string? SubChildFontStyle { get; set; }

        public string? FooterFontColor { get; set; }

        public string? Facebook { get; set; }

        public string? LinkedIn { get; set; }

        public string? Twitter { get; set; }

        public string? Instagram { get; set; }

        public string? WhatsApp { get; set; }
    }

    /// <summary>
    /// Blog Create Handler
    /// </summary>
    public class CreateBloggingHandler : IRequestHandler<CreateBloggingRequest, int>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public CreateBloggingHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateBloggingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var isNewBlog = _blogRepository.CheckTenantBlogExists(request.TenantId, request.BlogId).Result;

                var blog = _mapper.Map<Blog>(request);
                if (blog != null && blog.BlogId != 0 && isNewBlog)
                    return await _blogRepository.UpdateBlog(blog);
                else
                {
                    blog.BlogId = 0;
                    return await _blogRepository.CreateBlog(blog);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
