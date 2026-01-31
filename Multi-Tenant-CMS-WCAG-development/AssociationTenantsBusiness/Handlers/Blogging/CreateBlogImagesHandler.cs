using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Create Blogging Request
    /// </summary>
    public class CreateBlogImageRequest : IRequest<int>
    {
        public int ImageId { get; set; }

        public int BlogId { get; set; }

        public int TenantId { get; set; }

        public string? ImageUrl { get; set; }

        public string? Text { get; set; }

        public int Position { get; set; }

        public string? Udf { get; set; }
    }

    /// <summary>
    /// Blog Image Handler
    /// </summary>
    public class CreateBlogImagesHandler : IRequestHandler<CreateBlogImageRequest, int>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public CreateBlogImagesHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateBlogImageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var blogImage = _mapper.Map<BlogImage>(request);
                if (blogImage != null && blogImage.ImageId != 0)
                    return await _blogRepository.UpdateBlogImage(blogImage);
                else
                    return await _blogRepository.CreateBlogImage(blogImage);
            }
            catch
            {
                throw;
            }
        }
    }
}
