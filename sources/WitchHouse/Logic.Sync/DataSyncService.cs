using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Response;
using Data.Shared.Models.Sync;
using Data.Shared.Models.Sync.Database;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Logic.Sync.Interfaces;
using System.Net;

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
                    await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                    {
                        Message = $"Could not find sync entity for {syncModel.UserId}",
                        Stacktrace = "",
                        Trigger = nameof(DataSyncService),
                        TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat)
                    });

                    return new ResponseMessage<SqLiteDatabaseExport>
                    {
                        Success = false,
                        StatusCode = 200,
                        MessageKey = "labelSyncEntityNotFound",
                        Data = null
                    };

                }
                else
                {
                    if (syncEntity.LastSync <= syncModel.LastSync)
                    {
                        return new ResponseMessage<SqLiteDatabaseExport>
                        {
                            Success = false,
                            StatusCode = 200,
                            MessageKey = "labelNothinToSyncFound",
                            Data = null
                        };
                    }

                    var model = await LoadDataToSyncFromMysql(syncModel, syncEntity);

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

        private async Task<SqLiteDatabaseExport?> LoadDataToSyncFromMysql(SyncModel syncModel, DataSyncEntity syncEntity)
        {
            var currentSyncTimeStamp = DateTime.Now;

            var accountEntity = await _applicationUnitOfWork.AccountRepository.GetFirstOrDefault(x => x.Id == syncModel.UserId);

            if (accountEntity == null || accountEntity.CredentialGuid == null)
            {
                return null;
            }

            await LoadCredentials(accountEntity.CredentialGuid);

            var updatedModules = await _applicationUnitOfWork.ModuleRepository.GetByAsync(x => x.AccountGuid == syncModel.UserId);

            var modules = updatedModules.Where(x => !string.IsNullOrWhiteSpace(x.UpdatedAt) && DateTime.Parse(x.UpdatedAt) < syncModel.LastSync);

            return new SqLiteDatabaseExport
            {
                SyncTableModel = new SyncTableModel
                {
                    SyncId = syncEntity.Id.ToString(),
                    UserGuid = syncModel.UserId,
                    LastSync = currentSyncTimeStamp,
                    CreatedAt = syncEntity.CreatedAt,
                    CreatedBy = syncEntity.CreatedBy,
                    UpdatedAt = syncEntity.UpdatedAt,
                    UpdatedBy = syncEntity.UpdatedBy,
                },
                UserTableModel = LoadUserTableModel(accountEntity),
                CredentialTableModel = LoadCredentialsTableModel(accountEntity),
                ModuleTableModels = await LoadModuleTableData(accountEntity.Id, syncModel.LastSync),
            };
        }

        private async Task<List<ModuleTabelModel>> LoadModuleTableData(Guid? accountGuid, DateTime? lastSync)
        {
            var modules = await _applicationUnitOfWork.ModuleRepository.GetByAsync(x => x.AccountGuid == accountGuid);

            if (lastSync != null)
            {
                modules = modules.ToList().Where(x => DateTime.Parse(x.UpdatedAt) < lastSync);
            }

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

        private async Task LoadCredentials(Guid? credentialGuid)
        {
            if (credentialGuid == null)
            {
                return;
            }

            await _applicationUnitOfWork.CredentialsRepository.GetFirstByIdAsync((Guid)credentialGuid);
        }

        private UserTabelModel LoadUserTableModel(AccountEntity accountEntity)
        {
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

        private CredentialTableModel LoadCredentialsTableModel(AccountEntity? accountEntity)
        {
            return new CredentialTableModel
            {
                CredentialsId = accountEntity?.CredentialGuid.ToString(),
                MobilePin = accountEntity?.Credentials?.MobilePin ?? null,
                JwtToken = accountEntity?.Credentials?.JwtToken,
                RefreshToken = accountEntity?.Credentials?.RefreshToken,
                CreatedAt = accountEntity?.Credentials?.CreatedAt ?? "",
                CreatedBy = accountEntity?.Credentials?.CreatedBy ?? "",
                UpdatedAt = accountEntity?.Credentials?.UpdatedAt ?? "",
                UpdatedBy = accountEntity?.Credentials?.UpdatedBy ?? ""
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
