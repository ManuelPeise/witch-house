using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class UnitResultStaticticModel
    {
        public UnitTypeEnum UnitType { get; set; }
        public List<UnitResultStatisticEntry> Entries { get; set; } = new List<UnitResultStatisticEntry>();
    }

    public class UnitResultStatisticEntry
    {
        public DateTime TimeStamp { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
    }
}
