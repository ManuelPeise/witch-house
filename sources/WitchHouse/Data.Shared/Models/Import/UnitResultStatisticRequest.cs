namespace Data.Shared.Models.Import
{
    public class UnitResultStatisticRequest
    {
        public Guid UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
