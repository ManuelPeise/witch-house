using System.ComponentModel.DataAnnotations;

namespace Data.Shared.Entities
{
    public abstract class AEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
