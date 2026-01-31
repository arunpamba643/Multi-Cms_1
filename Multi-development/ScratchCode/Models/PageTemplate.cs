namespace ScratchCode.Models;

/// <summary>
/// Page Template - This will be a predefined template for a page.
/// </summary>
public class PageTemplate
{
    /// <summary>
    /// Unique Identifier for the Template
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Template Name - This will be a unique name for the template.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Predefined Rows containing containers and components. Which from a new Page Instance will be created
    /// </summary>
    public ICollection<Row> Rows { get; set; } = new HashSet<Row>();
}