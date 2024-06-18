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
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IModuleConfigurationService _moduleConfigurationService;
        private bool disposedValue;

        public DataSyncHandler(IApplicationUnitOfWork applicationUnitOfWork, IModuleConfigurationService moduleConfigurationService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _moduleConfigurationService = moduleConfigurationService;
        }

        public async Task<DataSyncExportModel?> ExecuteSync(DataSyncImportModel importModel)
        {
            try
            {
                //var model = new DataSyncExportModel();

                //model.UserData = await GetUserData(importModel.UserId);
                //model.ModuleConfiguration = await GetModuleConfiguration(importModel.UserId, importModel.FamilyId, importModel.RoleId);
                //model.SchoolModulesSettings = await GetSchoolModuleSettings(importModel.UserId);
                
                return null;

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(DataSyncHandler),
                    CreatedBy = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<string>(UserIdentityClaims.UserName) ?? "System",
                    CreatedAt = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat)
                });

                return null;
            }
        }

        private async Task<UserDataSync> GetUserData(Guid userId)
        {
            var userEntity = await _applicationUnitOfWork.AccountRepository.GetFirstByIdAsync(userId);

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
            return await _moduleConfigurationService.LoadUserModuleConfiguration(new UserModuleRequestModel
            {
                UserGuid = userId,
                FamilyGuid = familyId,
                RoleId = roleId
            });

        }
        
        private async Task<List<UserModule>> GetSchoolModuleSettings(Guid userId)
        {
            return await _moduleConfigurationService.LoadSchoolModuleSettings(userId);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _applicationUnitOfWork.Dispose();
                    _moduleConfigurationService.Dispose();
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
