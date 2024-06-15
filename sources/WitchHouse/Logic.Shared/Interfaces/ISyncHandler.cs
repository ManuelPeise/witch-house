using Data.Shared.Models.Account;
using Data.Shared.Models.Sync;

namespace Logic.Shared.Interfaces
{
    public interface ISyncHandler: IDisposable
    {
        Task<DataSyncExportModel?> ExecuteSync(DataSyncImportModel importModel);
    }
}
