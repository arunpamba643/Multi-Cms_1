namespace ScratchCode.Models.DB;

/// Derived class implementing a people display component with a list of persons.
public class ContactPersonComponent : Component
{
    public override ComponentType Type => ComponentType.SingleContactPerson;

    /// List of persons displayed in the component.
    public Guid ContactPersonId { get; set; }

    public Guid TenantId { get; set; }

    /// Method to populate the component's properties.
    public override void PopulateProperties()
    {
        Properties.FirstOrDefault(x => x.Key == "ContactPersonId")!.Value = ContactPersonId;
        Properties.FirstOrDefault(x => x.Key == "TenantId")!.Value = TenantId;
    }
}