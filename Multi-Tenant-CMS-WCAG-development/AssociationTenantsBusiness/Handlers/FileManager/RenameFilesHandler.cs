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
    public class RenameFilesHandlerRequest : IRequest<object>
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string NewName { get; set; }
        public bool Replace { get; set; }
        public FileManagerDirectoryContent[] Data { get; set; }
    }
    public class RenameFilesHandler : IRequestHandler<RenameFilesHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public RenameFilesHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(RenameFilesHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(_fileManager.Rename(request.Path, request.Name, request.NewName, request.Replace, request.Data));
        }
    }
}
