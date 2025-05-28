using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class DocumentFile
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public Guid DocumentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Document Document { get; set; } = null!;
}
