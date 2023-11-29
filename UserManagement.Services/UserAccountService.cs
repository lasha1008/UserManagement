using UserManagement.DTO;
using UserManagement.Facade.HelperExtentions;
using UserManagement.Facade.Interfaces.Repository;
using UserManagement.Facade.Interfaces.Services;

namespace UserManagement.Services;

public class UserAccountService : IUserAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public UserAccountService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public void Register(string username, string password, string email, UserProfile userProfile)
    {
        if (username == null) throw new ArgumentNullException(nameof(username));
        if (password == null) throw new ArgumentNullException(nameof(password));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (userProfile == null) throw new ArgumentNullException(nameof(userProfile));

        User user = new User()
        {
            Username = username,
            Password = password.GetHash(),
            Email = email,
        };

        _userService.Insert(user, userProfile);
    }

    public bool Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username)) throw new ArgumentException($"{nameof(username)} cannot be null or empty.", nameof(username));
        if (string.IsNullOrEmpty(password)) throw new ArgumentException($"{nameof(password)} cannot be null or empty.", nameof(password));

        User? user = _unitOfWork.UserRepository
            .Set(x => x.Username == username &&
                      x.Password == password.GetHash() &&
                      x.IsActive)
            .SingleOrDefault();

        if (user != null) return true;

        return false;
    }

    public void UpdatePassword(int userId, string Password)
    {
        if (Password == null) throw new ArgumentNullException(nameof(Password));

        User user = _unitOfWork.UserRepository
            .Set(x => x.UserId == userId &&
                      x.IsActive)
            .Single();

        user.Password = Password.GetHash();
        _userService.Update(user);
    }

    public void Unregister(int userId) => _userService.Delete(_userService.GetById(userId).User);

}
