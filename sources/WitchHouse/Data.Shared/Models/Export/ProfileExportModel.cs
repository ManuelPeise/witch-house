namespace Data.Shared.Models.Export
{
    public class ProfileExportModel
    {
        public Guid UserId { get; set; }
        public Guid FamilyGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public string Culture { get; set; } = string.Empty;
    }
}
