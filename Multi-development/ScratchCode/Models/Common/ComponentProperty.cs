namespace ScratchCode.Models;

/// Class representing a component property with a unique id, a component id, a key, and a value.
public class ComponentProperty
{
    /// Unique identifier for the property.
    public int PropertyId { get; set; }
    
    /// Key identifying the property.
    public string Key { get; set; }
    
    /// Value of the property.
    public dynamic Value { get; set; }
    
    /// Parent component of the property. Referencing for DB purposes.
    //public virtual Component Component { get; set; }
    /// Identifier of the parent component. Referencing for DB purposes.
    //public int ComponentId { get; set; }
}
