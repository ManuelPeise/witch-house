namespace Data.Shared.Entities
{
    public class UserModuleEntity:AEntityBase
    {
        public Guid UserId { get; set; }
        public Guid ModuleId { get; set; }
        public bool IsActive { get; set; }
        public string SettingsJson { get; set; } = string.Empty;
    }
}
