using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Infrastructure.Persistance.Data;
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
           
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
           
        }
        public void UpdateAsync(TEntity entities)
        {
            _dbSet.Update(entities);
            
        }
        
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
           
        }

        // Used to build build advanced LINQ queries (.Include(), .OrderBy(), etc.) outside the repository.
        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }
    }
}
