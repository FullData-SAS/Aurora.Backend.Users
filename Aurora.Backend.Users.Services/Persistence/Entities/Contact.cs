using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Contacts associated with subclients
/// </summary>
public partial class Contact
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Position { get; set; }

    public string? Department { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public Guid SubclientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Subclient Subclient { get; set; } = null!;
}
