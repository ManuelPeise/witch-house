using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Account;
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
    }
}
