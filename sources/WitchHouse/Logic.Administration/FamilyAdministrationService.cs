using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Response;
using Logic.Administration.Interfaces;
using Logic.Shared;
using Logic.Shared.Interfaces;
using System.Data;

namespace Logic.Administration
{
    public class FamilyAdministrationService : IFamilyAdministrationService
    {
        private readonly IApplicationUnitOfWork<DatabaseContext> _unitOfWork;

        public FamilyAdministrationService(IApplicationUnitOfWork<DatabaseContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserDataExportModel>> GetFamilyUsers(Guid? familyId)
        {
            try
            {
                if (familyId == null || familyId == Guid.Empty)
                {
                    throw new Exception($"FamilyGuid could not be null!");
                }

                var entities = await _unitOfWork.AccountRepository.GetByAsync(acc => acc.FamilyGuid != null && acc.FamilyGuid == familyId);

                if (!entities.Any())
                {
                    throw new Exception($"Could not find users of [{familyId}]!");
                }

                var exportModels = new List<UserDataExportModel>();

                foreach (var entity in entities.ToList())
                {
                    var model = entity.ToUserDataExportModel();


                    var userRoles = await _unitOfWork.RoleRepository.GetByAsync(x => x.AccountGuid == entity.Id);

                    model.Roles = (from role in userRoles.ToList() select role.RoleType).ToList();

                    exportModels.Add(model);
                }

                return exportModels;
            }
            catch (Exception exception)
            {
                await _unitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = familyId,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAdministrationService),
                });

                await _unitOfWork.SaveChanges();

                return new List<UserDataExportModel>();
            }
        }

        public async Task<ResponseMessage<UserDataModel>> GetUserData(Guid accountGuid)
        {
            try
            {
                var account = await _unitOfWork.AccountRepository.GetFirstByIdAsync(accountGuid);

                if (account == null)
                {
                    await _unitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                    {
                        Message = $"Could not load userData for [{accountGuid}]"
                    });

                    return new ResponseMessage<UserDataModel>
                    {
                        Success = false,
                        MessageKey = "labelLoadUserDataFailed",
                        StatusCode = 200,
                        Data = null
                    };
                }

                var userRoles = await _unitOfWork.RoleRepository.GetByAsync(x => x.AccountGuid == accountGuid);

                return new ResponseMessage<UserDataModel>
                {
                    Success = true,
                    StatusCode = 200,
                    MessageKey = "",
                    Data = new UserDataModel
                    {
                        UserId = accountGuid,
                        FamilyGuid = account.FamilyGuid,
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        UserName = account.UserName,
                        Culture = account.Culture,
                        DateOfBirth = account.DateOfBirth,
                        IsActive = account.IsActive,
                        UserRoles = (from role in userRoles.ToList()
                                     select (int)role.RoleType).ToList(),

                    }
                };
            }
            catch (Exception exception)
            {
                await _unitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _unitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAdministrationService),
                });

                return new ResponseMessage<UserDataModel>
                {
                    Success = false,
                    MessageKey = "labelLoadUserDataFailed",
                    StatusCode = 200,
                    Data = null
                };
            }
        }
    }
}
