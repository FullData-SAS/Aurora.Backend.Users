using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Digital signatures acquired by subclients
/// </summary>
public partial class DigitalSignature
{
    public Guid Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int TotalSeats { get; set; }

    public int? UsedSeats { get; set; }

    public int? AvailableSeats { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public decimal Value { get; set; }

    public Guid SubclientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Subclient Subclient { get; set; } = null!;

    public virtual ICollection<UserDigitalSignature> UserDigitalSignatures { get; set; } = new List<UserDigitalSignature>();
}
