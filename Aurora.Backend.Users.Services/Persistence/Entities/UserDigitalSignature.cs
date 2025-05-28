using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class UserDigitalSignature
{
    public Guid UserId { get; set; }

    public Guid DigitalSignatureId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual DigitalSignature DigitalSignature { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
