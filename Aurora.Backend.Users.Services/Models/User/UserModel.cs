namespace Aurora.Backend.Users.Services.Models.User;

public class UserCreateModel
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public Guid? GroupId { get; set; }

    public bool? Active { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

public class UserUpdateModel : UserCreateModel
{
    public Guid Id { get; set; }
}
