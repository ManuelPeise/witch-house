using Data.Shared.Enums;

namespace Data.Shared.Entities
{
    public class AccountEntity : AEntityBase
    {
        public Guid FamilyGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public UserRoleEnum Role { get; set; }
        public string Salt { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
