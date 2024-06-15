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
        public ModuleConfiguration UserModules { get; set; } = new ModuleConfiguration();
    }

    public class MobileLoginResult
    {
        public UserData UserData { get; set; } = new UserData();
        public JwtData JwtData { get; set; } = new JwtData();
        public ModuleConfiguration ModuleConfiguration { get; set; } = new ModuleConfiguration();
        public List<ModuleSettings> TrainingModuleSettings { get; set; } = new List<ModuleSettings>();
    }

    public class UserData
    {
        public Guid UserId { get; set; }
        public Guid? FamilyGuid { get; set; }
        public UserRoleEnum UserRole { get; set; }
        public string Language { get; set; } = "en";
        public string UserName { get; set; } = string.Empty;
    }

    public class JwtData
    {
        public string JwtToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
