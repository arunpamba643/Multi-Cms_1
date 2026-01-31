using AssociationRepository.Association;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Delete Blog Image Request
    /// </summary>
    public class DeleteBlogImageById : IRequest
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// Delete Blog Image Handler
    /// </summary>
    public class DeleteBlogImageByIdHandler : IRequestHandler<DeleteBlogImageById>
    {
        private readonly IBlogRepository _blogRepository;
        public DeleteBlogImageByIdHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task Handle(DeleteBlogImageById request, CancellationToken cancellationToken)
        {
            await _blogRepository.DeleteBlogImageById(request.Id);
        }

    }
}
