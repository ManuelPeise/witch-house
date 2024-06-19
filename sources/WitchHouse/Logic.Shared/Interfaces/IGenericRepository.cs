using Data.Shared.Entities;
using System.Linq.Expressions;

namespace Logic.Shared.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : AEntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetFirstByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression);
        Task<Guid?> AddAsync(TEntity entity);
        Task Update(TEntity entity);
        void Delete(Guid id);
    }
}
