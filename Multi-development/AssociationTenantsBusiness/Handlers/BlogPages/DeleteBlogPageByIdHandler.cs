using AssociationRepository.Association;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{

    public class DeleteBlogPageById : IRequest
    {
        public int Id { get; set; }
    }
    /// <summary>
    /// Handler for deleting a blog page by its unique identifier.
    /// </summary>
    public class DeleteBlogPageByIdHandler : IRequestHandler<DeleteBlogPageById>
    {
        private readonly IBlogPagesRepository _blogPagesRepository;
        public DeleteBlogPageByIdHandler(IBlogPagesRepository blogPagesRepository)
        {
            _blogPagesRepository = blogPagesRepository;
        }

        public async Task Handle(DeleteBlogPageById request, CancellationToken cancellationToken)
        {
            _blogPagesRepository.DeleteBlogPageById(request.Id);
        }

    }
}
