using Data.Shared.Enums;

namespace Data.Shared.Entities
{
    public class ModuleEntity : AEntityBase
    {
        public string ModuleName { get; set; } = string.Empty;
        public ModuleTypeEnum ModuleType { get; set; }
    }
}
