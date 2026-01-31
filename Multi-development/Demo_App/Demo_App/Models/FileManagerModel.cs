using Newtonsoft.Json;

namespace Demo_App.Models
{
    public class FileManagerModel
    {
    }

    public class FileDownloadResponse
    {
        public string fileName { get; set; }
        public string data { get; set; }
    }
    public class DownloadFileDetails
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        [JsonProperty("dateModified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("hasChild")]
        public bool HasChild { get; set; }

        [JsonProperty("isFile")]
        public bool IsFile { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("filterPath")]
        public string FilterPath { get; set; }

        [JsonProperty("filterId")]
        public string FilterId { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("caseSensitive")]
        public bool CaseSensitive { get; set; }

        [JsonProperty("showHiddenItems")]
        public bool ShowHiddenItems { get; set; }
    }

}
