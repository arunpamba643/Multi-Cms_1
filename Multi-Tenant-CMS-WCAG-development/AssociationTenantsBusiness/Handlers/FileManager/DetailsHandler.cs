using AssociationRepository.Association;
using MediatR;
using Syncfusion.Blazor.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers.FileManager
{
    public class DetailsHandlerRequest : IRequest<object>
    {
        public string path { get; set; }
        public string[] names { get; set; }
        public FileManagerDirectoryContent[] data { get; set; }
    }
    public class DetailsHandler : IRequestHandler<DetailsHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public DetailsHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(DetailsHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(_fileManager.Details(request.path, request.names, request.data));
        }
    }
}
