using Data.Shared.Enums;

namespace Data.Shared.Models.Sync
{
    public class DataSyncImportModel
    {
        public Guid UserId { get; set; }
        public Guid FamilyId  { get; set; }
        public UserRoleEnum RoleId { get; set; }
    }
}
