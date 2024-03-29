using UserManagement.Server.Models.Entities;

namespace UserManagement.Server.Data;

public class UserManagementInitializer(IServiceProvider serviceProvider) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public List<Permission> Permissions =>
    [
        new() { PermissionId= "P-SAM",  PermissionName="Supper Admin"},
        new() { PermissionId= "P-ADM", PermissionName="Admin"},
        new() { PermissionId= "P-EMP",PermissionName="Employee"},
    ];

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserManagementContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        _ = InitPermission(context);

    }

    private Task InitPermission(UserManagementContext context)
    {
        foreach (var permission in Permissions)
        {
            var isNotExist = !context.Permissions.Any(i => i.PermissionId == permission.PermissionId);
            if (isNotExist)
            {
                context.Permissions.Add(permission);
                continue;
            }

            context.Permissions.Update(permission);
        }
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
