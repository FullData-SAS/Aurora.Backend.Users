namespace Aurora.Backend.Users.Services.Models.User;

public class UserResponseModel
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public Guid? GroupId { get; set; }

    public bool? Active { get; set; }
}