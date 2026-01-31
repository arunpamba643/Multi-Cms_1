using AssociationRepository.Association;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class DeleteComponentByComponentIdRequest : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteComponentByComponentIdHander : IRequestHandler<DeleteComponentByComponentIdRequest>

    {
        private readonly IComponentsRepository _componentRepository;

        public DeleteComponentByComponentIdHander(IComponentsRepository componentRepository)
        {
            _componentRepository = componentRepository;
        }

        public async Task Handle(DeleteComponentByComponentIdRequest request, CancellationToken cancellationToken)
        {
            _componentRepository.DeleteComponentByComponentId(request.Id);
        }
    }
}
