using Data.Shared.Entities;

namespace Logic.Shared.Interfaces
{
    public interface ILogRepository: IDisposable
    {
        IUserDataClaimsAccessor ClaimsAccessor { get; }
        Task<IEnumerable<LogMessageEntity>> GetLogMessages(DateTime? from, DateTime? to);
        Task AddLogMessage(LogMessageEntity logMessage);
        Task DeleteMessage(int id);
        Task DeleteMessages(int[] ids);
    }
}
