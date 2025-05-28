using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class DocumentAction
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Document Document { get; set; } = null!;
}
