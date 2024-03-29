using System.ComponentModel.DataAnnotations;

namespace UserManagement.Server.Models.DtoRequest;

public class RequestAddUserDto
{
    [Required]
    public string ID { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Phone]
    public string? Phone { get; set; } 
    [Required]
    public string RoleID { get; set; } = string.Empty;
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public AddUserDtoPermission[] Permissions { get; set; } = [];

}

public class AddUserDtoPermission
{
    public string PermissionID { get; set; } = string.Empty;
    public bool IsReadable { get; set; }
    public bool IsWritable { get; set; }
    public bool IsDeletable { get; set; }
}
