using Data.Shared.Entities;
using Data.Shared.Enums;
using System.Text;

namespace Data.Shared.Models.Account
{
    public class AccountImportModel
    {
        public FamilyAccountImportModel? Family { get; set; }
        public UserAccountImportModel UserAccount { get; set; } = new UserAccountImportModel();
    }

    public class FamilyAccountImportModel
    {
        public Guid FamilyGuid { get; set; }
        public string? FamilyName { get; set; }
        public string? FamilyFullName { get; set; }

        public FamilyEntity ToEntity(Guid guid)
        {
            return new FamilyEntity
            {
                Id = guid,
                FamilyName = FamilyName,
                FamilyFullName = FamilyFullName,
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                CreatedBy = "System"
            };
        }
    }

    public class UserAccountImportModel
    {
        public Guid FamilyGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public string Secret { get; set; } = string.Empty;
        public UserRoleEnum? UserRole { get; set; }

        public AccountEntity ToEntity(Guid id, Guid familyGuid, Guid salt, string encodedSecret, UserRoleEnum role = UserRoleEnum.LocalAdmin)
        {
            var credentialGuid = Guid.NewGuid();

            return new AccountEntity
            {
                Id = id,
                FamilyGuid = familyGuid,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                DateOfBirth = DateOfBirth,
                CredentialGuid = credentialGuid,
                Credentials = new CredentialEntity
                {
                    Id = credentialGuid,
                    Salt = salt,
                    EncodedPassword = encodedSecret,
                    JwtToken = string.Empty,
                    RefreshToken = string.Empty,
                    MobilePin = 1234,
                },
                UserRoles = new List<RoleEntity>
                {
                      new RoleEntity
                      {
                          Id = Guid.NewGuid(),
                          AccountGuid = id,
                          RoleType = role,
                          RoleName = Enum.GetName(typeof(UserRoleEnum), role)
                      }
                },
                Modules = new List<ModuleEntity>
                {
                    new ModuleEntity
                    {
                        Id = Guid.NewGuid(),
                        AccountGuid = id,
                        ModuleType = ModuleTypeEnum.SchoolTraining,
                        ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTraining),
                        SettingsJson = null,
                        IsActive = true,
                    },
                    new ModuleEntity
                    {
                        Id = Guid.NewGuid(),
                        AccountGuid = id,
                        ModuleType = ModuleTypeEnum.SchoolTrainingStatistics,
                        ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTrainingStatistics),
                        SettingsJson = null,
                        IsActive = true,
                    }
                },
                IsActive = true,
                Culture = "en",
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                CreatedBy = "System",
                ProfileImage = null,
                UnitResults = null,
            };
        }

        public AccountEntity ToFamilyMember(Guid id, Guid familyGuid, Guid salt, string encodedSecret, UserRoleEnum role)
        {
            var credentialGuid = Guid.NewGuid();

            return new AccountEntity
            {
                Id = id,
                FamilyGuid = familyGuid,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                DateOfBirth = DateOfBirth,
                CredentialGuid = credentialGuid,
                Credentials = new CredentialEntity
                {
                    Id = credentialGuid,
                    Salt = salt,
                    EncodedPassword = encodedSecret,
                    JwtToken = string.Empty,
                    RefreshToken = string.Empty,
                    MobilePin = 1234,
                },
                UserRoles = new List<RoleEntity>
                {
                      new RoleEntity
                      {
                          Id = Guid.NewGuid(),
                          AccountGuid = id,
                          RoleType = role,
                          RoleName = Enum.GetName(typeof(RoleEntity), role)
                      }
                },
                Modules = new List<ModuleEntity>
                {
                    new ModuleEntity
                    {
                        Id = Guid.NewGuid(),
                        AccountGuid = id,
                        ModuleType = ModuleTypeEnum.SchoolTraining,
                        ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTraining),
                        SettingsJson = null,
                        IsActive = false,
                    },
                    new ModuleEntity
                    {
                        Id = Guid.NewGuid(),
                        AccountGuid = id,
                        ModuleType = ModuleTypeEnum.SchoolTrainingStatistics,
                        ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTrainingStatistics),
                        SettingsJson = null,
                        IsActive = false,
                    }
                },
                IsActive = false,
                Culture = "en",
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                CreatedBy = "System",
                ProfileImage = null,
                UnitResults = null,
            };
        }
    }
}
