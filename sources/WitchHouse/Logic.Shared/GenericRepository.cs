using Data.Shared.Entities;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Logic.Shared
{
    public class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity> where TContext: DbContext where TEntity : AEntityBase
    {
        private readonly TContext _databaseContext;

        private bool disposedValue;

        public GenericRepository(TContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var table = _databaseContext.Set<TEntity>();

            return await table.ToListAsync();
        }

        public async Task<TEntity?> GetFirstByIdAsync(Guid id)
        {
            var table = _databaseContext.Set<TEntity>();

            return await table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression)
        {
            var table = _databaseContext.Set<TEntity>();

            return await Task.FromResult(table.Where(expression));
        }

        public async Task<Guid?> AddAsync(TEntity entity)
        {
            var table = _databaseContext.Set<TEntity>();

            var existing = await table.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existing == null)
            {
                var result = await table.AddAsync(entity);

                return await Task.FromResult(entity.Id);
            }

            return null;
        }

        public async Task Update(TEntity entity)
        {
            var table = _databaseContext.Set<TEntity>();

            var existingEntity = table.Find(entity.Id);

            if (existingEntity != null)
            {
                existingEntity = entity;

                table.Update(existingEntity);
            }
        }


        public void Delete(Guid id)
        {
            var table = _databaseContext.Set<TEntity>();

            var entityToDelete = table.FirstOrDefault(x => x.Id == id);

            if(entityToDelete != null)
            {
                table.Remove(entityToDelete);
            }
        }
        #region dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _databaseContext.Dispose();
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
