using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class ModuleConfiguration
    {
        public Guid UserGuid { get; set; }
        public List<UserModule> Modules { get; set; } = new List<UserModule>();
    }

    public class UserModule
    {
        public Guid UserId { get; set; }
        public Guid ModuleId { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public bool IsActive { get; set; }
    }
}
