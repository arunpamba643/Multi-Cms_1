using AssociationEntities;
using AssociationRepository.Association;
using MediatR;
using Microsoft.AspNetCore.Http;
using Syncfusion.Blazor.FileManager;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssociationBusiness.Handlers
{
    public class UploadOperationHandlerRequest : IRequest<FileManagerResponse>
    {
        public string Path { get; set; }
        public IList<IFormFile> UploadFiles { get; set; }
        public string Action { get; set; }
        public FileManagerDirectoryContent[] Data { get; set; }
        public long Size { get; set; } = 0;
        public int ChunkIndex { get; set; } = 0;
        public int TotalChunk { get; set; } = 0;
    }
    public class UploadOperationHandler : IRequestHandler<UploadOperationHandlerRequest, FileManagerResponse>
    {
        private readonly IFileManager _fileManager;
        public UploadOperationHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<FileManagerResponse> Handle(UploadOperationHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<FileManagerResponse>(_fileManager.Upload(request.Path, request.UploadFiles, request.Action, request.Data, request.Size = 0, request.ChunkIndex = 0, request.TotalChunk = 0));
        }
    }
}
