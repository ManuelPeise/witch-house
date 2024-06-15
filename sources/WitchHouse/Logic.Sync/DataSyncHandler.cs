using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Sync;
using Logic.Shared;
using Logic.Shared.Interfaces;

namespace Logic.Sync
{
    public class DataSyncHandler: ISyncHandler
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleService;
        private bool disposedValue;

        public DataSyncHandler(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleService)
        {
            _accountUnitOfWork = accountUnitOfWork;
            _moduleService = moduleService;
        }

        public async Task<DataSyncExportModel?> ExecuteSync(DataSyncImportModel importModel)
        {
            try
            {
                var model = new DataSyncExportModel();

                model.UserData = await GetUserData(importModel.UserId);
                model.ModuleConfiguration = await GetModuleConfiguration(importModel.UserId, importModel.FamilyId, importModel.RoleId);
                model.SchoolModules = await GetSchoolModuleSettings(importModel.UserId);
                
                return model;

            }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(DataSyncHandler),
                    CreatedBy = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<string>(UserIdentityClaims.UserName) ?? "System",
                    CreatedAt = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat)
                });

                return null;
            }
        }

        private async Task<UserDataSync> GetUserData(Guid userId)
        {
            var userEntity = await _accountUnitOfWork.AccountRepository.GetFirstByIdAsync(userId);

            if (userEntity == null)
            {
                throw new Exception($"Could not sync userData, [REASON: entity for {userId} not found]!");
            }

            return new UserDataSync
            {
                UserGuid = userEntity.Id,
                FirstName = userEntity.FirstName ?? "",
                LastName = userEntity.LastName ?? "",
                UserName = userEntity.UserName,
                Birthday = userEntity.DateOfBirth,
            };
        }

        private async Task<ModuleConfiguration> GetModuleConfiguration(Guid userId, Guid familyId, UserRoleEnum roleId)
        {
            return await _moduleService.LoadUserModuleConfiguration(new UserModuleRequestModel
            {
                UserGuid = userId,
                FamilyGuid = familyId,
                RoleId = roleId
            });

        }
        
        private async Task<List<ModuleSettings>> GetSchoolModuleSettings(Guid userId)
        {
            return await _moduleService.LoadSchoolModuleSettings(userId);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _accountUnitOfWork.Dispose();
                  _moduleService.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
