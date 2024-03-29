﻿using Microsoft.AspNetCore.Http;
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
            var userDto = addResult.Data;
            userDto.Password = null;
            userDto.RoleID = null;
            
            response = new()
            {
                Status = new()
                {
                    Code = HttpStatusCode.OK.ToString(),
                    Description = "OK"
                },
                Data = userDto
            };

        }

        return Ok(response);
    }
}