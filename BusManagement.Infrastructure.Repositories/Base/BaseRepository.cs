using System.Linq.Expressions;
using BusManagement.Core.Repositories.Base;
using BusManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BusManagement.Infrastructure.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>().Find(id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public T? Find(Expression<Func<T?, bool>> criteria, string[]? includes = null)
    {
        IQueryable<T?> query = _context.Set<T>();

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query.SingleOrDefault(criteria);
    }

    public async Task<T?> FindAsync(Expression<Func<T?, bool>> criteria, string[]? includes = null)
    {
        IQueryable<T?> query = _context.Set<T>();

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.SingleOrDefaultAsync(criteria);
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[]? includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query.Where(criteria).ToList();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
    {
        return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
    }

    public IEnumerable<T> FindAll(
        Expression<Func<T, bool>> criteria,
        int? skip,
        int? take,
        Expression<Func<T, object>>? orderBy = null,
        string orderByDirection = "ASC"
    )
    {
        IQueryable<T> query = _context.Set<T>().Where(criteria);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (orderBy == null)
            return query.ToList();
        query =
            orderByDirection == "ASC" ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

        return query.ToList();
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        string[]? includes = null
    )
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.Where(criteria).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        int take,
        int skip
    )
    {
        return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        int? take,
        int? skip,
        Expression<Func<T, object>>? orderBy = null,
        string orderByDirection = "ASC"
    )
    {
        IQueryable<T> query = _context.Set<T>().Where(criteria);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (orderBy == null)
            return await query.ToListAsync();
        query =
            orderByDirection == "ASC" ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

        return await query.ToListAsync();
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        return entities;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    public virtual T Update(T entity, Guid id)
    {
        // Fetch the entity from the database using the provided id
        var existingEntity = GetById(id);

        if (existingEntity != null)
        {
            // Update the properties of the fetched entity with the properties of the provided entity
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            throw new Exception("Entity not found");
        }

        return existingEntity;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public void Attach(T entity)
    {
        _context.Set<T>().Attach(entity);
    }

    public void AttachRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AttachRange(entities);
    }

    public int Count()
    {
        return _context.Set<T>().Count();
    }

    public int Count(Expression<Func<T, bool>> criteria)
    {
        return _context.Set<T>().Count(criteria);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
    {
        return await _context.Set<T>().CountAsync(criteria);
    }
}
