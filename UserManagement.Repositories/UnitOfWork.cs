using Microsoft.EntityFrameworkCore.Storage;
using UserManagement.Facade.Interfaces.Repository;

namespace UserManagement.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly UserManagementDbContext _context;
    private readonly Lazy<IUserRepository> _userRepositoryLazy;
    private readonly Lazy<IUserProfileRepository> _userProfileRepositoryLazy;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(UserManagementDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userRepositoryLazy = new Lazy<IUserRepository>(() => new UserRepository(context));
        _userProfileRepositoryLazy = new Lazy<IUserProfileRepository>(() => new UserProfileRepository(context));
    }

    public IUserRepository UserRepository => _userRepositoryLazy.Value;

    public IUserProfileRepository UserProfileRepository => _userProfileRepositoryLazy.Value;

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();

    public void CommitTransaction()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _context.Dispose();
        _transaction?.Dispose();
    }
}
