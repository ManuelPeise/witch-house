using Data.Shared.Enums;
using System.Text.Json.Serialization;

namespace Data.Shared.Models.Import
{
    public class UserUpdateImportModel
    {
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public int Role { get; set; }
    }
}
