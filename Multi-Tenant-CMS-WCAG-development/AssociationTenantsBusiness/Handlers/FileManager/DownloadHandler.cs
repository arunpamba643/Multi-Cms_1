using AssociationRepository.Association;
using MediatR;
using Syncfusion.Blazor.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{
    public class DownloadHandlerRequest() : IRequest<object>
    {
        public string path { get; set; }
        public string[] names { get; set; }
        public FileManagerDirectoryContent[] data { get; set; }
    }

    public class DownloadHandler : IRequestHandler<DownloadHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public DownloadHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(DownloadHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(_fileManager.Download(request.path, request.names, request.data));
        }
    }
}
