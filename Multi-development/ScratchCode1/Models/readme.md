
## PageTemplate

```markdown
public class PageTemplate
{
    /// <summary>
    /// Unique Identifier for the Template
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Template Name - This will be a unique name for the template.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Predefined Rows containing containers and components. Which from a new Page Instance will be created
    /// </summary>
    public ICollection<Row> Rows { get; set; } = new HashSet<Row>();
}
```

## Row

```markdown
public class Row
{
    /// Unique identifier for the row.
    public Guid Id { get; set; }

    /// Position of the row in relation to others.
    public int OrderNumber { get; set; }

    /// Collection of containers contained within the row.
    public List<Container> Containers { get; set; } = new List<Container>();
}
```

## Container (Base Class)

```markdown
public abstract class Container
{
    /// Unique identifier for the container.
    public int Id { get; set; }

    /// Name of the container.
    public string Name { get; set; }

    /// <summary>
    /// Represents the width needed for the component to display properly.
    /// Range from 0-12
    /// </summary>
    public int Width { get; set; }

    /// Height of the Container from 1-3 where 1 is the full height
    public int Height { get; set; }

    /// Collection of components placed inside the container.
    public virtual ICollection<Component> Components { get; set; } = new HashSet<Component>();
}
```

## Component

```markdown
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

public enum ComponentType
{
    ContactPerson,
    EventList,
    BlogList
}
```

This should provide enough detail for anyone looking at your README.MD file to understand how the C# classes are structured for managing templates, rows, containers, and components.