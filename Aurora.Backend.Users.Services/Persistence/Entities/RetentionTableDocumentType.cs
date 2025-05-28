using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class RetentionTableDocumentType
{
    public Guid RetentionTableId { get; set; }

    public Guid DocumentTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual RetentionTable RetentionTable { get; set; } = null!;
}
