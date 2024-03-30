using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Models.Wrappers;

namespace UserManagement.Server.Controllers;

[Route("api/users")]
[ApiController]
public class DeleteUserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public DeleteUserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var result = _userRepository.DeleteById(id);

        if (result.IsSeccess)
        {
            return Ok(new ResponseWrapper<object>()
            {
                Status = new()
                {
                    Code = HttpStatusCode.OK.ToString(),
                    Description = "OK"
                },
                Data = new
                {
                    Result = result.IsSeccess,
                    result.Message
                }
            });
        }

        return BadRequest(new ResponseWrapper<object>()
        {
            Status = new()
            {
                Code = HttpStatusCode.BadRequest.ToString(),
                Description = "BadRequest"
            },
            Data = new
            {
                Result = result.IsSeccess,
                Message = result?.Error?.Message
                    ?? result?.Message
                    ?? "Can not delete"
            }
        });
    }
}
