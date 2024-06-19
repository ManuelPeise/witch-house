using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Family.Interfaces;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Newtonsoft.Json;

namespace Logic.Family
{
    public class FamilyAccountService : IFamilyAccountService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public FamilyAccountService(IApplicationUnitOfWork applicationUnitOfWork) : base()
        {
            _applicationUnitOfWork = applicationUnitOfWork;

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

                var result = await _applicationUnitOfWork.FamilyRepository.AddAsync(familyEntity);

                if (result == null)
                {
                    throw new Exception("Cou´ld not add family to database!");
                }

                var accountGuid = Guid.NewGuid();
                var salt = Guid.NewGuid();

                var accountEntity = accountImportModel.UserAccount.ToEntity(
                    accountGuid,
                    familyGuid,
                    salt,
                    Helpers.GetEncodedSecret(accountImportModel.UserAccount.Secret, salt.ToString()),
                    UserRoleEnum.LocalAdmin);

                if (accountEntity == null)
                {
                    throw new Exception("Account entity could not be null!");
                }

                result = await _applicationUnitOfWork.AccountRepository.AddAsync(accountEntity);

                if (result != null)
                {
                    await _applicationUnitOfWork.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(FamilyAccountService),
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm")
                });

                return false;
            }
        }

        public async Task AssignAccountToFamily(AccountImportModel accountImportModel)
        {
            try
            {
                if (accountImportModel?.Family?.FamilyGuid == null)
                {
                    throw new Exception("Could not assign user to Family.");
                }

                var familyEntity = await _applicationUnitOfWork.FamilyRepository.GetFirstByIdAsync(accountImportModel.Family.FamilyGuid);

                if (familyEntity == null)
                {
                    throw new Exception("Family entity could not be null!");
                }

                var salt = Guid.NewGuid();
                var accountGuid = Guid.NewGuid();

                var accountEntity = accountImportModel.UserAccount.ToFamilyMember(
                    accountGuid,
                    accountImportModel.Family.FamilyGuid,
                    salt,
                    Helpers.GetEncodedSecret("P@ssword", salt.ToString()),
                    UserRoleEnum.User);

                var result = await _applicationUnitOfWork.AccountRepository.AddAsync(accountEntity);

                if (result != null)
                {
                    await _applicationUnitOfWork.SaveChanges();
                }

                return;
            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
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
                    var accountEntity = await _applicationUnitOfWork.AccountRepository.GetFirstByIdAsync(accountGuid);

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
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
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
                var entity = await _applicationUnitOfWork.AccountRepository.GetFirstByIdAsync(importModel.UserId);

                if (entity != null)
                {
                    entity.FirstName = importModel.FirstName;
                    entity.LastName = importModel.LastName;
                    entity.DateOfBirth = importModel.DateOfBirth ?? "";
                    entity.Culture = importModel.Culture;

                    await _applicationUnitOfWork.AccountRepository.Update(entity);

                    await _applicationUnitOfWork.SaveChanges();
                }
            }

            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
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
                var allAccounts = await _applicationUnitOfWork.AccountRepository.GetAllAsync();

                if (allAccounts.Any())
                {
                    var selectedUserName = allAccounts.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());

                    return selectedUserName == null;
                }

                return true;
            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
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
                var entity = await _applicationUnitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                if (entity == null)
                {
                    throw new Exception($"Could not check password of user: [{model.UserId}]!");
                }

                var credentialsEntity = await _applicationUnitOfWork.CredentialsRepository.GetFirstByIdAsync((Guid)entity.CredentialGuid);

                if (credentialsEntity == null)
                {
                    return false;
                }

                var securedPassword = Helpers.GetEncodedSecret(model.Password, credentialsEntity.Salt.ToString());

                return securedPassword == credentialsEntity.EncodedPassword;

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
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
                var entity = await _applicationUnitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                if (entity == null)
                {
                    throw new Exception($"Could not update password of user: [{model.UserId}]!");
                }

                var credentialsEntity = await _applicationUnitOfWork.CredentialsRepository.GetFirstByIdAsync((Guid)entity.CredentialGuid);

                if (credentialsEntity == null)
                {
                    return false;
                }

                var salt = Guid.NewGuid();
                var securedPassword = Helpers.GetEncodedSecret(model.Password, salt.ToString());

                credentialsEntity.Salt = salt;
                credentialsEntity.EncodedPassword = securedPassword;

                await _applicationUnitOfWork.CredentialsRepository.Update(credentialsEntity);
                await _applicationUnitOfWork.SaveChanges();

                return true;

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
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
                var account = await _applicationUnitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                if (account == null)
                {
                    throw new Exception($"Could not update user: [{model.UserId}]");
                }

                account.IsActive = model.IsActive;

                var roles = await _applicationUnitOfWork.RoleRepository.GetByAsync(x => x.AccountGuid == account.Id);

                var roleCopy = roles.ToList();

                var rolesToAdd = model.Roles.Where(x => !roleCopy.Select(r => (int)r.RoleType).Contains(x));

                // delete roles
                foreach (var role in roleCopy)
                {
                    if (!model.Roles.Contains((int)role.RoleType))
                    {
                        _applicationUnitOfWork.RoleRepository.Delete(role.Id);
                    }
                }

                foreach (var role in rolesToAdd)
                {
                    var roleType = (UserRoleEnum)role;

                    await _applicationUnitOfWork.RoleRepository.AddAsync(new RoleEntity
                    {
                        Id = Guid.NewGuid(),
                        AccountGuid = account.Id,
                        RoleType = roleType,
                        RoleName = Enum.GetName(typeof(UserRoleEnum), roleType)
                    });
                }

                await _applicationUnitOfWork.AccountRepository.Update(account);

                await _applicationUnitOfWork.SaveChanges();

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAccountService),
                });
            }
        }

        public async Task UploadProfileImage(ProfileImageUploadModel model)
        {
            try
            {
                var account = await _applicationUnitOfWork.AccountRepository.GetFirstByIdAsync(model.UserId);

                if (account == null)
                {
                    throw new Exception($"Could not upload image for [{model.ProfileImage}]");
                }

                account.ProfileImage = model.ProfileImage;

                await _applicationUnitOfWork.AccountRepository.Update(account);

                await _applicationUnitOfWork.SaveChanges();
            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(FamilyAccountService),
                });
            }
        }

        private string GetDefaultSettings(ModuleTypeEnum moduleType)
        {
            switch (moduleType)
            {
                case ModuleTypeEnum.SchoolTraining:
                    var settings = new SchoolSettings
                    {
                        AllowAddition = false,
                        AllowSubtraction = false,
                        AllowMultiply = false,
                        AllowDivide = false,
                        AllowDoubling = false,
                        MinValue = 0,
                        MaxValue = 0,
                        MaxWordLength = 0,
                    };
                    return JsonConvert.SerializeObject(settings);
                case ModuleTypeEnum.SchoolTrainingStatistics:
                default: return string.Empty;
            }
        }
    }
}
