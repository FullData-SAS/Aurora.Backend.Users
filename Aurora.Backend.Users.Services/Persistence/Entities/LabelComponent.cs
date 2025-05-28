using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

public partial class LabelComponent
{
    public Guid Id { get; set; }

    public string NameEs { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string? Value { get; set; }

    public int? FontSize { get; set; }

    public string? FontFamily { get; set; }

    public Guid LabelId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Label Label { get; set; } = null!;
}
