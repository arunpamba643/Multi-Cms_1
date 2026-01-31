namespace ScratchCode.Models;

/// Concrete implementation of a component with a unique id, a name, a type, an order number, and a collection of properties.
public class BaseComponent
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
    /// Each component has a text property but some will not use it.
    /// If some additional information is needed to display the user can add it here.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Represents the width needed for the component to display properly.
    /// Range from 0-12
    /// </summary>
    public int Width { get; set; }
}