
namespace ScratchCode.ApiModels
{
    public class TagElement
    {
        
        public string Id { get; set; }

        
        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public string BorderColor { get; set; }

        public string TextColor { get; set; }
        
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        
        public DateTime LastSyncAt { get; set; } = DateTime.UtcNow;
    }
}