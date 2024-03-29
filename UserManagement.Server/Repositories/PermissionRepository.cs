using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Data;
using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly UserManagementContext _context;

    public PermissionRepository(UserManagementContext context)
    {
        _context = context;
    }

    public IList<Permission> AllPermission => [.. _context.Permissions];
}
