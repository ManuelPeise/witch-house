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
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        private bool disposedValue;

        public ModuleConfigurationService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task<List<UserModule>> GetUserModules(Guid accountGuid, ModuleTypeEnum moduleType)
        {
            try
            {
                var modules = await _applicationUnitOfWork.ModuleRepository.GetByAsync(
                    x => x.AccountGuid == accountGuid && x.ModuleType == moduleType);

                return (from module in modules
                        select new UserModule
                        {
                            ModuleId = module.Id,
                            UserId = accountGuid,
                            ModuleType = module.ModuleType,
                            ModuleSettings = module.SettingsJson,
                            IsActive = module.IsActive,

                        }).ToList();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public async Task<List<UserModule>> LoadSchoolModuleSettings(Guid accountGuid)
        {
            try
            {
                return await GetUserModules(accountGuid, ModuleTypeEnum.SchoolTraining);

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });

                return new List<UserModule>();
            }
        }

        public async Task<ModuleConfiguration> LoadUserModuleConfiguration(
            UserModuleRequestModel requestModel)
        {
            try
            {
                var modules = await _applicationUnitOfWork.ModuleRepository.GetByAsync(
                   x => x.AccountGuid == requestModel.UserGuid);

                var config = new ModuleConfiguration
                {
                    UserGuid = requestModel.UserGuid,
                    Modules = (from module in modules
                               select new UserModule
                               {
                                   ModuleId = module.Id,
                                   UserId = requestModel.UserGuid,
                                   ModuleType = module.ModuleType,
                                   ModuleSettings = module.SettingsJson,
                                   IsActive = module.IsActive,

                               }).ToList()
                };

                return config;

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });

                return new ModuleConfiguration
                {
                    UserGuid = requestModel.UserGuid,
                    Modules = new List<UserModule>()
                };
            }
        }

        public async Task UpdateModule(UserModule module)
        {
            try
            {
                var moduleToUpdate = await _applicationUnitOfWork.ModuleRepository.GetFirstByIdAsync(module.ModuleId);

                if (moduleToUpdate == null)
                {
                    throw new Exception($"Could not update userModule!");
                }

                moduleToUpdate.IsActive = module.IsActive;
                moduleToUpdate.SettingsJson = module.ModuleSettings;

                await _applicationUnitOfWork.ModuleRepository.Update(moduleToUpdate);

                await _applicationUnitOfWork.SaveChanges();
            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
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
                var moduleToUpdate = await _applicationUnitOfWork.ModuleRepository.GetFirstByIdAsync(settings.ModuleGuid);

                if (moduleToUpdate == null)
                {
                    throw new Exception("Module to update not found!");
                }

                moduleToUpdate.SettingsJson = JsonConvert.SerializeObject(settings.Settings);

                await _applicationUnitOfWork.ModuleRepository.Update(moduleToUpdate);

                await _applicationUnitOfWork.SaveChanges();

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(ModuleConfigurationService),
                });
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _applicationUnitOfWork.Dispose();
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
