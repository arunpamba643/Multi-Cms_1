using AssociationRepository.Association;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class DeletePageById : IRequest
    {
        public int Id { get; set; }
    }
    public class DeletePageByIdHandler : IRequestHandler<DeletePageById>
    {
        private readonly IPageRepository _pageRepository;
        public DeletePageByIdHandler(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task Handle(DeletePageById request, CancellationToken cancellationToken)
        {
            await _pageRepository.DeletePageById(request.Id);
        }

    }
}
