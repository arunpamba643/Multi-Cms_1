using AssociationRepository.Association;
using AutoMapper;
using MediatR;


namespace AssociationBusiness.Handlers
{
    public class DeleteContainerByRowIdRequest : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteContainerByRowIdHandle : IRequest<DeleteContainerByRowIdRequest>
    {
        private readonly IContainerRepository _containerRepository;
         private readonly IMapper _mapper;
        public DeleteContainerByRowIdHandle(IContainerRepository containerRepository,  IMapper mapper)
        {
            _containerRepository = containerRepository;
            _mapper = mapper;
             

        }

        public async Task Handle(DeleteContainerByRowIdRequest request, CancellationToken cancellationToken)
        {
            _containerRepository.DeleteContainerByRowId(request.Id);
        }
    }
}
