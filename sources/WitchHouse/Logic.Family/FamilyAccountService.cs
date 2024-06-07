using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Newtonsoft.Json;

namespace Logic.Family
{
    public class FamilyAccountService : LogicBase
    {
        private readonly ILogRepository _logRepository;
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleService;
        private readonly ISettingsService _settingsService;
        private readonly CurrentUser _currentUser;

        public FamilyAccountService(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleService, CurrentUser currentUser) : base()
        {
            _accountUnitOfWork = accountUnitOfWork;
            _logRepository = new LogRepository(accountUnitOfWork.DatabaseContext);
            _settingsService = new SettingsService(accountUnitOfWork.DatabaseContext);
            _moduleService = moduleService;
            _currentUser = currentUser;
        }

        public async Task<bool> CreateFamilyAccount(AccountImportModel accountImportModel)
        {
            try
            {
                var familyGuid = Guid.NewGuid();

                var familyEntity = accountImportModel.Family?.ToEntity(familyGuid);

                if (familyEntity == null)
                {
                    throw new Exception("Family entity could not be null!");
                }

                var result = await _accountUnitOfWork.FamilyRepository.AddAsync(familyEntity);

                if (result == null)
                {
                    throw new Exception("Cou´ld not add family to database!");
                }

                var accountGuid = Guid.NewGuid();
                var salt = Guid.NewGuid().ToString();

                var accountEntity = accountImportModel.UserAccount.ToEntity(
                    accountGuid,
                    familyGuid,
                    salt,
                    Helpers.GetEncodedSecret(accountImportModel.UserAccount.Secret, salt),
                    familyEntity.City,
                    UserRoleEnum.LocalAdmin);

                if (accountEntity == null)
                {
                    throw new Exception("Account entity could not be null!");
                }

                result = await _accountUnitOfWork.AccountRepository.AddAsync(accountEntity);

                if (result != null)
                {
                    await _moduleService.CreateModules(_currentUser, accountGuid, true);

                    var modules = await _moduleService.GetUserModules(accountGuid, ModuleTypeEnum.SchoolTraining);

                    foreach (var module in modules)
                    {
                        await _settingsService.CreateSchoolTrainingSettings(module);
                    }

                    await _accountUnitOfWork.SaveChanges(_currentUser);

                    return true;
                }

                return false;



            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(FamilyAccountService),
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm")
                });

                return false;
            }
        }

        public async Task AssignAccountToFamily(FamilyMemberImportModel accountImportModel)
        {
            try
            {
                if (!Guid.TryParse(accountImportModel.FamilyGuid, out var familyGuid))
                {
                    return;
                }

                var familyEntity = await _accountUnitOfWork.FamilyRepository.GetFirstByIdAsync(familyGuid);

                if (familyEntity == null)
                {
                    throw new Exception("Family entity could not be null!");
                }

                var salt = Guid.NewGuid().ToString();
                var accountGuid = Guid.NewGuid();
                var accountEntity = new AccountEntity
                {
                    Id = accountGuid,
                    FamilyGuid = familyGuid,
                    FirstName = accountImportModel.FirstName,
                    LastName = accountImportModel.LastName,
                    UserName = accountImportModel.UserName,
                    Role = UserRoleEnum.User,
                    Culture = "en",
                    Secret = Helpers.GetEncodedSecret("P@ssword", salt),
                    Salt = salt
                };

                var result = await _accountUnitOfWork.AccountRepository.AddAsync(accountEntity);

                if (result != null)
                {
                    await _moduleService.CreateModules(_currentUser, accountGuid, false);

                    await _accountUnitOfWork.SaveChanges();

                    return;
                }

                return;

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(FamilyAccountService),
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm")
                });

                return;
            }
        }

        public async Task<ProfileExportModel?> GetProfile(string accountId)
        {
            try
            {
                if (Guid.TryParse(accountId, out var accountGuid))
                {
                    var accountEntity = await _accountUnitOfWork.AccountRepository.GetFirstByIdAsync(accountGuid);

                    if (accountEntity == null)
                    {
                        throw new Exception("Account entity could not be null!");
                    }

                    var model = accountEntity.ToExportModel();

                    return model;
                }

                return null;

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm"),
                    Trigger = nameof(FamilyAccountService),
                });

                return null;
            }
        }

        public async Task UpdateProfile(ProfileImportModel importModel)
        {
            try
            {
                var entity = await _accountUnitOfWork.AccountRepository.GetFirstByIdAsync(importModel.UserId);

                if (entity != null)
                {
                    entity.FirstName = importModel.FirstName;
                    entity.LastName = importModel.LastName;
                    entity.DateOfBirth = importModel.DateOfBirth ?? "";
                    entity.Culture = importModel.Culture;

                    await _accountUnitOfWork.AccountRepository.Update(entity);

                    await _accountUnitOfWork.SaveChanges();
                }
            }

            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy.MM.dd"),
                    Trigger = nameof(FamilyAccountService),
                });
            }
        }

        public async Task<bool> CheckUserName(string userName)
        {
            try
            {
                var allAccounts = await _accountUnitOfWork.AccountRepository.GetAllAsync();

                if (allAccounts.Any())
                {
                    var selectedUserName = allAccounts.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());

                    return selectedUserName == null;
                }

                return true;
            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(FamilyAccountService),
                    TimeStamp = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm")
                });

                return false;
            }
        }

        public async Task<bool> CheckPassword(PasswordUpdateModel model)
        {
            try
            {
                var entity = await _accountUnitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                if (entity == null)
                {
                    throw new Exception($"Could not check password of user: [{model.UserId}]!");
                }

                var securedPassword = Helpers.GetEncodedSecret(model.Password, entity.Salt);

                return securedPassword == entity.Secret;

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Trigger = nameof(FamilyAccountService),
                });

                return false;
            }
        }

        public async Task<bool> UpdatePassword(PasswordUpdateModel model)
        {
            try
            {
                var entity = await _accountUnitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                if (entity == null)
                {
                    throw new Exception($"Could not update password of user: [{model.UserId}]!");
                }

                var salt = Guid.NewGuid().ToString();
                var securedPassword = Helpers.GetEncodedSecret(model.Password, salt);

                entity.Salt = salt;
                entity.Secret = securedPassword;

                await _accountUnitOfWork.AccountRepository.Update(entity);
                await _accountUnitOfWork.SaveChanges();

                return true;

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Trigger = nameof(FamilyAccountService),
                });

                return false;
            }
        }

        public async Task UpdateUser(UserUpdateImportModel model)
        {
            try
            {
                var account = await _accountUnitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                if (account == null)
                {
                    throw new Exception($"Could not update user: [{model.UserId}]");
                }

                account.IsActive = model.IsActive;
                account.Role = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), model.Role.ToString());

                await _accountUnitOfWork.AccountRepository.Update(account);

                await _accountUnitOfWork.SaveChanges();

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAccountService),
                });
            }
        }


    }
}
