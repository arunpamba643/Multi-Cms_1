using AssociationRepository.Association;
using MediatR;

namespace AssociationBusiness.Handlers
{
    public class DeleteComponentByContainerIdRequest : IRequest
    {
        public int Id { get; set; }
    }


    public class DeleteComponentByContainerIdHandler : IRequestHandler<DeleteComponentByContainerIdRequest>
    {
        private readonly IComponentsRepository _componentsRepository;
        public DeleteComponentByContainerIdHandler(IComponentsRepository componentsRepository)
        {
            _componentsRepository = componentsRepository;
        }

        public async Task Handle(DeleteComponentByContainerIdRequest request, CancellationToken cancellationToken)
        {
            _componentsRepository.DeleteComponentByContainerId(request.Id);
        }
    }
}
