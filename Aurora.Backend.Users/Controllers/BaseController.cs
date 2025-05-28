using System.Text;
using Aurora.Backend.Users.Services.Enumerables;
using Aurora.Backend.Users.Services.Models;
using Aurora.Backend.Users.Services.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Backend.Users.Controllers;

[EnableCors]
[ApiController]
[Route("Users/api")]
public abstract class BaseController : ControllerBase
{
    public BaseController()
    {
    }

    protected Result<object> GetApiResponse(EResult state, string msj, object data)
    {
        return new Result<object> { Status = state, Message = msj, Response = data };
    }

    protected Dictionary<string, string> GetErrorListFromModelState()
    {
        var errList = new Dictionary<string, string>();
        var errIndex = 1;
        var builder = new StringBuilder();
        foreach (var value in ModelState.Values)
        {
            foreach (var item in value.Errors)
                builder.Append($"{item.ErrorMessage} - ");

            if (builder.Length > 0)
            {
                errList.Add($"error{errIndex}", builder.ToString().Substring(0, builder.Length - 3));
                errIndex += 1;
                builder.Clear();
            }
        }

        return errList;
    }

    protected bool IsBase64Valid(string base64)
    {
        bool response = true;
        try
        {
            Convert.FromBase64String(base64);
        }
        catch
        {
            response = false;
        }
        return response;
    }
    
    protected IActionResult ValidateEnumState(int state)
    {
        if (Enum.IsDefined(typeof(EGeneralStates), state))
            return null;
        else
            return BadRequest(GetApiResponse(EResult.ERROR, TextValues.ESTATE_IS_NOT_VALID, null));
    }
}