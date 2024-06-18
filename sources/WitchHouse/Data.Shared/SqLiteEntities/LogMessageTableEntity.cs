namespace Data.Shared.SqLiteEntities
{
    public class LogMessageTableEntity:ASqliteEntityBase
    {
        public string? Message { get; set; }
        public string? Stacktrace { get; set; }
        public string? Trigger { get; set; }
    }
}
