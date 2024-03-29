using System.ComponentModel.DataAnnotations;

namespace UserManagement.Server.Models.Entities;

public class User
{
    public string UserID { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    [Required]
    public string? RoleID { get; set; }
    public string Username { get; set; } = string.Empty;
    [Required]
    public string? Password { get; set; }

    public Role? Role { get; set; }
    public ICollection<UserPermission>? Permissions { get; set; }
}
