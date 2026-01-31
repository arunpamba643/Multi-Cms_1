namespace ScratchCode;

/// Represents a page with a title, URL path, and a main component.
public class Page
{
    /// Unique identifier for the page.
    public int Id { get; set; }

    /// Title of the page.
    public string Title { get; set; }

    /// <summary>
    /// Url path of the page. For Example /myCustom/PageUrl.
    /// For better routing, apply the same resourcepath to the resourcepath if possible
    /// </summary>
    public string ResourcePath { get; set; }

    /// Identifier of the main component on the page.
    public int? MainComponentId { get; set; }

    /// <summary>
    /// Starting publish date of the page.
    /// </summary>
    public DateTime? PublishStarting { get; set; }

    /// <summary>
    /// Endning publish date of the page.
    /// </summary>
    public DateTime? PublishEnding { get; set; }


    /// <summary>
    /// Represents the Padding in Width for the page.
    /// Values from 1-8 where it represents the padding in width.
    /// Padding should applied to the left and right of the page.
    /// If the padding is 2 then the page should have 2 columns of padding on the left and right.
    /// The content can then be only 8 columns wide.
    /// </summary>
    public int PaddingWidth { get; set; }
    
    /// <summary>
    /// Rows containing containers and components.
    /// </summary>
    public ICollection<Row> Rows { get; set; } = new HashSet<Row>();
}