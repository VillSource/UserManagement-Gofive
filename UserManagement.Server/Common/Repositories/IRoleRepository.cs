using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Common.Repositories;

public interface IRoleRepository
{
    public IList<Role> AllRoles { get; }
}
