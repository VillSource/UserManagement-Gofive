using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Server.Models.Entities;

public class Permission
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string PermissionId { get; set; } = string.Empty;

    [Required]
    public string PermissionName { get; set; } = string.Empty;

    public ICollection<UserPermission> Users { get; set; } = [];
}
