using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Logic.Shared;
using Logic.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Administration
{
    public class LogService
    {
        private readonly ILogRepository _logRepository;
        private readonly CurrentUser _currentUser;
        public LogService(ILogRepository logRepository, CurrentUser currentUser)
        {
            _logRepository = logRepository;
            _currentUser = currentUser;
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
                    FamilyGuid = _currentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });

                await _logRepository.SaveChanges();

                return new List<LogMessageEntity>();

            }
        }

        public async Task DeleteLogmessages(int[] ids)
        {
            try
            {
                await _logRepository.DeleteMessages(ids);

                await _logRepository.SaveChanges();

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _currentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });

                await _logRepository.SaveChanges();
            }
        }
    }
}
