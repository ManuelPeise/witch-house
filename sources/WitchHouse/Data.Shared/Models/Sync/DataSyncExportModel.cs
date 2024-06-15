using Data.Shared.Models.Export;

namespace Data.Shared.Models.Sync
{
    public class DataSyncExportModel
    {
        public UserDataSync UserData { get; set; } = new UserDataSync();
        public ModuleConfiguration ModuleConfiguration { get; set; } = new ModuleConfiguration();
        public List<ModuleSettings> SchoolModules { get; set; } = new List<ModuleSettings>();

    }

    public class UserDataSync
    {
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Birthday { get; set; }
    }

    public class SchoolModuleSync
    {
        public UserModule UserModule { get; set; } = new UserModule();
        public ModuleSettings ModuleSettings { get; set; } = new ModuleSettings();

    }
}
