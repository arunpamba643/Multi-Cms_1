namespace ScratchCode;

/// Concrete implementation of a component with a unique id, a name, a type, an order number, and a collection of properties.
public class Component
{
    /// Unique identifier for the component.
    public int Id { get; set; }
    
    /// Name of the component.
    public string Name { get; set; }
    
    /// Type of the component.
    public virtual ComponentType Type { get; set; }
    
    /// Position of the component in relation to others.
    public int OrderNumber { get; set; }
    
    /// <summary>
    /// Represents the width needed for the component to display properly.
    /// Range from 0-12
    /// </summary>
    public int Width { get; set; }
    
    /// Collection of properties belonging to the component.
    public virtual IEnumerable<ComponentProperty> Properties { get; set; } = Enumerable.Empty<ComponentProperty>();

    public virtual void PopulateProperties()
    {
        throw new NotImplementedException();
    }
}
