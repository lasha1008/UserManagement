using UserManagement.DTO;

namespace UserManagement.Facade.Interfaces.Services;

public interface IUserAccountService
{
    void Register(User user, UserProfile userProfile);
    bool Login(string username, string password);
    public void UpdatePassword(int userId, string Password);
    public void Unregister(int userId);
}
