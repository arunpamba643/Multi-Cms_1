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
    public class CreateFilesHandlerRequest : IRequest<object>
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public FileManagerDirectoryContent[] Data { get; set; }
    }
    public class CreateFilesHandler : IRequestHandler<CreateFilesHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public CreateFilesHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(CreateFilesHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(_fileManager.Create(request.Path, request.Name, request.Data));
        }
    }
}
