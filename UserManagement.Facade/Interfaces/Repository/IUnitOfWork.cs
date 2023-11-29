namespace UserManagement.Facade.Interfaces.Repository;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IUserProfileRepository UserProfileRepository { get; }

    int SaveChanges();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
