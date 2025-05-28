using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Filings that contain document types
/// </summary>
public partial class Filing
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string Subject { get; set; } = null!;

    public Guid FileId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual FileRecord File { get; set; } = null!;

    public virtual ICollection<FilingDocumentType> FilingDocumentTypes { get; set; } = new List<FilingDocumentType>();
}
