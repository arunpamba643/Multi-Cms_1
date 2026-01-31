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
    public class GetFilesHandlerRequest() : IRequest<object>
    {
        public string path { get; set; }
        public bool showHiddenItems { get; set; }
        public FileManagerDirectoryContent[] data { get; set; }
    }
    public class GetFilesHandler : IRequestHandler<GetFilesHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public GetFilesHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(GetFilesHandlerRequest request, CancellationToken cancellationToken)
        {

            return Task.FromResult<object>(_fileManager.GetFiles(request.path, request.showHiddenItems, request.data));
        }
    }
}
