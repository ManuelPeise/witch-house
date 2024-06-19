using Data.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Shared.Entities
{
    public class ModuleEntity : AEntityBase
    {
        public string ModuleName { get; set; } = string.Empty;
        public ModuleTypeEnum ModuleType { get; set; }
        public ModuleSettingsTypeEnum ModuleSettingsType { get; set; }
        public string? SettingsJson { get; set; }
        public bool IsActive { get; set; }
        public Guid? AccountGuid { get; set; }
        [ForeignKey(nameof(AccountGuid))]
        public AccountEntity? Account { get; set; }
    }
}
