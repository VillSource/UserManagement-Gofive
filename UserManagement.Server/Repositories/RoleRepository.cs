using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Data;
using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly UserManagementContext _context;

    public RoleRepository(UserManagementContext context)
    {
        _context = context;
    }

    public IList<Role> AllRoles => [.. _context.Roles];
}
