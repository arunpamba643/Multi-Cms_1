
namespace ScratchCode.ApiModels
{
    public class ContactPerson 
    {
        
        public string Id { get; set; }

        
        public string Pkey { get; set; }

        
        public string TenantId { get; set; }

        
        public string ElementId { get; set; }

        public string UserId { get; set; }

        public string UserB2CId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int    Age   { get; set; }
        
        public string Title { get; set; }

        public string AvatarImageUrl { get; set; }

        public string InternAvatarImageUrl { get; set; }

        public string ExternAvatarImageUrl { get; set; }

        public string HeaderImageUrl { get; set; }

        
        public List<string> ContactPersonTypes { get; set; }
        
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        
        public DateTime LastSyncAt { get; set; } = DateTime.UtcNow;
    }
}