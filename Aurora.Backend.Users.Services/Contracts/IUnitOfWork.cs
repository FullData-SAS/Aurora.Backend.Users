namespace Aurora.Backend.Users.Services.Contracts;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task<bool> CommitAsync();
}