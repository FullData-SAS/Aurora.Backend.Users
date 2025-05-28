using Aurora.Backend.Users.Services.Models;
using Aurora.Backend.Users.Services.Models.User;

namespace Aurora.Backend.Users.Services.Contracts;

public interface IUserService
{
    Task<Result<IEnumerable<UserResponseModel>>> GetAll();
    Task<Result<UserResponseModel>> Get(Guid guid);
    Task<Result<UserResponseModel>> Login(string email, string password);
    Task<Result<bool>> Create(UserCreateModel clientCreateModel);
    Task<Result<bool>> Update(UserUpdateModel clientUpdateModel);
    Task<Result<bool>> Delete(Guid guid);
}