using Data.Shared.Enums;

namespace Data.Shared.SqLiteEntities
{
    public class UnitResultEntity:ASqliteEntityBase
    {
        public UnitTypeEnum UnitType { get; set; }
        public Guid UserId { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
    }
}
