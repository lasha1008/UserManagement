using UserManagement.DTO;

namespace UserManagement.Facade.Interfaces.Services;

public interface IUserAccountService
{
    void Register(string username, string password, string email, UserProfile userProfile);
    bool Login(string username, string password);
    public void UpdatePassword(int userId, string Password);
    public void Unregister(int userId);
}
