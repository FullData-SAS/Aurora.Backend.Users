using System.Linq.Expressions;
using Aurora.Backend.Users.Services.Contracts;
using Aurora.Backend.Users.Services.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Backend.Users.Services.Implements;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AuroraContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AuroraContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    
    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).FirstOrDefaultAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<bool> SaveAllAsync() => await _context.SaveChangesAsync() > 0;
}