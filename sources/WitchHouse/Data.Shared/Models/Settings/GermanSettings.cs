using System.Text.Json.Serialization;

namespace Data.Shared.Models.Settings
{
    public class GermanSettings
    {
        [JsonPropertyName("maxWordLength")]
        public int MaxWordLength { get; set; }
    }
}
