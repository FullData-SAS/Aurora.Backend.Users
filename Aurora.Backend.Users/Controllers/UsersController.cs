using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Aurora.Backend.Users.Services.Contracts;
using Aurora.Backend.Users.Services.Enumerables;
using Aurora.Backend.Users.Services.Models;
using Aurora.Backend.Users.Services.Models.User;
using Aurora.Backend.Users.Services.Utils;

namespace Aurora.Backend.Users.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;
    
    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    [Route("GetAll")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<UserUpdateModel>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _userService.GetAll();
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }
    
    [HttpGet]
    [Route("Get")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<UserUpdateModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Get([Required][FromQuery] Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _userService.Get(id);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }
    
    [HttpGet]
    [Route("Login")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<UserUpdateModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Login([Required][FromQuery] string email, [Required][FromQuery] string password)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _userService.Login(email, password);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }

    [HttpPost]
    [Route("Create")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Create(UserCreateModel request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _userService.Create(request);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }
    
    [HttpPut]
    [Route("Update")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Update(UserUpdateModel request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _userService.Update(request);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }

    [HttpDelete]
    [Route("Delete")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _userService.Delete(id);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }

}