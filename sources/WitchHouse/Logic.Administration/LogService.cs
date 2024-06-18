using Data.Database;
using Data.Shared.Entities;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.Administration
{
    public class LogService 
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        
        public LogService(IApplicationUnitOfWork applicationUnitOfWork) 
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task<List<LogMessageEntity>> LoadLogMessages(Guid? familiGuid)
        {
            try
            {
                IEnumerable<LogMessageEntity>? messages = null;

                if (familiGuid == null) {

                    messages = await _applicationUnitOfWork.LogRepository.GetLogMessages(null, null);

                    return messages.ToList();
                }

                messages = await _applicationUnitOfWork.LogRepository.GetLogMessages((Guid)familiGuid);

                return messages.ToList();
            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });

                return new List<LogMessageEntity>();

            }
        }

        public async Task DeleteLogmessages(int[] ids)
        {
            try
            {
                await _applicationUnitOfWork.LogRepository.DeleteMessages(ids);

                await _applicationUnitOfWork.SaveChanges();

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });
            }
        }
    }
}
