using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Response;
using Data.Shared.Models.Sync;
using Data.Shared.Models.Sync.Database;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Logic.Sync.Interfaces;
using System.Globalization;
using System.Net;
using ZstdSharp;

namespace Logic.Sync
{
    public class DataSyncService : IDataSyncService
    {
        private bool disposedValue;

        private readonly IApplicationUnitOfWork<DatabaseContext> _applicationUnitOfWork;

        public DataSyncService(IApplicationUnitOfWork<DatabaseContext> applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task<ResponseMessage<SqLiteDatabaseExport>> ExexuteMobileSyncAsync(SyncModel syncModel)
        {
            try
            {
                var syncEntity = await _applicationUnitOfWork.SyncRepository.GetFirstOrDefault(x => x.UserGuid == syncModel.UserId);

                if (syncEntity == null)
                {
                    return await LogMessageOnFail($"Could not find sync entity for {syncModel.UserId}", "labelSyncEntityNotFound");
                }
                else
                {
                    if (syncEntity.LastSync <= syncModel.LastSync)
                    {
                        return await LogMessageOnFail($"Nothing to sync for {syncModel.UserId}", "labelNothinToSyncFound");
                    }

                    var model = await LoadDataUpdateData(syncModel, syncEntity);

                    return new ResponseMessage<SqLiteDatabaseExport>
                    {
                        Success = true,
                        StatusCode = 200,
                        MessageKey = "labelSyncSuccess",
                        Data = model
                    };
                }
            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(DataSyncService),
                });

                return new ResponseMessage<SqLiteDatabaseExport>
                {
                    Success = false,
                    StatusCode = 200,
                    MessageKey = "labelSyncError",
                    Data = null
                };
            }
        }

        private async Task<SqLiteDatabaseExport?> LoadDataUpdateData(SyncModel syncModel, DataSyncEntity syncEntity)
        {
            var databaseModified = false;
            var currentSyncTimeStamp = DateTime.Now;
            var exportModel = new SqLiteDatabaseExport();

            var accountEntity = await _applicationUnitOfWork.AccountRepository.GetFirstOrDefault(x => x.Id == syncModel.UserId);
            await LoadCredentials(accountEntity?.CredentialGuid);

            if (syncModel.Data == null)
            {
                return new SqLiteDatabaseExport
                {
                    SyncTableModel = null,
                    UserTableModel = LoadUserTableModel(accountEntity),
                    CredentialTableModel = LoadCredentialsTableModel(accountEntity?.Credentials),
                    ModuleTableModels = await LoadModulesFromDatabase(syncModel.UserId),
                };
            }

            if (accountEntity == null || accountEntity.CredentialGuid == null)
            {
                throw new Exception("Could not sync data!");
            }

            if (syncModel.Data != null && syncModel.Data?.UserTableModel != null)
            {
                accountEntity = TryGetUpdatedAccountEntity(syncModel.Data.UserTableModel, accountEntity);

                exportModel.UserTableModel = LoadUserTableModel(accountEntity);
                CredentialEntity? credentials = null;

                if (syncModel.Data?.CredentialTableModel != null)
                {
                    credentials = await TryUpdateCredentials(accountEntity.Credentials, syncModel.Data.CredentialTableModel);
                }

                if (credentials != null)
                {
                    exportModel.CredentialTableModel = LoadCredentialsTableModel(credentials);
                }

                await _applicationUnitOfWork.AccountRepository.Update(accountEntity);

                databaseModified = true;
            }

            if (syncModel.Data != null && syncModel.Data?.ModuleTableModels != null)
            {
                var moduleEntities = await _applicationUnitOfWork.ModuleRepository.GetByAsync(x => x.AccountGuid == syncModel.UserId);

                moduleEntities = await TryGetUpdateModules(moduleEntities.ToList(), syncModel.Data.ModuleTableModels);

                exportModel.ModuleTableModels = LoadModuleTableData(moduleEntities);

                databaseModified = true;
            }


            if(databaseModified)
            {
                await _applicationUnitOfWork.SaveChanges();
            }

            return exportModel;
        }

        private async Task<IEnumerable<ModuleEntity>> TryGetUpdateModules(List<ModuleEntity> moduleEntities, List<ModuleTabelModel> moduleTableModels)
        {
            var modules = new List<ModuleEntity>();

            foreach (var module in moduleTableModels)
            {
                if (string.IsNullOrWhiteSpace(module.ModuleId))
                {
                    continue;
                }

                var entity = moduleEntities.FirstOrDefault(x => x.Id == new Guid(module.ModuleId));

                if (entity != null)
                {
                    entity.SettingsJson = module.SettingsJson;
                    entity.IsActive = module.IsActive;

                    await _applicationUnitOfWork.ModuleRepository.Update(entity);

                    modules.Add(entity);
                }
            }

            return modules;
        }

