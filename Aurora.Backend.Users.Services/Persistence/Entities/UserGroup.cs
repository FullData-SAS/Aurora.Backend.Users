using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// User groups with specific permissions
/// </summary>
public partial class UserGroup
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();

    public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
}
