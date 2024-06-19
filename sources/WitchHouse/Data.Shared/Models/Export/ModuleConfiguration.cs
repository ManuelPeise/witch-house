using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class ModuleConfiguration
    {
        public Guid UserGuid { get; set; }
        public List<UserModule> Modules { get; set; } = new List<UserModule>();
    }

    public class ModuleBase
    {
        public Guid UserId { get; set; }
        public Guid ModuleId { get; set; }
        public ModuleSettingsTypeEnum ModuleSettingsType { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserModule: ModuleBase
    {
        public string? ModuleSettings { get; set; }
    }

    public class SchoolModule: ModuleBase
    {
        public SchoolSettings? Settings { get; set; }
    }
}
