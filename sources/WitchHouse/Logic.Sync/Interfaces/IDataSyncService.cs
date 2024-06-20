using Data.Shared.Models.Response;
using Data.Shared.Models.Sync;
using Data.Shared.Models.Sync.Database;

namespace Logic.Sync.Interfaces
{
    public interface IDataSyncService: IDisposable
    {
        Task<ResponseMessage<SqLiteDatabaseExport>> ExexuteMobileSyncAsync(SyncModel syncModel);
    }
}
