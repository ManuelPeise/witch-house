using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class LoginResult
    {
        public UserData? UserData{ get; set; }
        public JwtData? JwtData { get; set; }
        public List<UserModule>? Modules { get; set; }
    }

    public class MobileLoginResult
    {
        public Guid UserGuid { get; set; }
        public string? UserName { get; set; }
        public string? JwtToken { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
    }

    public class UserData
    {
        public Guid UserId { get; set; }
        public Guid? FamilyGuid { get; set; }
        public List<UserRoleEnum>? UserRoles { get; set; }
        public string Language { get; set; } = "en";
        public string UserName { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
    }

    public class JwtData
    {
        public string JwtToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
