using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Sync;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Newtonsoft.Json;

namespace Logic.Sync
{
    public class DataSyncHandler: ISyncHandler
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly ISettingsService _settingsService;
        private bool disposedValue;

        public DataSyncHandler(IAccountUnitOfWork accountUnitOfWork, ISettingsService settingsService)
        {
            _accountUnitOfWork = accountUnitOfWork;
            _settingsService = settingsService;
        }

        public async Task<DataSyncExportModel?> ExecuteSync(DataSyncImportModel importModel, CurrentUser currentUser)
        {
            try
            {
                // TODO add data to sync to import model [Statistic data]

                return await GetExportModel(importModel, currentUser);

            }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = currentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(DataSyncHandler),
                    CreatedBy = currentUser.UserName ?? "System",
                    CreatedAt = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat)
                });

                return null;
            }
        }

        public async Task<DataSyncExportModel?> GetExportModel(DataSyncImportModel importModel, CurrentUser currentUser)
        {
            try
            {
                var exportModel = new DataSyncExportModel();

                var userEntity = await _accountUnitOfWork.AccountRepository.GetFirstByIdAsync(importModel.UserId);

                if (userEntity == null)
                {
                    throw new Exception($"Could not sync userData, [REASON: entity for {importModel.UserId} not found]!");
                }

                exportModel.UserData = new UserDataSync
                {
                    UserGuid = userEntity.Id,
                    FirstName = userEntity.FirstName ?? "",
                    LastName = userEntity.LastName ?? "",
                    UserName = userEntity.UserName,
                    Birthday = userEntity.DateOfBirth,
                };

                var modules = new List<SchoolModuleSync>();
                var moduleEntities = await _accountUnitOfWork.UserModuleRepository.GetByAsync(x => x.UserId == importModel.UserId);

                if (moduleEntities == null)
                {
                    throw new Exception($"Could not sync module data, [REASON: no entities found for {importModel.UserId}]!");
                }

                foreach (var module in moduleEntities.ToList())
                {
                    if (module.ModuleType != ModuleTypeEnum.SchoolTraining)
                    {
                        continue;
                    }

                    var settings = await _settingsService.GetSettingsByUserId(new UserModule
                    {
                        UserId = importModel.UserId,
                        ModuleId = module.Id,
                        ModuleType = module.ModuleType,
                        IsActive = module.IsActive,
                    });

                    if (settings == null || settings.FirstOrDefault() == null)
                    {
                        continue;
                    }

                    foreach (var currentSetting in settings.ToList())
                    {
                        var model = new SchoolModuleSync
                        {
                            UserModule = new UserModule
                            {
                                UserId = module.Id,
                                ModuleId = module.ModuleId,
                                ModuleType = module.ModuleType,
                                IsActive = module.IsActive,
                            },
                            ModuleSettings = new ModuleSettings
                            {
                                UserId = module.UserId,
                                ModuleType = module.ModuleType,
                                ModuleSettingsType = currentSetting.SettingsType,
                                Settings = JsonConvert.DeserializeObject<SchoolSettings>(currentSetting.SettingsJson) ?? null
                            }
                        };

                        modules.Add(model);
                    }
                }

                exportModel.SchoolModules = modules;

                return exportModel;

            }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = currentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(DataSyncHandler),
                    CreatedBy = currentUser.UserName ?? "System",
                    CreatedAt = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat)
                });

                return null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _accountUnitOfWork.Dispose();
                    _settingsService.Dispose();
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
