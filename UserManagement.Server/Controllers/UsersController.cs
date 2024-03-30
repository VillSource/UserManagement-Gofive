using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Models.DtoRequest;
using UserManagement.Server.Models.Entities;
using UserManagement.Server.Models.Wrappers;

namespace UserManagement.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public IActionResult Add(RequestAddUserDto data)
    {
        var user = new User()
        {
            UserID = data.ID,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Phone = data.Phone,
            RoleID = data.RoleID,
            Username = data.Username,
            Password = data.Password
        };

        var permissions = data.Permissions.Select(i => new UserPermission()
        {
            UserID = data.ID,
            IsDeletable = i.IsDeletable,
            IsReadable = i.IsReadable,
            IsWriteable = i.IsReadable,
            PermissionID = i.PermissionID,
        }).ToList();

        var addResult = _userRepository.Add(user, permissions);

        ResponseWrapper<User> response;

        if (!addResult.IsSeccess)
        {
            response = new()
            {
                Status = new()
                {
                    Code = HttpStatusCode.BadRequest.ToString(),
                    Description = addResult.Message
                        ?? addResult?.Error?.Message
                        ?? ""
                },
            };
        }
        else
        {
            return GetUserByID(data.ID);
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserByID(string id)
    {
        var userResult = _userRepository.GetUserById(id);

        if (userResult.IsSeccess)
        {
            var user = userResult.Data;
            var data = new
            {
                user.UserID,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Phone,
                user.Role,
                user.Username,
                Permissions = user.Permissions
                    .Select(i => new
                    {
                        i.Permission.PermissionId,
                        i.Permission.PermissionName
                    }),
            };
            return Ok(new ResponseWrapper()
            {
                Status = new()
                {
                    Code = HttpStatusCode.OK.ToString(),
                    Description = "OK"
                },
                Data = data
            });
        }

        return BadRequest(new ResponseWrapper()
        {
            Status = new()
            {
                Code = HttpStatusCode.BadRequest.ToString(),
                Description = userResult?.Message
                    ?? userResult?.Error?.Message
                    ?? "Can not get user."
            }
        });
    }

    [HttpPut("{id}")]
    public IActionResult EditUser(string id, [FromBody] RequestEditUserDto body)
    {
        var user = new User
        {
            UserID = id,
            FirstName = body.FirstName,
            LastName = body.LastName,
            Email = body.Email,
            Phone = body.Phone,
            RoleID = body.RoleID,
            Password = body.Password,
            Username = body.Username,
        };

        var permission = body.Permissions
            .Select(i => new UserPermission()
            {
                UserID = id,
                PermissionID = i.PermissionID,
                IsReadable = i.IsReadable,
                IsWriteable = i.IsWritable,
                IsDeletable = i.IsDeletable,
            }).ToList();

        var result = _userRepository.EditUser(user, permission);

        if (!result.IsSeccess)
        {
            return BadRequest(new ResponseWrapper()
            {
                Status = new()
                {
                    Code= HttpStatusCode.BadRequest.ToString(),
                    Description = result.Message
                        ?? result.Error?.Message
                        ?? "Can not update user."
                }
            });
        }

        return GetUserByID(id);
    }
}
