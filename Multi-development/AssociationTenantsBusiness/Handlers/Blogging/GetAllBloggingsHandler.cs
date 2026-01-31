using AssociationEntities.Models;
using AssociationRepository.Association;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Get All Blogs request
    /// </summary>
    public class GetAllBlogRequest : IRequest<List<Blog>>
    {

    }

    /// <summary>
    /// Get All Blog Handler
    /// </summary>
    public class GetAllBloggingsHandler : IRequestHandler<GetAllBlogRequest, List<Blog>>
    {
        private readonly IBlogRepository _blogRepository;
        public GetAllBloggingsHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        Task<List<Blog>> IRequestHandler<GetAllBlogRequest, List<Blog>>.Handle(GetAllBlogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _blogRepository.GetAllBlogs();
                return Task.FromResult(data);
            }
            catch
            {
                throw;
            }
        }
    }
}
