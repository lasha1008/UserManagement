using System.Linq.Expressions;
using UserManagement.DTO;

namespace UserManagement.Facade.Interfaces.Services;

public interface IUserService
{
    void Insert(User user, UserProfile userProfile);
    void Update(User user, UserProfile userProfile);
    void Update(UserProfile userProfile);
    void Update(User user);
    void Delete(User user);
    UserProfile GetById(int id);
    IEnumerable<UserProfile> Set();
    IEnumerable<UserProfile> Set(Expression<Func<UserProfile, bool>> predicate);
}
