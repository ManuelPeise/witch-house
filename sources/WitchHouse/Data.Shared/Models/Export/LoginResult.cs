using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public Guid UserId { get; set; }
        public Guid? FamilyGuid { get; set; }
        public UserRoleEnum UserRole { get; set; }
        public string Language { get; set; } = "en";
        public string UserName { get; set; } = string.Empty;
        public string Jwt { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
