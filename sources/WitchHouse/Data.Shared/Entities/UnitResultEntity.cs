using Data.Shared.Enums;

namespace Data.Shared.Entities
{
    public class UnitResultEntity:AEntityBase
    {
        public UnitTypeEnum UnitType { get; set; }
        public Guid UserId { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
        public string? TimeStamp { get; set; }
    }
}
