using Data.Shared.Enums;
using System.Text.Json.Serialization;

namespace Data.Shared.Models.Export
{
    public class ModuleSettings
    {
        public Guid ModuleGuid { get; set; }
        public Guid UserId { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public ModuleSettingsTypeEnum ModuleSettingsType { get; set; }
        public string? Settings { get; set; }
    }

    public class SchoolSettings
    {
        [JsonPropertyName("allowAddition")]
        public bool AllowAddition { get; set; }
        [JsonPropertyName("allowSubtraction")]
        public bool AllowSubtraction { get; set; }
        [JsonPropertyName("allowMultiply")]
        public bool AllowMultiply { get; set; }
        [JsonPropertyName("allowDivide")]
        public bool AllowDivide { get; set; }
        [JsonPropertyName("allowDoubling")]
        public bool AllowDoubling { get; set; }
        [JsonPropertyName("minValue")]
        public int MinValue { get; set; }
        [JsonPropertyName("maxValue")]
        public int MaxValue { get; set; }
        [JsonPropertyName("maxWordLength")]
        public int MaxWordLength { get; set; }
    }
}
