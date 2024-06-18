using Data.Shared.Entities;
using Data.Shared.Enums;

namespace Data.Shared.SqLiteEntities
{
    public class RoleTableEntity:ASqliteEntityBase
    {
        public UserRoleEnum RoleType { get; set; }
        public string? RoleName { get; set; }
    }
}
