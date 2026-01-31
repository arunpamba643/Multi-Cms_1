namespace ScratchCode;

/// Derived class implementing an event list component with a list of events.
public class EventListComponent : Component
{
    public override ComponentType Type => ComponentType.EventList;
    
    /// List of events displayed in the component.
    public List<Event> Events { get; set; }

    /// Method to populate the component's properties.
    public override void PopulateProperties()
    {
        // Implementation to populate the properties
    }
}

public class Event
{
    public Guid Id { get; set; }
}