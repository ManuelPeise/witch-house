using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Newtonsoft.Json;

namespace Logic.Administration
{
    public class ModuleConfigurationService : IModuleConfigurationService
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly ISettingsService _settingsService;

        private bool disposedValue;

        public ModuleConfigurationService(IAccountUnitOfWork accountUnitOfWork, ISettingsService settingsService)
        {
            _accountUnitOfWork = accountUnitOfWork;
            _settingsService = settingsService;
        }

        public async Task CreateModules(Guid userId, bool isActive)
        {
            try
            {
                
                var modules = await _accountUnitOfWork.ModuleRepository.GetAllAsync();

                foreach (var module in modules)
                {
                    await CreateUserModules(module, userId, module.ModuleType, isActive);
                }
            }
            catch (Exception exception)
            {

                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });
            }
        }

        public async Task<List<UserModule>> GetUserModules(Guid userId, ModuleTypeEnum moduleType)
        {
            try
            {
                var modules = new List<UserModuleEntity>();

                var entities = await _accountUnitOfWork.UserModuleRepository.GetByAsync(x => x.UserId == userId && x.ModuleType == moduleType);

                return (from e in entities
                        select e.ToExportModel()).ToList();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public async Task<List<ModuleSettings>> LoadSchoolModuleSettings(Guid userId, bool? isActive = null)
        {
            var settings = new List<ModuleSettings>();
           
            try
            {
                var modules = await _accountUnitOfWork.UserModuleRepository.GetByAsync(x => x.UserId == userId);

                if(isActive != null)
                {
                    modules = modules.Where(x => x.IsActive == isActive);
                }

                foreach (var module in modules.ToList())
                {
                    var moduleSettings = await _settingsService.GetSettingsByUserId(module.ToExportModel());

                    foreach (var setting in moduleSettings)
                    {
                        settings.Add(new ModuleSettings
                        {
                            ModuleType = setting.ModuleType,
                            ModuleSettingsType = setting.SettingsType,
                            UserId = setting.UserId,
                            Settings = JsonConvert.DeserializeObject<SchoolSettings>(setting.SettingsJson) ?? null
                        });
                    }
                }

                return settings;

            }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });

                return settings;
            }
        }

        public async Task<ModuleConfiguration> LoadUserModuleConfiguration(
            UserModuleRequestModel requestModel)
        {
            var config = new ModuleConfiguration
            {
                UserGuid = requestModel.UserGuid,
                Modules = new List<UserModule>()
            };

            try
            {
                var modules = await _accountUnitOfWork.ModuleRepository.GetAllAsync();

                foreach (var module in modules)
                {
                    var entity = await GetUserModuleEntityAsync(module, requestModel, module.ModuleType);

                    if (entity != null)
                    {
                        config.Modules.Add(entity.ToExportModel());
                    }
                }

                return config;

            }
            catch (Exception exception)
            {

                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });

                return config;
            }
        }

        public async Task UpdateModule(UserModule module)
        {
            try
            {
                var modulesToUpdate = await _accountUnitOfWork.UserModuleRepository.GetByAsync(x => x.ModuleId == module.ModuleId && x.UserId == module.UserId);

                if (!modulesToUpdate.Any() || modulesToUpdate.Count() > 1)
                {
                    throw new Exception("Module is not defined or is defined multiple times");
                }

                var moduleToUpdate = modulesToUpdate.First();

                moduleToUpdate.IsActive = module.IsActive;

                await _accountUnitOfWork.UserModuleRepository.Update(moduleToUpdate);

                if (moduleToUpdate.ModuleType == ModuleTypeEnum.SchoolTraining)
                {
                    await _settingsService.CreateSchoolTrainingSettings(module);
                }

                await _accountUnitOfWork.SaveChanges();

            }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });
            }
        }

        public async Task UpdateSchoolModuleSettings(ModuleSettings settings)
        {
            try
            {
                var entity = await _settingsService.GetSettingsBy(settings.UserId, settings.ModuleSettingsType);

                if (entity != null)
                {
                    entity.SettingsJson = JsonConvert.SerializeObject(settings.Settings);

                    await _settingsService.UpdateSettings(entity);

                    await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                    {
                        FamilyGuid = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                        Message = $"Settings for [{settings.UserId} - {Enum.GetName(typeof(ModuleSettingsTypeEnum), settings.ModuleSettingsType)}] upadated with success!",
                        Stacktrace = "",
                        Trigger = nameof(ModuleConfigurationService),
                        TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                        CreatedAt = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                        CreatedBy = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<string>(UserIdentityClaims.UserName) ?? "System"
                    });
                }

            }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _accountUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });
            }
        }

        private async Task CreateUserModules(ModuleEntity module, Guid userId, ModuleTypeEnum moduleType, bool isActive)
        {
            try
            {
                var entities = await _accountUnitOfWork.UserModuleRepository.GetByAsync(x => x.UserId == userId && x.ModuleType == moduleType);

                if (!entities.Any())
                {
                    var moduleEntity = new UserModuleEntity
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        ModuleId = module.Id,
                        ModuleType = moduleType,
                        IsActive = isActive,
                    };


                    await _accountUnitOfWork.UserModuleRepository.AddAsync(moduleEntity);

                    await _accountUnitOfWork.SaveChanges();
                }

                if (entities.Any() && entities.Count() < 1)
                {
                    throw new Exception($"There are more than one User module settings defined for [{userId} - {moduleType}]");
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        private async Task<UserModuleEntity?> GetUserModuleEntityAsync(
            ModuleEntity module,
            UserModuleRequestModel requestModel,
            ModuleTypeEnum moduleType)
        {
            try
            {
                var entities = await _accountUnitOfWork.UserModuleRepository.GetByAsync(x => x.UserId == requestModel.UserGuid && x.ModuleType == moduleType);

                if (entities.Any() && entities.Count() < 1)
                {
                    throw new Exception($"There are more than one User module settings defined for [{requestModel.UserGuid} - {moduleType}]");
                }

                return entities.First();

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _accountUnitOfWork.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
