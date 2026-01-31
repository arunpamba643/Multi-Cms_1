using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssociationEntities.Models;

public partial class BlogPost
{
    public int Id { get; set; } 

    public string Title { get; set; } = null!;

    public ScopeType ScopeType { get; set; }

    public string? Description { get; set; }

    public string? DetailItemId { get; set; }

    public string? Tags { get; set; }

    public DateTime? ValidTo { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Link { get; set; }

    public int TenantId { get; set; }
}
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ScopeType
{
    Intern,
    Public
}