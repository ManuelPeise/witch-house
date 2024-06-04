using System.Text.Json.Serialization;

namespace Data.Shared.Models.Settings
{
    public class MathSettings
    {
        [JsonPropertyName("allowAddition")]
        public bool AllowAddition { get; set; }
        [JsonPropertyName("allowSubtraction")]
        public bool AllowSubtraction { get; set; }
        [JsonPropertyName("allowMultiply")]
        public bool AllowMultiply { get; set; }
        [JsonPropertyName("allowDivide")]
        public bool AllowDivide { get; set; }
        [JsonPropertyName("maxValue")]
        public int MaxValue { get; set; }
    }
}
