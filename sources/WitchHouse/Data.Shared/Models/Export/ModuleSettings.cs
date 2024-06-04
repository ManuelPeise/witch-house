using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class ModuleSettings
    {
        public Guid UserId { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public ModuleSettingsTypeEnum ModuleSettingsType { get; set; }
        public string Settings { get; set; } = string.Empty;
    }
}
