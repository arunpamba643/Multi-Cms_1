namespace ScratchCode.Models;

/// Base class for containers with a unique id and a name.
public abstract class Container
{
    /// Unique identifier for the container.
    public int Id { get; set; }

    /// Name of the container.
    public string Name { get; set; }

    /// <summary>
    /// Represents the width needed for the component to display properly.
    /// Range from 0-12
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// Height of the Container from 1-3 where 1 is the full height
    /// </summary>
    public int Height { get; set; }

    /// Collection of components placed inside the container.
    public virtual ICollection<Component> Components { get; set; } = new HashSet<Component>();
}