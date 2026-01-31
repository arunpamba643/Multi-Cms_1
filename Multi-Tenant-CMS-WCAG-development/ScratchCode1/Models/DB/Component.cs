namespace ScratchCode.Models;

/// Concrete implementation of a component with a unique id, a name, a type, an order number, and a collection of properties.
public class Component : BaseComponent
{

    /// Collection of properties belonging to the component.
    public virtual IEnumerable<ComponentProperty> Properties { get; set; } = Enumerable.Empty<ComponentProperty>(); // This Data could also be  Dictiornary<string, dynamic> Properties and be dumped as JSON in SQL

    public virtual void PopulateProperties()
    {
        throw new NotImplementedException();
    }
}