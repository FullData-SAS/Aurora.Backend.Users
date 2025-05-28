using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Roles that can be assigned to users
/// </summary>
public partial class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
