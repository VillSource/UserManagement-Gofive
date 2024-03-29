using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Models.Entities;
using UserManagement.Server.Models.Wrappers;

namespace UserManagement.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RolesController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var roles = _roleRepository.AllRoles;
        var response = new ResponseWrapper<IList<Role>>()
        {
            Data = roles,
            Status = new()
            {
                Code = HttpStatusCode.OK.ToString(),
                Description = "OK"
            }
        };

        return Ok(response);
    }
}
