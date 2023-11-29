using Microsoft.EntityFrameworkCore;
using UserManagement.DTO;

namespace UserManagement.Repositories;

public class UserManagementDbContext : DbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserProfile>().HasIndex(up => up.PersonalNumber).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<UserProfile>().HasIndex(up => up.UserId).IsUnique();
    }
}