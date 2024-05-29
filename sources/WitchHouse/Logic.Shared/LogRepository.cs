using Data.Database;
using Data.Shared.Entities;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.Shared
{
    public class LogRepository : ILogRepository
    {
        private bool disposedValue;
        private readonly DatabaseContext _context;
        private DbSet<LogMessageEntity>? _logTable;

        public LogRepository(DatabaseContext context)
        {
            _context = context;
            _logTable = _context.Set<LogMessageEntity>();
        }

        public async Task AddLogMessage(LogMessageEntity logMessage)
        {
            if (_logTable != null)
            {
                await _logTable.AddAsync(logMessage);

                await _context.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<LogMessageEntity>> GetLogMessages(DateTime? from, DateTime? to)
        {
            if (_logTable != null)
            {
                if (from == null || to == null)
                {
                    return await Task.FromResult(_logTable.Select(x => x));
                }

                var fromDate = from ?? default(DateTime?);
                var toDate = to ?? default(DateTime?);

                return await Task.FromResult(_logTable.Where(msg => DateTime.Parse(msg.TimeStamp) >= from && DateTime.Parse(msg.TimeStamp) <= toDate));
            }

            return new List<LogMessageEntity>();
        }

        public async Task DeleteMessage(int id)
        {
            var table = _context.Set<LogMessageEntity>();

            var entityToDelete = await table.FirstOrDefaultAsync(x => x.Id == id);

            if(entityToDelete != null)
            {
                table.Remove(entityToDelete);

            }
        }

        public async Task DeleteMessages(int[] ids)
        {
            var table = _context.Set<LogMessageEntity>();

            var entitiesToDelete = table.Where(x => ids.Contains(x.Id));

            if (entitiesToDelete.Any())
            {
                table.RemoveRange(entitiesToDelete);
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #region dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _logTable = null;
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
