using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Permissions assigned to roles
/// </summary>
public partial class RolePermission
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public string Module { get; set; } = null!;

    public bool? CreatePerm { get; set; }

    public bool? ReadPerm { get; set; }

    public bool? UpdatePerm { get; set; }

    public bool? DeletePerm { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Role Role { get; set; } = null!;
}
