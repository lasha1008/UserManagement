using Microsoft.EntityFrameworkCore;
using UserManagement.Facade.Interfaces.Repository;
using UserManagement.Repositories;

namespace UserManagement.Configurations;

public static class DependencyConfiguration
{
    public static void ConfigureDependency(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UserManagement")));
    }
}
