using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class FilingFile
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public Guid FilingDocumentTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual FilingDocumentType FilingDocumentType { get; set; } = null!;
}
