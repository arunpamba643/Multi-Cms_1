namespace ScratchCode.ApiModels;


/// <summary>
/// This is our implemtation -> Be aware that you might want to change the implementation to fit your needs
/// </summary>
public class BlogEntry 
{
    private static readonly string _hardPrefix = "/b/d";

    public string Title { get; set; }

    
    public string ScopeType { get; set; }

    
    public string TemplateId { get; set; }

    
    public string DetailItemId { get; set; }

    public List<TagElement> Tags { get; set; } = new();

    public string ComponentsAsJson { get; set; }

    
    public DateTime ValidUntil { get; set; }

    
    public DateTime ValidFrom { get; set; }

    
    public DateTime CreatedAt { get; set; }

    
    public DateTime UpdatedAt { get; set; }

    public string Link { get; set; }

    public PersonInfo Creator { get; set; }

    public List<Image> Images { get; set; } = new();

    public string Id { get; set; }    
    public string TenantId { get; set; }
    public string GetLink()
    {
        return _hardPrefix + Link + "-" + Id;
    }
}