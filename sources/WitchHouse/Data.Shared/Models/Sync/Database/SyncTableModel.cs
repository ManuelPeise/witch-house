namespace Data.Shared.Models.Sync.Database
{
    public class SyncTableModel
    {
        public string? SyncId { get; set; }
        public Guid UserGuid { get; set; }
        public DateTime LastSync { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
