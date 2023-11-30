﻿using UserManagement.DTO;
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

    public void Register(User user, UserProfile userProfile)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (userProfile == null) throw new ArgumentNullException(nameof(userProfile));

        user.Password = user.Password.GetHash();

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

    public void Unregister(int userId)
    {
        User user = _unitOfWork.UserRepository.Set(x => x.UserId == userId).Single();

        _userService.Delete(user);
    }
}
