using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Types of documents that can be managed
/// </summary>
public partial class DocumentType
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<FilingDocumentType> FilingDocumentTypes { get; set; } = new List<FilingDocumentType>();

    public virtual ICollection<RetentionTableDocumentType> RetentionTableDocumentTypes { get; set; } = new List<RetentionTableDocumentType>();
}
