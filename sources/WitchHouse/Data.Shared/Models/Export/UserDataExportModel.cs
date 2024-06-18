using Data.Shared.Enums;

namespace Data.Shared.Models.Export
{
    public class UserDataExportModel
    {
        public Guid UserId { get; set; }
        public Guid? FamilyGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<UserRoleEnum>? Roles { get; set; }
    }
}
