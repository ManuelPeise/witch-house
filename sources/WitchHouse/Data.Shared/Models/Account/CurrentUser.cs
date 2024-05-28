using Data.Shared.Enums;

namespace Data.Shared.Models.Account
{
    public class CurrentUser
    {
        public Guid? UserGuid { get; set; }
        public Guid? FamilyGuid { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public UserRoleEnum? UserRole { get; set; }
    }
}
