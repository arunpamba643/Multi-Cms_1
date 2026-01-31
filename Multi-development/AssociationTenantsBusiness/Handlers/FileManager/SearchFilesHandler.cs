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
    public class SearchFilesHandlerRequest : IRequest<object>
    {
        public string Path { get; set; }
        public string SearchString { get; set; }
        public bool ShowHiddenItems { get; set; }
        public bool CaseSensitive { get; set; }
        public FileManagerDirectoryContent[] Data { get; set; }
    }
    public class SearchFilesHandler : IRequestHandler<SearchFilesHandlerRequest, object>
    {
        private readonly IFileManager _fileManager;
        public SearchFilesHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public Task<object> Handle(SearchFilesHandlerRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(_fileManager.Search(request.Path, request.SearchString, request.ShowHiddenItems, request.CaseSensitive, request.Data));
        }
    }
}
