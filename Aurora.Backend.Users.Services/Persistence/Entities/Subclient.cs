using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Stores information about subclients associated with a main client
/// </summary>
public partial class Subclient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public bool? Active { get; set; }

    public Guid ClientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CertifiedEmail> CertifiedEmails { get; set; } = new List<CertifiedEmail>();

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<DigitalSignature> DigitalSignatures { get; set; } = new List<DigitalSignature>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}
