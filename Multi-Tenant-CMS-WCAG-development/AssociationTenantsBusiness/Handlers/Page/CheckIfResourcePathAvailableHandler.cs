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
    public class CheckIfResourcePathAvailableRequest : IRequest<bool>
    {
        public string? ResourcePath { get; set; }
        public int TenantId { get; set; }
        public int PageId { get; set; }
    }
    public class CheckIfResourcePathAvailableHandler : IRequestHandler<CheckIfResourcePathAvailableRequest, bool>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;
        public CheckIfResourcePathAvailableHandler(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public Task<bool> Handle(CheckIfResourcePathAvailableRequest request, CancellationToken cancellationToken)
        {
            var data = _pageRepository.CheckIfResourcePathAvailable(request.ResourcePath, request.TenantId, request.PageId);
            return Task.FromResult(data);
        }
    }
}
