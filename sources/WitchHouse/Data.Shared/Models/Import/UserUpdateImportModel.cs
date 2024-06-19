namespace Data.Shared.Models.Import
{
    public class UserUpdateImportModel
    {
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
        public List<int> Roles { get; set; }
    }
}
