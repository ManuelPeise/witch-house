using Data.Shared.Enums;
using Data.Shared.Models.Export;

namespace Data.Shared.Entities
{
    public class AccountEntity : AEntityBase
    {
        public Guid? FamilyGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public string Culture { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; }
        public string Salt { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsActive { get; set; } = false;
        public string? ProfileImage { get; set; }

        public ProfileExportModel ToExportModel()
        {
            return new ProfileExportModel
            {
                UserId = Id,
                FamilyGuid = FamilyGuid,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Culture = Culture,
                DateOfBirth = string.IsNullOrWhiteSpace(DateOfBirth) ? null : DateTime.Parse(DateOfBirth).ToString("yyyy/MM/dd"),
                ProfileImage = ProfileImage,
            };
        }

        public UserDataExportModel ToUserDataExportModel()
        {
            return new UserDataExportModel
            {
                UserId = Id,
                FamilyGuid = FamilyGuid,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Role = Role,
                IsActive = IsActive,
            };
        }
    }
}
