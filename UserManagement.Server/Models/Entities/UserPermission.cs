using System.ComponentModel.DataAnnotations;

namespace UserManagement.Server.Models.Entities;

public class UserPermission
{
    [Key]
    public int ID { get; set; } 
    public string UserID { get; set; } = string.Empty;
    public string PermissionID { get; set; } = string.Empty;

    public bool IsReadable { get; set; }
    public bool IsWriteable { get; set; }
    public bool IsDeletable { get; set; }

    public User? User { get; set; }
    public Permission? Permission { get; set; }
}
