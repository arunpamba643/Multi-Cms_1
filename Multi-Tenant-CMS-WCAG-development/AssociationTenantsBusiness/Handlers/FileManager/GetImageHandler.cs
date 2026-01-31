using AssociationEntities;
using AssociationRepository.Association;
using MediatR;
using Syncfusion.Blazor.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace AssociationBusiness.Handlers
{
    public class GetImageHandlerRequest : IRequest<FileStreamResult>
    {
        public string Path { get; set; }
        public string ParentId { get; set; }
        public string Id { get; set; }
        public bool AllowCompress { get; set; }
        public ImageSize Size { get; set; }
        public FileManagerDirectoryContent[] Data { get; set; }

    }
    public class GetImageHandler : IRequestHandler<GetImageHandlerRequest, FileStreamResult>
    {
        private readonly IFileManager _fileManager;
        public GetImageHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<FileStreamResult> Handle(GetImageHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<FileStreamResult>(_fileManager.GetImage(request.Path, request.ParentId, request.Id, request.AllowCompress, request.Size, request.Data));


        }
    }

}
