using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Logic.Shared;
using Logic.Shared.Interfaces;
using System.Data;

namespace Logic.Administration
{
    public class FamilyAdministration : LogicBase
    {
        private readonly ILogRepository _logReopsitory;

        public FamilyAdministration(DatabaseContext context, CurrentUser currentUser) : base(context, currentUser)
        {
            _logReopsitory = new LogRepository(context);
        }

        public async Task<List<UserDataExportModel>> GetFamilyUsers(Guid? familyId)
        {
            try
            {
                if (familyId == null || familyId == Guid.Empty)
                {
                    throw new Exception($"FamilyGuid could not be null!");
                }

                using (var unitOfWork = new AccountUnitOfWork(DatabaseContext))
                {
                    var entities = await unitOfWork.AccountRepository.GetByAsync(acc => acc.FamilyGuid != null && acc.FamilyGuid == familyId);

                    if (!entities.Any())
                    {
                        throw new Exception($"Could not find users of [{familyId}]!");
                    }


                    var result = (from e in entities
                                  select e.ToUserDataExportModel()).ToList();

                    return result;
                }

            }
            catch (Exception exception)
            {
                await _logReopsitory.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = familyId,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAdministration),

                });

                await SaveChanges();

                return new List<UserDataExportModel>();
            }
        }

    }
}
