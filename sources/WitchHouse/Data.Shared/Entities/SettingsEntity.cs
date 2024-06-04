using Data.Shared.Enums;

namespace Data.Shared.Entities
{
    public class SettingsEntity: AEntityBase
    {
        public Guid UserId { get; set; }
        public ModuleSettingsTypeEnum SettingsType { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public string SettingsJson { get; set; } = string.Empty;
    }
}
