namespace Data.Shared.Models.Account
{
    public class UserDataModel
    {
        public Guid UserId { get; set; }
        public Guid? FamilyGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string? Culture { get; set; }
        public List<int>? UserRoles { get; set; }
    }
}
