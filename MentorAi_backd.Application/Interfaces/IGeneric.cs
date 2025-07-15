using System.Linq.Expressions;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IGeneric <TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entities);

        //Update Range
        Task DeleteAsync(int id);
        void Delete(TEntity entity);
        IQueryable<TEntity> Query();
    }
}
