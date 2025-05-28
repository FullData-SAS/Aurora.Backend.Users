using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Certified emails acquired by subclients
/// </summary>
public partial class CertifiedEmail
{
    public Guid Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int TotalEmails { get; set; }

    public int? UsedEmails { get; set; }

    public int? AvailableEmails { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public decimal Value { get; set; }

    public Guid SubclientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Subclient Subclient { get; set; } = null!;
}
