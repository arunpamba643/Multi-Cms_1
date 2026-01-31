namespace ScratchCode.Models.UI;

/// Derived class implementing a people display component with a list of persons.
public class ListContactPersonComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.ListContactPerson;

    /// ContactPerson to display
    public IEnumerable<ContactPerson> ContactPersons { get; set; }
}