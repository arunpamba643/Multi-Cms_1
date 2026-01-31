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
    public class CopyFilesHandlerRequest : IRequest<object>
    {
        public string Path { get; set; }
        public string TargetPath { get; set; }
        public string[] Names { get; set; }
        public string[] RenameFiles { get; set; }
        public FileManagerDirectoryContent TargetData { get; set; }
        public FileManagerDirectoryContent[] Data { get; set; }
    }
    public class CopyFilesHandler : IRequestHandler<CopyFilesHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public CopyFilesHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(CopyFilesHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(_fileManager.Copy(request.Path, request.TargetPath, request.Names, request.RenameFiles, request.TargetData, request.Data));
        }
    }
}
