using Aurora.Backend.Users.Services.Models;
using Aurora.Backend.Users.Services.Models.User;

namespace Aurora.Backend.Users.Services.Contracts;

public interface IUserService
{
    Task<Result<IEnumerable<UserUpdateModel>>> GetAll();
    Task<Result<UserUpdateModel>> Get(Guid guid);
    Task<Result<UserUpdateModel>> Login(string email, string password);
    Task<Result<bool>> Create(UserCreateModel clientCreateModel);
    Task<Result<bool>> Update(UserUpdateModel clientUpdateModel);
    Task<Result<bool>> Delete(Guid guid);
}