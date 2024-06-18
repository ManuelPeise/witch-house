using System.ComponentModel.DataAnnotations;

namespace Data.Shared.SqLiteEntities
{
    public abstract class ASqliteEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
