using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Files that group filings
/// </summary>
public partial class FileRecord
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? CompanyName { get; set; }

    public string Entity { get; set; } = null!;

    public string Series { get; set; } = null!;

    public string Subseries { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Filing> Filings { get; set; } = new List<Filing>();
}
