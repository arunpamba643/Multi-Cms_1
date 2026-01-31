namespace ScratchCode;

/// Class representing a component property with a unique id, a component id, a key, and a value.
public class ComponentProperty
{
    /// Unique identifier for the property.
    public int PropertyId { get; set; }
    
    /// Identifier of the parent component.
    public int ComponentId { get; set; }
    
    /// Key identifying the property.
    public string Key { get; set; }
    
    /// Value of the property.
    public dynamic Value { get; set; }
    
    /// Parent component of the property.
    public virtual Component Component { get; set; }
}
