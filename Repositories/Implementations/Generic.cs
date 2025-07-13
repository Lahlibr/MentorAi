using MentorAi_backd.Data;
using MentorAi_backd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MentorAi_backd.Repositories.Implementations
{
    public class Generic<TEntity> : IGeneric<TEntity>
    where TEntity : class
    {
        protected readonly MentorAiDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Generic(MentorAiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entities)
        {
            _dbSet.Update(entities);
             await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChangesAsync();
        }
        // Used to build build advanced LINQ queries (.Include(), .OrderBy(), etc.) outside the repository.
        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }
    }
}
