using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ScratchCode.ApiModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderByType
    {
        [EnumMember(Value = "ascending")]
        Ascending,

        [EnumMember(Value = "descending")]
        Descending
    }
}