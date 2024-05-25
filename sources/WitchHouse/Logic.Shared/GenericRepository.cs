using Data.Database;
using Data.Shared.Entities;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Shared
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : AEntityBase
    {
        private readonly DatabaseContext _databaseContext;

        private bool disposedValue;

        public GenericRepository(DatabaseContext databaseContext)
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
