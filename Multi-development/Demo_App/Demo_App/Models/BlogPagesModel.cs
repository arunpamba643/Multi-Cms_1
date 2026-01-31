using AssociationEntities.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Demo_App.Models
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

        [Required(ErrorMessage = "Title is mandatory")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Scope Type is mandatory")]
        public ScopeType? ScopeType { get; set; }
        [Required(ErrorMessage = "Description is mandatory")]
        [MinLength(200, ErrorMessage ="Minmum length is 200 characters")]
        public string? Description { get; set; }

        public string? DetailItemId { get; set; }

        public string? Tags { get; set; }

        public DateTime? ValidTo { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? Link { get; set; }

        public int? TenantId { get; set; } = 0;
        public string? HeaderImage { get; set; }
        public virtual ICollection<BlogAttribute> BlogAttributes { get; set; } = new List<BlogAttribute>();

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ScopeType
    {
        Intern,
        Public
    }

    public class BlogAttribute
    {
        public int Baid { get; set; }

        public int? BpId { get; set; }

        public string? AttributeTitle { get; set; }

        public string? AttributeType { get; set; }


    }

}
