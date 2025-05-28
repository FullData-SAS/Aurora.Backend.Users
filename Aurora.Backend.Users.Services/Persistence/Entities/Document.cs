using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Documents generated in the system
/// </summary>
public partial class Document
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Entity { get; set; } = null!;

    public string Series { get; set; } = null!;

    public string Subseries { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? FileId { get; set; }

    public Guid? AssignedToId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AppUser? AssignedTo { get; set; }

    public virtual AppUser? CreatedByNavigation { get; set; }

    public virtual ICollection<DocumentAction> DocumentActions { get; set; } = new List<DocumentAction>();

    public virtual ICollection<DocumentFile> DocumentFiles { get; set; } = new List<DocumentFile>();

    public virtual FileRecord? File { get; set; }
}
