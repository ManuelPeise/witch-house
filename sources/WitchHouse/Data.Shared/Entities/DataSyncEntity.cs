namespace Data.Shared.Entities
{
    public class DataSyncEntity : AEntityBase
    {
        public Guid UserGuid { get; set; }
        public DateTime LastSync { get; set; }
    }
}
