using Data.Shared.Models.Sync.Database;

namespace Data.Shared.Models.Sync
{
    public class SyncModel
    {
        public Guid UserId { get; set; }
        public DateTime? LastSync { get; set; }
        public SqLiteDatabaseExport? Data { get; set; }
    }
}
