using Data.Shared.Enums;

namespace Data.Shared.Models.Import
{
    public class FamilyMemberImportModel
    {
        public string FamilyGuid { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
