namespace Data.Shared.Models.Sync.Database
{
    public class UserTabelModel
    {
        public string? UserId { get; set; }
        public string? FamilyId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string? Culture { get; set; }
        public string? ProfileImage { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
