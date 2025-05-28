using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Steps of workflows
/// </summary>
public partial class Step
{
    public Guid Id { get; set; }

    public int OrderNum { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? TimeLimit { get; set; }

    public bool? Required { get; set; }

    public Guid FlowId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Flow Flow { get; set; } = null!;

    public virtual ICollection<StepUser> StepUsers { get; set; } = new List<StepUser>();
}
