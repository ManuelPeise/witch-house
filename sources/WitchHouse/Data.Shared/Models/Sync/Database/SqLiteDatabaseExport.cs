namespace Data.Shared.Models.Sync.Database
{
    public class SqLiteDatabaseExport
    {
        public SyncTableModel? SyncTableModel { get; set; }
        public UserTabelModel? UserTableModel { get; set; }
        public CredentialTableModel? CredentialTableModel { get; set; }
        public List<ModuleTabelModel>? ModuleTableModels { get; set; }
    }
}
