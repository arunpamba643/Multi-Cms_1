using AssociationRepository.Association;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{
    public class DeleteRowByIdRequest : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteRowByIdHandler : IRequestHandler<DeleteRowByIdRequest>
    {
        private readonly IRowRepository _rowRepository;
        private readonly IComponentsRepository _componentsRepository;
        private readonly IContainerRepository _containerRepository;

        public DeleteRowByIdHandler(IRowRepository rowRepository, IComponentsRepository componentsRepository, IContainerRepository containerRepository)
        {
            _rowRepository = rowRepository;
            _componentsRepository = componentsRepository;
            _containerRepository = containerRepository;
        }

        public async Task Handle(DeleteRowByIdRequest request, CancellationToken cancellationToken)
        {
            await _rowRepository.DeleteRowById(request.Id);
        }


    }
}
