using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Permissions assigned to groups
/// </summary>
public partial class GroupPermission
{
    public Guid Id { get; set; }

    public Guid GroupId { get; set; }

    public string Module { get; set; } = null!;

    public bool? CreatePerm { get; set; }

    public bool? ReadPerm { get; set; }

    public bool? UpdatePerm { get; set; }

    public bool? DeletePerm { get; set; }

    public bool? AssignPerm { get; set; }

    public bool? SignPerm { get; set; }

    public bool? ManagePerm { get; set; }

    public bool? ApprovePerm { get; set; }

    public bool? AllocatePerm { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual UserGroup Group { get; set; } = null!;
}
