using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Common.Repositories;

public interface IPermissionRepository
{
    public IList<Permission> AllPermission { get; }
}
