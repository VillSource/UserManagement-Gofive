
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UserManagement.Server.Common.Repositories;
using UserManagement.Server.Data;
using UserManagement.Server.Repositories;

namespace UserManagement.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(o=>o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Config
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);


            // Add DataContext
            builder.Services.AddDbContext<UserManagementContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddHostedService<UserManagementInitializer>();

            // Add Database Exception page
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            // Add Services
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
