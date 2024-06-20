using Data.Shared.Enums;

namespace Data.Shared.Models.Sync.Database
{
    public class ModuleTabelModel
    {
        public string? AccountGuid { get; set; }
        public string? ModuleId { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public ModuleTypeEnum ModuleType { get; set; }
        public ModuleSettingsTypeEnum ModuleSettingsType { get; set; }
        public string? SettingsJson { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;


    }
}
