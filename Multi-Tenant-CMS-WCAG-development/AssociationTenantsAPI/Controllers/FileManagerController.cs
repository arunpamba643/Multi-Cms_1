using AssociationBusiness.Handlers;
using AssociationEntities.Common;
using AssociationEntities;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor.FileManager;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;
using AssociationBusiness.Handlers.FileManager;
using System.Text.RegularExpressions;
using Azure.Core;
using AssociationRepository.Association;

namespace AssociationAPI.Controllers
{

    [Route("api/[controller]")]
    // [EnableCors("AllowAllOrigins")]

    public class FileManagerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFileManager _fileManager;

        public FileManagerController(IMediator mediator, IFileManager fileManager)
        {
            _mediator = mediator;
            _fileManager = fileManager;
        }
        [HttpPost]
        [Route("FileOperations")]
        public object FileOperations([FromBody] FileManagerDirectoryContent args)
        {
            var JsonResponse = JsonSerializer.Serialize(args);
            if ((args.Action == "delete" || args.Action == "rename") && ((args.TargetPath == null) && (args.Path == "")))
            {
                FileManagerResponse response = new FileManagerResponse();
                response.Error = new ErrorDetails { Code = "403", Message = "Restricted to modify the root folder." };
                return Utilities.ToCamelCase(response);
            }

            switch (args.Action)
            {
                case "read":
                    return _mediator.Send(new GetFilesHandlerRequest { path = args.Path, showHiddenItems = false, data = args.Data }).Result;
                case "delete":
                    return _mediator.Send(new DeleteFilesHandlerRequest { Path = args.Path, Names = args.Names, Data = args.Data }).Result;
                case "details":
                    return _mediator.Send(new DetailsHandlerRequest { path = args.Path, names = args.Names, data = args.Data }).Result;
                case "create":
                    return _mediator.Send(new CreateFilesHandlerRequest { Path = args.Path, Name = args.Name, Data = args.Data }).Result;
                case "search":
                    return _mediator.Send(new SearchFilesHandlerRequest { Path = args.Path, SearchString = args.SearchString, ShowHiddenItems = args.ShowHiddenItems, CaseSensitive = args.CaseSensitive, Data = args.Data }).Result;
                case "rename":
                    return _mediator.Send(new RenameFilesHandlerRequest { Path = args.Path, Name = args.Name, NewName = args.NewName, Replace = false, Data = args.Data }).Result;
                case "move":
                    return _mediator.Send(new MoveFilesHandlerRequest { Path = args.Path, TargetPath = args.TargetPath, Names = args.Names, RenameFiles = args.RenameFiles, TargetData = args.TargetData, Data = args.Data }).Result;
                case "copy":
                    return _mediator.Send(new CopyFilesHandlerRequest { Path = args.Path, TargetPath = args.TargetPath, Names = args.Names, RenameFiles = args.RenameFiles, TargetData = args.TargetData, Data = args.Data }).Result;
            }
            return null;
        }

        // Uploads the file(s) into a specified path
        [Route("Upload")]
        [HttpPost]
        public IActionResult Upload(string path, long size, IList<IFormFile> uploadFiles, string action, string data)
        {
            FileManagerResponse uploadResponse;
            FileManagerDirectoryContent[] dataObject = new FileManagerDirectoryContent[1];
            dataObject[0] = JsonSerializer.Deserialize<FileManagerDirectoryContent>(data);
            if (dataObject[0].Name == null)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                dataObject[0] = JsonSerializer.Deserialize<FileManagerDirectoryContent>(data, options);
            }
            int chunkIndex = int.TryParse(HttpContext.Request.Form["chunk-index"], out int parsedChunkIndex) ? parsedChunkIndex : 0;
            int totalChunk = int.TryParse(HttpContext.Request.Form["total-chunk"], out int parsedTotalChunk) ? parsedTotalChunk : 0;
            uploadResponse = _fileManager.Upload(path, uploadFiles, action, dataObject, size, chunkIndex, totalChunk);

            if (uploadResponse.Error != null)
            {
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.StatusCode = Convert.ToInt32(uploadResponse.Error.Code);
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = uploadResponse.Error.Message;
            }
            return Content("");
        }

        // Downloads the selected file(s) and folder(s)
        [Route("Download")]
        [HttpPost]
        public async Task<object> Download(string downloadInput)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            FileManagerDirectoryContent args = JsonSerializer.Deserialize<FileManagerDirectoryContent>(downloadInput, options);
            args.Path = (args.Path);
            return _mediator.Send(new DownloadHandlerRequest { path = args.Path, names = args.Names, data = args.Data }).Result;
        }

        [Route("DownloadBase64")]
        public IActionResult DownloadBase64(string downloadInput)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            FileManagerDirectoryContent args = JsonSerializer.Deserialize<FileManagerDirectoryContent>(downloadInput, options);
            args.Path = (args.Path);
            FileStreamResult result = _fileManager.Download(args.Path, args.Names, args.Data);
            string Base64String;

            if (result == null)
            {
                return NotFound("Data Not Found");
            }

            using (var memoryStream = new MemoryStream())
            {

                result.FileStream.CopyTo(memoryStream);
                Base64String = Convert.ToBase64String(memoryStream.ToArray());
            }

            var fileExtension = Path.GetExtension(result.FileDownloadName).ToLower();

            string contentType;

            switch (fileExtension)
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;
                case ".doc":
                    contentType = "application/msword";
                    break;
                case ".docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".txt":
                    contentType = "text/plain";
                    break;
                case ".xls":
                    contentType = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".jpeg":
                case ".jpg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".zip":
                    contentType = "application/zip";
                    break;
                default:
                    throw new NotSupportedException("Unsupported file type");
            }

            Base64String = $"data:{contentType};base64," + Base64String;

            return Ok(new { fileName = result.FileDownloadName, data = Base64String });
        }


        // Gets the image(s) from the given path
        [Route("GetImage")]
        [HttpGet]
        public async Task<IActionResult> GetImage(FileManagerDirectoryContent args)
        {

            return _fileManager.GetImage(args.Path, args.ParentId, args.Id, true, null, null);
            //FileStreamResult response = await _mediator.Send(new GetImageHandlerRequest
            //{
            //    Path = args.Path,
            //    Id = args.Id,
            //    ParentId = args.ParentId,
            //    AllowCompress = false,
            //    Size = null,
            //    Data = null
            //});


            //if (response == null)
            //{
            //    return NotFound("Image not found");
            //}


            //return Ok(response);
        }
    }

}
