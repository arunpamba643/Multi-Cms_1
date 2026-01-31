using AssociationEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AssociationEntities.CustomModels
{
    public class BlogPagesModel
    {
        public int Bpid { get; set; }

        public int? TenantId { get; set; }

        public string Title { get; set; } = null!;

        public string? ScopeType { get; set; }

        public string? Description { get; set; }

        public string? DetailItemId { get; set; }

        public string? Tags { get; set; }

        public DateTime? ValidTo { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? Link { get; set; }

        public string? Division { get; set; }

        public string? Discipline { get; set; }

        public DateTime? PublishedOn { get; set; }

        public string? HeaderImage { get; set; }


    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ScopeType
    {
        Intern,
        Public
    }

    public class BlogFilters
    {
        public int TenantId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? SearchFields { get; set; }
        public string? Keyword { get; set; }
        public string? DivisionsIds { get; set; }
        public string? DisciplinesIds { get; set; }
        public string? TagIds { get; set; }
        public string? CreatorIds { get; set; }
    }

    public class BlogFilterSuggestions
    {
        public string FilterType { get; set; }
        public string FilterValue { get; set; }
    }
}
