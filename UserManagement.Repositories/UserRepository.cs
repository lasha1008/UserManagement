using UserManagement.DTO;
using UserManagement.Facade.Interfaces.Repository;

namespace UserManagement.Repositories;

internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(UserManagementDbContext context) : base(context) { }
}
