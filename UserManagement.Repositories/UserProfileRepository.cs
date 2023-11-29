using UserManagement.DTO;
using UserManagement.Facade.Interfaces.Repository;

namespace UserManagement.Repositories;

internal sealed class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(UserManagementDbContext context) : base(context) { }
}
