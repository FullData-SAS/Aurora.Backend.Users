using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Document subseries
/// </summary>
public partial class Subseries
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public Guid SeriesId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Series Series { get; set; } = null!;
}
