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
        
        public LogRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddLogMessage(LogMessageEntity logMessage)
        {
            var table = _context.Set<LogMessageEntity>();
            await table.AddAsync(logMessage);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LogMessageEntity>> GetLogMessages(DateTime? from, DateTime? to)
        {
            var table = _context.Set<LogMessageEntity>();

            if (from == null || to == null)
            {
                return await Task.FromResult(table.Select(x => x));
            }

            var fromDate = from ?? default(DateTime?);
            var toDate = to ?? default(DateTime?);

            return await Task.FromResult(table.Where(msg => DateTime.Parse(msg.TimeStamp) >= from && DateTime.Parse(msg.TimeStamp) <= toDate));

        }

        public async Task<IEnumerable<LogMessageEntity>> GetLogMessages(Guid familyGuid)
        {
            var table = _context.Set<LogMessageEntity>();

            return await Task.FromResult(table.Where(msg => msg.FamilyGuid == familyGuid));

        }

        public async Task DeleteMessage(int id)
        {
            var table = _context.Set<LogMessageEntity>();

            var entityToDelete = await table.FirstOrDefaultAsync(x => x.Id == id);

            if (entityToDelete != null)
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
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
    }
}
