using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Logic.Shared;
using Logic.Shared.Interfaces;

namespace Logic.Administration
{
    public class LogService : LogicBase
    {
        private readonly ILogRepository _logRepository;
        public LogService(DatabaseContext context, ILogRepository logRepository, CurrentUser currentUser) : base(context, currentUser)
        {
            _logRepository = logRepository;
        }

        public async Task<List<LogMessageEntity>> LoadLogMessages()
        {
            try
            {
                var messages = await _logRepository.GetLogMessages(null, null);

                return messages.ToList();
            }
            catch (Exception exception)
            {

                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = base.CurrentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });

                await SaveChanges();

                return new List<LogMessageEntity>();

            }
        }

        public async Task DeleteLogmessages(int[] ids)
        {
            try
            {
                await _logRepository.DeleteMessages(ids);

                await SaveChanges();

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = CurrentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });

                await SaveChanges();
            }
        }
    }
}
