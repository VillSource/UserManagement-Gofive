using Microsoft.EntityFrameworkCore;
using UserManagement.Server.Common;
using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Data;
using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagementContext _context;

    public UserRepository(UserManagementContext context)
    {
        _context = context;
    }

    public Result<User> Add(User user, IList<UserPermission> permissions)
    {
        if (_context.Users.Any(i => i.UserID == user.UserID))
        {
            return new Result<User>()
            {
                Error = new("Dupicated User ID.")
            };
        }
        if (!_context.Roles.Any(i => i.RoleId == user.RoleID))
        {
            return new Result<User>()
            {
                Error = new("Invalid Role ID.")
            };
        }
        if (!IsPermissionExist(permissions))
        {
            return new Result<User>()
            {
                Error = new("Invalid Permission ID.")
            };
        }
        _context.Add(user);
        _context.UsersPeremissions.AddRange(permissions);
        _context.SaveChanges();

        _context.Entry(user)
            .Reference(e => e.Role)
            .Load();
        _context.Entry(user)
            .Collection(e => e.Permissions)
            .Load();

        return new() { Data = user };
    }

    public Result DeleteById(string id)
    {
        var transaction = _context.Database.BeginTransaction();
        try
        {
            var deletedUser = _context.Users
                .Where(i => i.UserID == id)
                .ExecuteDelete();

            var deletedPermission = _context.UsersPeremissions
                .Where(i => i.UserID == id)
                .ExecuteDelete();

            if (deletedPermission + deletedUser == 0)
                throw new Exception("Invalid user ID");

            _context.SaveChanges();
            transaction.Commit();
            return new()
            {
                Message = $"""User id "{id}" have been delete"""
            };
        }
        catch 
        {
            transaction.Rollback();
            return new()
            {
                Error = new("Can not delete")
            };
        }
    }

    private bool IsPermissionExist(IList<UserPermission> permissions)
    {
        var permissionID = permissions.Select(i => i.PermissionID);
        var existedID = _context.Permissions
            .Where(i => permissionID.Contains(i.PermissionId))
            .Count();

        Console.WriteLine($"{permissionID.Count()}, {existedID}");

        return permissionID.Count() == existedID;
    }
}
