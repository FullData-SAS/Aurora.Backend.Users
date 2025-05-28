using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class FilingDocumentType
{
    public Guid Id { get; set; }

    public Guid FilingId { get; set; }

    public Guid DocumentTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual Filing Filing { get; set; } = null!;

    public virtual ICollection<FilingFile> FilingFiles { get; set; } = new List<FilingFile>();
}
