using AssociationEntities.Models;
using System.Text.Json.Serialization;

namespace WebsiteDemo.Models
{
    public class BlogCreatorModel
    {
        public int Bcid { get; set; }
        public int? Bpid { get; set; }

        public string? Gender { get; set; }

        public string? Firstname { get; set; }

        public string? SecondName { get; set; }

        public int? Age { get; set; }
    }
    public class BlogPagesModel
    {
        public int BpId { get; set; }

        public string Title { get; set; } = null!;

        public ScopeType? ScopeType { get; set; }

        public string? Description { get; set; }

        public string? DetailItemId { get; set; }

        public string? Tags { get; set; }
        public string? HeaderImage { get; set; }
        public DateTime? ValidTo { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? Link { get; set; }

        public int? TenantId { get; set; } = 0;

        public BlogCreatorModel BlogCreator = new BlogCreatorModel();
        public virtual ICollection<BlogCreatorModel> BlogCreators { get; set; } = new List<BlogCreatorModel>();


    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ScopeType
    {
        Intern,
        Public
    }
}
