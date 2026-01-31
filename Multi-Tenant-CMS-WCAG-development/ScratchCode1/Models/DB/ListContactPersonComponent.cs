namespace ScratchCode.Models.DB;

/// Derived class implementing a people display component with a list of persons.
public class ListContactPersonComponent : Component
{
    public override ComponentType Type => ComponentType.ListContactPerson;

    /// List of persons displayed in the component.
    public Guid ObjectId { get; set; }

    public Guid TenantId { get; set; }

    /// Method to populate the component's properties.
    public override void PopulateProperties()
    {
        Properties.FirstOrDefault(x => x.Key == "ObjectId")!.Value = ObjectId;
        Properties.FirstOrDefault(x => x.Key == "TenantId")!.Value = TenantId;
    }
}
