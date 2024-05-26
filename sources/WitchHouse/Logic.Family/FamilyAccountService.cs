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
    public class FamilyAccountService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogRepository _logRepository;

        public FamilyAccountService(DatabaseContext databaseContext, ILogRepository logRepository)
        {
            _databaseContext = databaseContext;
            _logRepository = logRepository;
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
                        familyEntity.City);

                    if (accountEntity == null)
                    {
                        throw new Exception("Account entity could not be null!");
                    }

                    result = await unitOfWork.AccountRepository.AddAsync(accountEntity);

                    if (result != null)
                    {
                        await unitOfWork.SaveChanges();

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

                await _logRepository.SaveChanges();

                return false;
            }
        }

        public async Task<bool> AssignAccountToFamily(AccountImportModel accountImportModel)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    if (accountImportModel.Family == null)
                    {
                        return false;
                    }

                    var familyEntity = await unitOfWork.FamilyRepository.GetFirstByIdAsync(accountImportModel.Family.FamilyGuid);

                    if (familyEntity == null)
                    {
                        throw new Exception("Family entity could not be null!");
                    }

                    var salt = Guid.NewGuid().ToString();

                    var accountEntity = accountImportModel.UserAccount.ToEntity(
                        Guid.NewGuid(),
                        familyEntity.Id,
                        salt,
                        Helpers.GetEncodedSecret(accountImportModel.UserAccount.Secret, salt),
                        familyEntity.City,
                        UserRoleEnum.User);

                    if (accountEntity == null)
                    {
                        throw new Exception("Could not create account.");
                    }

                    var result = await unitOfWork.AccountRepository.AddAsync(accountEntity);

                    if (result != null)
                    {
                        await unitOfWork.SaveChanges();

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

                await _logRepository.SaveChanges();

                return false;
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

                await _logRepository.SaveChanges();

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

                        await unitOfWork.SaveChanges();
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

                await _logRepository.SaveChanges();
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
                    TimeStamp = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm")
                });

                await _logRepository.SaveChanges();

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

                    if(entity == null)
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

                await _logRepository.SaveChanges();

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
                    await unitOfWork.SaveChanges();

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

                await _logRepository.SaveChanges();

                return false;
            }
        }
    }
}
