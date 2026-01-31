namespace ScratchCode.Models.UI;

/// Derived class implementing a people display component with a list of persons.
public class ContactPersonComponent : BaseComponent
{
    public override ComponentType Type => ComponentType.SingleContactPerson;

    /// ContactPerson to display
    public ContactPerson ContactPerson { get; set; }
}