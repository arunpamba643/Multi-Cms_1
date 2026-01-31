namespace ScratchCode.Models;

/// Represents a row containing one or more containers.
public class Row
{
    /// Unique identifier for the row.
    public Guid Id { get; set; }
    
    /// Position of the row in relation to others.
    public int OrderNumber { get; set; }
    
    /// Collection of containers contained within the row.
    public List<Container> Containers { get; set; } = new List<Container>();
}
