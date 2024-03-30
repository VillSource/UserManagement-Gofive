using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Common.Repositories;

public interface IUserRepository
{
    Result<User> Add(User user, IList<UserPermission> permissions);
    Result DeleteById(string id);
}

