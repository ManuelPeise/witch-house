using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared;
using Logic.Shared.Interfaces;

namespace Logic.Family
{
    public class FamilyAccountService : LogicBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogRepository _logRepository;

        public FamilyAccountService(DatabaseContext databaseContext, CurrentUser currentUser) : base(databaseContext, currentUser)
        {
            _databaseContext = databaseContext;
            _logRepository = new LogRepository(databaseContext);
        }

        public async Task<bool> CreateFamilyAccount(AccountImportModel accountImportModel)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    var familyGuid = Guid.NewGuid();

                    var familyEntity = accountImportModel.Family?.ToEntity(familyGuid);

                    if (familyEntity == null)
                    {
                        throw new Exception("Family entity could not be null!");
                    }

                    var result = await unitOfWork.FamilyRepository.AddAsync(familyEntity);

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

                    result = await unitOfWork.AccountRepository.AddAsync(accountEntity);

                    if (result != null)
                    {
                        await SaveChanges();

                        return true;
                    }

                    return false;
                }


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

                await SaveChanges();

                return false;
            }
        }

        public async Task AssignAccountToFamily(FamilyMemberImportModel accountImportModel)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    if (!Guid.TryParse(accountImportModel.FamilyGuid, out var familyGuid))
                    {
                        return;
                    }

                    var familyEntity = await unitOfWork.FamilyRepository.GetFirstByIdAsync(familyGuid);

                    if (familyEntity == null)
                    {
                        throw new Exception("Family entity could not be null!");
                    }

                    var salt = Guid.NewGuid().ToString();

                    var accountEntity = new AccountEntity
                    {
                        Id = Guid.NewGuid(),
                        FamilyGuid = familyGuid,
                        FirstName = accountImportModel.FirstName,
                        LastName = accountImportModel.LastName,
                        UserName = accountImportModel.UserName,
                        Role = UserRoleEnum.User,
                        Culture = "en",
                    };

                    var result = await unitOfWork.AccountRepository.AddAsync(accountEntity);

                    if (result != null)
                    {
                        await SaveChanges();

                        return;
                    }

                    return;
                }
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

                await SaveChanges();

                return;
            }
        }

        public async Task<ProfileExportModel?> GetProfile(string accountId)
        {
            try
            {
                if (Guid.TryParse(accountId, out var accountGuid))
                {
                    using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                    {
                        var accountEntity = await unitOfWork.AccountRepository.GetFirstByIdAsync(accountGuid);

                        if (accountEntity == null)
                        {
                            throw new Exception("Account entity could not be null!");
                        }

                        var model = accountEntity.ToExportModel();

                        return model;
                    }
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

                await SaveChanges();

                return null;
            }
        }

        public async Task UpdateProfile(ProfileImportModel importModel)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    var entity = await unitOfWork.AccountRepository.GetFirstByIdAsync(importModel.UserId);

                    if (entity != null)
                    {
                        entity.FirstName = importModel.FirstName;
                        entity.LastName = importModel.LastName;
                        entity.DateOfBirth = importModel.DateOfBirth ?? "";
                        entity.Culture = importModel.Culture;

                        await unitOfWork.AccountRepository.Update(entity);

                        await SaveChanges();
                    }
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

                await SaveChanges();
            }
        }

        public async Task<bool> CheckUserName(string userName)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    var allAccounts = await unitOfWork.AccountRepository.GetAllAsync();

                    if (allAccounts.Any())
                    {
                        var selectedUserName = allAccounts.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());

                        return selectedUserName == null;
                    }

                    return true;
                }

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

                await SaveChanges();

                return false;
            }
        }

        public async Task<bool> CheckPassword(PasswordUpdateModel model)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    var entity = await unitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                    if (entity == null)
                    {
                        throw new Exception($"Could not check password of user: [{model.UserId}]!");
                    }

                    var securedPassword = Helpers.GetEncodedSecret(model.Password, entity.Salt);

                    return securedPassword == entity.Secret;
                }

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

                await SaveChanges();

                return false;
            }
        }

        public async Task<bool> UpdatePassword(PasswordUpdateModel model)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    var entity = await unitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                    if (entity == null)
                    {
                        throw new Exception($"Could not update password of user: [{model.UserId}]!");
                    }

                    var salt = Guid.NewGuid().ToString();
                    var securedPassword = Helpers.GetEncodedSecret(model.Password, salt);

                    entity.Salt = salt;
                    entity.Secret = securedPassword;

                    await unitOfWork.AccountRepository.Update(entity);
                    await SaveChanges();

                    return true;
                }

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

                await SaveChanges();

                return false;
            }
        }

        public async Task UpdateUser(UserUpdateImportModel model)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    var account = await unitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                    if(account == null)
                    {
                        throw new Exception($"Could not update user: [{model.UserId}]");
                    }

                    account.IsActive = model.IsActive;
                    account.Role = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), model.Role.ToString());

                    await unitOfWork.AccountRepository.Update(account);

                    await SaveChanges();
                }

                }catch(Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAccountService),
                });

                await SaveChanges();
            }
        }
    }
}
