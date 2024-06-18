using Data.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Shared.Entities
{
    public class RoleEntity:AEntityBase
    {
        public UserRoleEnum RoleType { get; set; }
        public string? RoleName { get; set; }
        public Guid? AccountGuid { get; set; }
        [ForeignKey(nameof(AccountGuid))]
        public virtual AccountEntity? Account { get; set; }
    }
}
