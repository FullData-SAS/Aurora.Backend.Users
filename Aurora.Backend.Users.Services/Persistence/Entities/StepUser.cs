using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class StepUser
{
    public Guid StepId { get; set; }

    public Guid UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Step Step { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
