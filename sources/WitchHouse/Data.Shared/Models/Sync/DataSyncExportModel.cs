using Data.Shared.Models.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared.Models.Sync
{
    public class DataSyncExportModel
    {
        public UserDataSync UserData { get; set; } = new UserDataSync();
        public List<SchoolModuleSync> SchoolModules { get; set; } = new List<SchoolModuleSync>();

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
