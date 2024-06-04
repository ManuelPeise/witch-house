using Data.Shared.Enums;

namespace Data.Shared.Models.Import
{
    public class UserModuleRequestModel
    {
        public Guid UserGuid { get; set; } 
        public Guid FamilyGuid { get; set; }
        public UserRoleEnum RoleId { get; set; }
    }
}
