using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Document series
/// </summary>
public partial class Series
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public Guid EntityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Entity Entity { get; set; } = null!;

    public virtual ICollection<Subseries> Subseries { get; set; } = new List<Subseries>();
}
