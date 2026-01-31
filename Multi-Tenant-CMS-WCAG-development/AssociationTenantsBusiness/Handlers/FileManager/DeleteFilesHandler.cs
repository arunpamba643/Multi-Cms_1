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
    public class DeleteFilesHandlerRequest : IRequest<object>
    {
        public string Path { get; set; }
        public string[] Names { get; set; }
        public FileManagerDirectoryContent[] Data { get; set; }

    }
    public class DeleteFilesHandler : IRequestHandler<DeleteFilesHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public DeleteFilesHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(DeleteFilesHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(_fileManager.Delete(request.Path, request.Names, request.Data));
        }
    }

}
