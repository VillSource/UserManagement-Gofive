using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Data;

public class UserManagementContext(DbContextOptions<UserManagementContext> op) : DbContext(op)
{
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPermission> UsersPeremissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(e => e.Role)
            .WithMany(e=>e.Users)
            .HasForeignKey(e=>e.RoleID);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Permissions)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserID);

        modelBuilder.Entity<Permission>()
            .HasMany( e => e.Users)
            .WithOne( e=> e.Permission)
            .HasForeignKey(e => e.PermissionID);

    }
}
