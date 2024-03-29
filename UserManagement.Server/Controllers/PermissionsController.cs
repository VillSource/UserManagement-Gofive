using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Models.Entities;
using UserManagement.Server.Models.Wrappers;

namespace UserManagement.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionsController(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var allPermissions = _permissionRepository.AllPermissions;
        var response = new ResponseWrapper<IList<Permission>>()
        {
            Status = new()
            {
                Code= HttpStatusCode.OK.ToString(),
                Description = "OK"
            },
            Data = allPermissions
        };

        return Ok(response);
    }
}
