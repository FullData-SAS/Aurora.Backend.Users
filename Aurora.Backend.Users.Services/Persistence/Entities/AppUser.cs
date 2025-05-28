using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// System users
/// </summary>
public partial class AppUser
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public Guid? GroupId { get; set; }

    public bool? Active { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Document> DocumentAssignedTos { get; set; } = new List<Document>();

    public virtual ICollection<Document> DocumentCreatedByNavigations { get; set; } = new List<Document>();

    public virtual ICollection<FileRecord> FileRecords { get; set; } = new List<FileRecord>();

    public virtual UserGroup? Group { get; set; }

    public virtual ICollection<StepUser> StepUsers { get; set; } = new List<StepUser>();

    public virtual ICollection<UserDigitalSignature> UserDigitalSignatures { get; set; } = new List<UserDigitalSignature>();
}
