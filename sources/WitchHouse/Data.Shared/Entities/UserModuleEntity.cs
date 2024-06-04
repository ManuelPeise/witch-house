using Data.Shared.Enums;
using Data.Shared.Models.Export;

namespace Data.Shared.Entities
{
    public class UserModuleEntity:AEntityBase
    {
        public Guid UserId { get; set; }
        public Guid ModuleId { get; set; }
        public ModuleTypeEnum ModuleType { get; set; }
        public bool IsActive { get; set; }
 

        public UserModule ToExportModel()
        {
            return new UserModule
            {
                UserId = UserId,
                ModuleId = ModuleId,
                ModuleType = ModuleType,
                IsActive = IsActive,
            };
        }
    }
}
