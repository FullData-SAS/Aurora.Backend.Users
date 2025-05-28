using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Stores information about the main clients of the system
/// </summary>
public partial class Client
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string? Location { get; set; }

    public string? CompanyType { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Subclient> Subclients { get; set; } = new List<Subclient>();
}
