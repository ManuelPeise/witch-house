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
        public string FamilyName { get; set; } = string.Empty;
        public string? City { get; set; }

        public FamilyEntity ToEntity(Guid guid)
        {
            return new FamilyEntity
            {
                Id = guid,
                FamilyName = FamilyName,
                City = City,
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

        public AccountEntity ToEntity(Guid id, Guid familyGuid, string salt, string encodedSecret, string city, UserRoleEnum? role = UserRoleEnum.Admin)
        {
            return new AccountEntity
            {
                Id = id,
                FamilyGuid = familyGuid,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                DateOfBirth = DateOfBirth,
                Salt = salt,
                Role = role ?? UserRoleEnum.User,
                Secret = encodedSecret,
                Culture = "en",
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                CreatedBy = "System"
            };
        }

        public AccountEntity ToFamilyMember(Guid id, Guid familyGuid, UserRoleEnum? role)
        {
            return new AccountEntity
            {
                Id = id,
                FamilyGuid = familyGuid,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                DateOfBirth = DateOfBirth,
                Salt = Guid.NewGuid().ToString(),
                Role = role ?? UserRoleEnum.User,
                Secret = "",
                Culture = "en",
            };
        }
    }
}
