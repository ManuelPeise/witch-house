using Data.Shared.Entities;

namespace Logic.Shared.Interfaces
{
    public interface ILogRepository: IDisposable
    {
        Task<IEnumerable<LogMessageEntity>> GetLogMessages(DateTime? from, DateTime? to);
        Task<IEnumerable<LogMessageEntity>> GetLogMessages(Guid familyGuid);
        Task AddLogMessage(LogMessageEntity logMessage);
        Task DeleteMessage(int id);
        Task DeleteMessages(int[] ids);
    }
}
