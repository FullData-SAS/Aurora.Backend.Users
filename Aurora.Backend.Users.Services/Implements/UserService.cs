using Aurora.Backend.Users.Services.Models.User;
using Aurora.Backend.Users.Services.Persistence.Entities;
using Aurora.Backend.Users.Services.Contracts;
using Aurora.Backend.Users.Services.Enumerables;
using Aurora.Backend.Users.Services.Models;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Backend.Users.Services.Implements;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<UserResponseModel>>> GetAll()
    {
        Result<IEnumerable<UserResponseModel>> result = new Result<IEnumerable<UserResponseModel>>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<AppUser>().GetAllAsync();

            if (resultQuery.Count() > 0)
                result = new Result<IEnumerable<UserResponseModel>>
                {
                    Message = EResult.SUCCESS.ToString(),
                    Status = EResult.SUCCESS,
                    Response = resultQuery.Select(x => new UserResponseModel
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        DocumentType = x.DocumentType,
                        DocumentNumber = x.DocumentNumber,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        GroupId = x.GroupId,
                        Active = x.Active
                    })
                };
            else
                result = new Result<IEnumerable<UserResponseModel>>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = null
                };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<IEnumerable<UserResponseModel>>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = null
            };
        }
            
        return result;
    }
    
    public async Task<Result<UserResponseModel>> Get(Guid guid)
    {
        Result<UserResponseModel> result = new Result<UserResponseModel>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<AppUser>().GetByIdAsync(guid);

            if (resultQuery != null)
                result = new Result<UserResponseModel>
                {
                    Message = EResult.SUCCESS.ToString(),
                    Status = EResult.SUCCESS,
                    Response = new UserResponseModel
                    {
                        Id = resultQuery.Id,
                        FirstName = resultQuery.FirstName,
                        LastName = resultQuery.LastName,
                        DocumentType = resultQuery.DocumentType,
                        DocumentNumber = resultQuery.DocumentNumber,
                        PhoneNumber = resultQuery.PhoneNumber,
                        Email = resultQuery.Email,
                        GroupId = resultQuery.GroupId,
                        Active = resultQuery.Active
                    }
                };
            else
                result = new Result<UserResponseModel>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = null
                };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<UserResponseModel>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = null
            };
        }
            
        return result;
    }
    
    public async Task<Result<UserResponseModel>> Login(string email, string password)
    {
        Result<UserResponseModel> result = new Result<UserResponseModel>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Email == email && x.PasswordHash == password.ToUpper());

            if (resultQuery != null)
                result = new Result<UserResponseModel>
                {
                    Message = EResult.SUCCESS.ToString(),
                    Status = EResult.SUCCESS,
                    Response = new UserResponseModel
                    {
                        Id = resultQuery.Id,
                        FirstName = resultQuery.FirstName,
                        LastName = resultQuery.LastName,
                        DocumentType = resultQuery.DocumentType,
                        DocumentNumber = resultQuery.DocumentNumber,
                        PhoneNumber = resultQuery.PhoneNumber,
                        Email = resultQuery.Email,
                        GroupId = resultQuery.GroupId,
                        Active = resultQuery.Active,
                    }
                };
            else
                result = new Result<UserResponseModel>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = null
                };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<UserResponseModel>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = null
            };
        }
            
        return result;
    }
    
    public async Task<Result<bool>> Create(UserCreateModel clientCreateModel)
    {
        Result<bool> result = new Result<bool>();
        try
        {
            var psw = HashPasswordSha256(GenerateRandomPassword());
            
            await _unitOfWork.GetRepository<AppUser>().AddAsync(new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = clientCreateModel.FirstName,
                LastName = clientCreateModel.LastName,
                DocumentType = clientCreateModel.DocumentType,
                DocumentNumber = clientCreateModel.DocumentNumber,
                PhoneNumber = clientCreateModel.PhoneNumber,
                Email = clientCreateModel.Email,
                PasswordHash = psw.ToUpper(),
                GroupId = clientCreateModel.GroupId,
                LastLogin = clientCreateModel.LastLogin,
                Active = clientCreateModel.Active,
                CreatedAt = clientCreateModel.CreatedAt,
                UpdatedAt = clientCreateModel.UpdatedAt
            });
            await _unitOfWork.CommitAsync();

            result = new Result<bool>
            {
                Message = EResult.SUCCESS.ToString(),
                Status = EResult.SUCCESS,
                Response = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<bool>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = false
            };
        }
            
        return result;
    }
    
    public async Task<Result<bool>> Update(UserUpdateModel clientUpdateModel)
    {
        Result<bool> result = new Result<bool>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<AppUser>().GetByIdAsync(clientUpdateModel.Id);
            if (resultQuery == null)
                return new Result<bool>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = false
                };

            resultQuery.FirstName = clientUpdateModel.FirstName;
            resultQuery.LastName = clientUpdateModel.LastName;
            resultQuery.DocumentType = clientUpdateModel.DocumentType;
            resultQuery.DocumentNumber = clientUpdateModel.DocumentNumber;
            resultQuery.PhoneNumber = clientUpdateModel.PhoneNumber;
            resultQuery.Email = clientUpdateModel.Email;
            resultQuery.GroupId = clientUpdateModel.GroupId;
            resultQuery.LastLogin = clientUpdateModel.LastLogin;
            resultQuery.Active = clientUpdateModel.Active;
            resultQuery.CreatedAt = clientUpdateModel.CreatedAt;
            resultQuery.UpdatedAt = clientUpdateModel.UpdatedAt;
            
            await _unitOfWork.CommitAsync();
            
            result = new Result<bool>
            {
                Message = EResult.SUCCESS.ToString(),
                Status = EResult.SUCCESS,
                Response = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<bool>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = false
            };
        }
            
        return result;
    }
    
    public async Task<Result<bool>> Delete(Guid guid)
    {
        Result<bool> result = new Result<bool>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<AppUser>().GetByIdAsync(guid);
            if (resultQuery == null)
                return new Result<bool>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = false
                };

            resultQuery.Active = false;
            resultQuery.UpdatedAt = DateTime.Now;
            
            await _unitOfWork.CommitAsync();
            
            result = new Result<bool>
            {
                Message = EResult.SUCCESS.ToString(),
                Status = EResult.SUCCESS,
                Response = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<bool>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = false
            };
        }
            
        return result;
    }
    
    public static string GenerateRandomPassword(int length = 12)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        var randomBytes = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var result = new StringBuilder(length);
        foreach (var b in randomBytes)
        {
            result.Append(validChars[b % validChars.Length]);
        }

        return result.ToString();
    }

    // Cifra la contraseña con SHA256
    public static string HashPasswordSha256(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToHexString(hash); // Devuelve en formato hexadecimal (todo en mayúsculas)
    }

}