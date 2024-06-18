using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class ModuleSettings
    {
        public Guid ModuleGuid { get; set; }
        public Guid UserId { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public ModuleSettingsTypeEnum ModuleSettingsType { get; set; }
        public SchoolSettings? Settings { get; set; }
    }

    public class SchoolSettings
    {
        // math settings
        public bool AllowAddition { get; set; }
        public bool AllowSubtraction { get; set; }
        public bool AllowMultiply { get; set; }
        public bool AllowDivide { get; set; }
        public bool AllowDoubling { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        // german settings
        public int MaxWordLength { get; set; }
    }
}
