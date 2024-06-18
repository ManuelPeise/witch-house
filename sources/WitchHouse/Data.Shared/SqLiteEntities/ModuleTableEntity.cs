using Data.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Shared.SqLiteEntities
{
    public class ModuleTableEntity:ASqliteEntityBase
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public string? ModuleName { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserDataTableEntity? UserData { get; set; }
        public string? SettingsJson { get; set; }
    }
}
