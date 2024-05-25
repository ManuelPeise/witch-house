using System.ComponentModel.DataAnnotations;

namespace Data.Shared.Entities
{
    public class LogMessageEntity
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Stacktrace { get; set; } = string.Empty;
        public string TimeStamp { get; set; } = string.Empty;
        public string Trigger { get; set; } = string.Empty;
    }
}
