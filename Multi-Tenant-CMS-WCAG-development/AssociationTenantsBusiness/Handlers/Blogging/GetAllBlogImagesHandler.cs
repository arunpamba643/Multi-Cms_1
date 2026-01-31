using AssociationEntities.Models;
using AssociationRepository.Association;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Get All Blog Images request
    /// </summary>
    public class GetAllBlogImagesRequest : IRequest<List<BlogImage>>
    {
        public int BlogId { get; set; }

        public int TenantId { get; set; }

    }

    /// <summary>
    /// Get All Blog Image Handler
    /// </summary>
    public class GetAllBlogImagesHandler : IRequestHandler<GetAllBlogImagesRequest, List<BlogImage>>
    {
        private readonly IBlogRepository _blogRepository;
        public GetAllBlogImagesHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        Task<List<BlogImage>> IRequestHandler<GetAllBlogImagesRequest, List<BlogImage>>.Handle(GetAllBlogImagesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _blogRepository.GetAllBlogImages(request.BlogId,request.TenantId  );
                return Task.FromResult(data);
            }
            catch
            {
                throw;
            }
        }
    }
}
