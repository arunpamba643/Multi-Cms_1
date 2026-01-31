using AssociationRepository.Association;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class DeleteMenuById : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteMenuByIdHandler : IRequestHandler<DeleteMenuById>
    {
        private readonly ICustomMenuRepository _customMenuRepository;
        public DeleteMenuByIdHandler(ICustomMenuRepository customMenuRepository)
        {
            _customMenuRepository = customMenuRepository;
        }

        public async Task Handle(DeleteMenuById request, CancellationToken cancellationToken)
        {
            _customMenuRepository.DeleteMenuById(request.Id);
        }

    }
}
