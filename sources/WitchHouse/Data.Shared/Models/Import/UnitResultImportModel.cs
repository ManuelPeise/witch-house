using Data.Shared.Entities;
using Data.Shared.Enums;

namespace Data.Shared.Models.Import
{
    public class UnitResultImportModel
    {
        public UnitTypeEnum UnitType { get; set; }
        public Guid UserId { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
        public string? TimeStamp { get; set; }

        public UnitResultEntity ToEntity()
        {
            return new UnitResultEntity
            {
                UnitType = UnitType,
                UserId = UserId,
                Success = Success,
                Failed = Failed,
                TimeStamp = TimeStamp
            };
        }
    }
}
