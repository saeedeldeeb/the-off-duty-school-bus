using BusManagement.Core.Repositories.Base;
using BusManagement.Infrastructure.Context;

namespace BusManagement.Infrastructure.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public Task<int> CompleteAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
