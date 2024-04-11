namespace BusManagement.Core.Repositories.Base;

public interface IUnitOfWork
{
    int Complete();
    Task<int> CompleteAsync();
    void Dispose();
}
