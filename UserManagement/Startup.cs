using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories;

namespace UserManagement;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("UserManagement")));
    }
}
