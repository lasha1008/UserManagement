using System.Linq.Expressions;
using UserManagement.DTO;
using UserManagement.Facade.Interfaces.Repository;
using UserManagement.Facade.Interfaces.Services;

namespace UserManagement.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Insert(User user, UserProfile userProfile)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.SaveChanges();

            userProfile.UserId = user.UserId;

            _unitOfWork.UserProfileRepository.Insert(userProfile);
            _unitOfWork.SaveChanges();



            _unitOfWork.CommitTransaction();
        }
        catch (Exception ex)
        {
            _unitOfWork.RollbackTransaction();
            throw new Exception(ex.Message);
        }
    }

    public void Update(User user, UserProfile userProfile)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            Update(userProfile);
            Update(user);

            _unitOfWork.CommitTransaction();
        }
        catch (Exception ex)
        {
            _unitOfWork.RollbackTransaction();
            throw new Exception(ex.Message);
        }
    }

    public void Update(UserProfile userProfile)
    {
        _unitOfWork.UserProfileRepository.Update(userProfile);
        _unitOfWork.SaveChanges();
    }

    public void Update(User user)
    {
        _unitOfWork.UserRepository.Update(user);
        _unitOfWork.SaveChanges();
    }

    public void Delete(User user)
    {
        user.IsActive = false;
        _unitOfWork.UserRepository.Update(user);

        _unitOfWork.SaveChanges();
    }

    public IEnumerable<UserProfile> Set(Expression<Func<UserProfile, bool>> predicate) => _unitOfWork.UserProfileRepository.Set();

    public IEnumerable<UserProfile> Set() => _unitOfWork.UserProfileRepository.Set();

    public UserProfile GetById(int id) => _unitOfWork
        .UserProfileRepository
        .Set(x => x.User.UserId == id && x.User.IsActive)
        .Single();

    public IEnumerable<UserProfile?> Search(string text)
    {
        var userProfileQuery = _unitOfWork
            .UserProfileRepository
            .Set(x => x.User.IsActive &&
                      (x.FirstName.Contains(text) || x.LastName.Contains(text)));

        var userQuery = _unitOfWork
            .UserRepository
            .Set(x => x.IsActive &&
            (x.Username.Contains(text) || x.Email.Contains(text)))
            .Select(x => x.UserProfile);

        var result = userProfileQuery.Union(userQuery);

        return result;
    }
}