using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Labels for printing
/// </summary>
public partial class Label
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Width { get; set; }

    public int Height { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<LabelComponent> LabelComponents { get; set; } = new List<LabelComponent>();
}
