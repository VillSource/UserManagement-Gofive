using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Server.Models.Entities;

public class Role
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string RoleId { get; set; } = string.Empty;

    [Required]
    public string RoleName { get; set; } = string.Empty;
}
