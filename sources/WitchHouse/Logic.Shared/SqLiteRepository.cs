using Data.Database;
using Data.Shared.Entities;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Logic.Shared
{
    public class SqLiteRepository<TEntity> : ISqLiteRepository<TEntity> where TEntity : AEntityBase
    {
        private readonly SqliteDbContext _databaseContext;
        private bool disposedValue;

        public SqLiteRepository(SqliteDbContext databaseContext)
        {
            _databaseContext = databaseContext;
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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var table = _databaseContext.Set<TEntity>();

            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> expression)
        {
            var table = _databaseContext.Set<TEntity>();

            return await Task.FromResult(table.Where(expression));
        }

        public async Task<TEntity?> GetFirstByIdAsync(Guid id)
        {
            var table = _databaseContext.Set<TEntity>();

            return await table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(TEntity entity)
        {
            var table = _databaseContext.Set<TEntity>();

            var existingEntity = table.Find(entity.Id);

            if (existingEntity != null)
            {
                existingEntity = entity;

                table.Update(existingEntity);
            }
        }

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
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
