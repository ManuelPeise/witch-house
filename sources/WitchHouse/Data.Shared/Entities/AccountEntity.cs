using Data.Shared.Models.Export;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Shared.Entities
{
    public class AccountEntity : AEntityBase
    {
        public Guid? FamilyGuid { get; set; }
        [ForeignKey(nameof(FamilyGuid))]
        public virtual FamilyEntity? Family { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string? Culture { get; set; }
        public string? ProfileImage { get; set; }
        public Guid? CredentialGuid { get; set; }
        [ForeignKey(nameof(CredentialGuid))]
        public virtual CredentialEntity? Credentials { get; set; }
        public virtual List<RoleEntity>? UserRoles { get; set; }
        public virtual List<ModuleEntity>? Modules { get; set; }
        public virtual List<UnitResultEntity>? UnitResults { get; set; }

        public ProfileExportModel ToExportModel()
        {
            return new ProfileExportModel
            {
                UserId = Id,
                FamilyGuid = FamilyGuid,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Culture = Culture ?? "en",
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
                IsActive = IsActive,
            };
        }
    }
}
