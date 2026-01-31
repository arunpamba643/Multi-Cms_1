using Newtonsoft.Json;

namespace ScratchCode.ApiModels
{
    public class BaseList<T>
    {
        public BaseList()
        {
        }

        [JsonProperty(PropertyName = "items")] public List<T> Items { get; set; } = new List<T>();

        [JsonProperty(PropertyName = "page")] public long? Page { get; set; }

        [JsonProperty(PropertyName = "pagesize")]
        public long? PageSize { get; set; }

        [JsonProperty(PropertyName = "keyword")]
        public string Keyword { get; set; }

        [JsonProperty(PropertyName = "searchfields")]
        public string SearchFields { get; set; }

        [JsonProperty(PropertyName = "maximumcount")]
        public long MaximumCount { get; set; }

        [JsonProperty(PropertyName = "orderbytype")]
        public OrderByType OrderByType { get; set; }

        [JsonProperty(PropertyName = "orderby")]
        public string OrderBy { get; set; }
    }
}