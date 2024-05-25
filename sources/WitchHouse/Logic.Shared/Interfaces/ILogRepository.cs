using Data.Shared.Entities;

namespace Logic.Shared.Interfaces
{
    public interface ILogRepository : IDisposable
    {
        Task<IEnumerable<LogMessageEntity>> GetLogMessages(DateTime? from, DateTime? to);
        Task AddLogMessage(LogMessageEntity logMessage);

        Task SaveChanges();
    }
}