        private async Task<CredentialEntity?> TryUpdateCredentials(CredentialEntity? credentials, CredentialTableModel credentialTableModel)
        {
            if (credentials == null)
            {
                return null;
            }

            var shouldUpdate = false;

            if (shouldUpdate && credentials != null)
            {
                if (DateTime.TryParseExact(credentials?.UpdatedAt, new[] { Constants.LogMessageDateFormat }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var updatedAt))
                {
                    shouldUpdate = updatedAt >= DateTime.Parse(credentialTableModel.UpdatedAt);

                    if (shouldUpdate)
                    {
                        credentials.MobilePin = credentialTableModel.MobilePin != null ? (int)credentialTableModel.MobilePin : credentials.MobilePin;
                        credentials.JwtToken = credentialTableModel.JwtToken;
                        credentials.RefreshToken = credentialTableModel.RefreshToken;

                        await _applicationUnitOfWork.CredentialsRepository.Update(credentials);

                        return credentials;
                    }
                }
            }

            return null;
        }

        private AccountEntity TryGetUpdatedAccountEntity(UserTabelModel userTableModel, AccountEntity accountEntity)
        {
            if (DateTime.Parse(accountEntity.UpdatedAt) >= DateTime.Parse(userTableModel.UpdatedAt))
            {
                return accountEntity;
            }

            accountEntity.FirstName = userTableModel.FirstName;
            accountEntity.LastName = userTableModel.LastName;
            accountEntity.UserName = userTableModel.UserName;
            accountEntity.DateOfBirth = userTableModel.DateOfBirth;
            accountEntity.Culture = userTableModel.Culture;
            accountEntity.IsActive = userTableModel.IsActive;

            return accountEntity;
        }

        private List<ModuleTabelModel> LoadModuleTableData(IEnumerable<ModuleEntity> modules)
        {
            return (from module in modules
                    select new ModuleTabelModel
                    {
                        ModuleId = module.Id.ToString(),
                        AccountGuid = module.AccountGuid.ToString(),
                        ModuleName = module.ModuleName,
                        ModuleSettingsType = module.ModuleSettingsType,
                        ModuleType = module.ModuleType,
                        SettingsJson = module.SettingsJson,
                        IsActive = module.IsActive,
                        CreatedBy = module.CreatedBy,
                        CreatedAt = module.CreatedAt,
                        UpdatedAt = module.UpdatedAt,
                        UpdatedBy = module.UpdatedBy,
                    }).ToList();
        }

        private async Task<List<ModuleTabelModel>> LoadModulesFromDatabase(Guid userId)
        {
            var moduleEntities = await _applicationUnitOfWork.ModuleRepository.GetByAsync(x => x.AccountGuid == userId);

            return LoadModuleTableData(moduleEntities.ToList());
        }
        
        private async Task LoadCredentials(Guid? credentialGuid)
        {
            if (credentialGuid == null)
            {
                return;
            }

            await _applicationUnitOfWork.CredentialsRepository.GetFirstByIdAsync((Guid)credentialGuid);
        }

        private UserTabelModel LoadUserTableModel(AccountEntity? accountEntity)
        {
            if (accountEntity == null)
            {
                throw new Exception("Data sync failed, accountentity could not be null!");
            }

            return new UserTabelModel
            {
                UserId = accountEntity.Id.ToString(),
                FamilyId = accountEntity?.FamilyGuid.ToString() ?? "",
                FirstName = accountEntity?.FirstName,
                LastName = accountEntity?.LastName,
                UserName = accountEntity?.UserName ?? "",
                DateOfBirth = accountEntity?.DateOfBirth,
                Culture = accountEntity?.Culture,
                ProfileImage = accountEntity?.ProfileImage,
                IsActive = accountEntity?.IsActive ?? false,
                CreatedAt = accountEntity?.CreatedAt ?? "",
                CreatedBy = accountEntity?.CreatedBy ?? "",
                UpdatedAt = accountEntity?.UpdatedAt ?? "",
                UpdatedBy = accountEntity?.UpdatedBy ?? ""
            };
        }

        private CredentialTableModel LoadCredentialsTableModel(CredentialEntity? credentials)
        {
            if (credentials == null)
            {
                throw new Exception("Data sync failed, credentials could not be null!");
            }

            return new CredentialTableModel
            {
                CredentialsId = credentials.Id.ToString(),
                MobilePin = credentials.MobilePin,
                JwtToken = credentials.JwtToken,
                RefreshToken = credentials.RefreshToken,
                CreatedAt = credentials.CreatedAt ?? "",
                CreatedBy = credentials.CreatedBy ?? "",
                UpdatedAt = credentials.UpdatedAt ?? "",
                UpdatedBy = credentials.UpdatedBy ?? ""
            };
        }

        private async Task<ResponseMessage<SqLiteDatabaseExport>> LogMessageOnFail(string msg, string messageKey)
        {
            await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
            {
                Message = msg,
                Stacktrace = "",
                Trigger = nameof(DataSyncService),
                TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat)
            });

            return new ResponseMessage<SqLiteDatabaseExport>
            {
                Success = false,
                StatusCode = 200,
                MessageKey = messageKey,
                Data = null
            };
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
