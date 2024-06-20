using Data.Shared.Models.Response;
using Data.Shared.Models.Sync;
using Data.Shared.Models.Sync.Database;
using Logic.Sync.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Sync
{
    [Authorize]
    public class DataSyncController : ApiControllerBase
    {
        private readonly IDataSyncService _syncService;

        public DataSyncController(IDataSyncService syncService) : base()
        {
            _syncService = syncService;
        }



        [HttpGet(Name = "LoadAppData")]
        public async Task<ResponseMessage<SqLiteDatabaseExport>> LoadAppData([FromQuery] string userId)
        {
            return await _syncService.ExexuteMobileSyncAsync(new SyncModel { UserId = new Guid(userId)});
        }

        [HttpPost(Name = "SyncAppData")]
        public async Task<ResponseMessage<SqLiteDatabaseExport>> SyncAppData([FromBody] SyncModel model)
        {
            return await _syncService.ExexuteMobileSyncAsync(model);
        }
    }
}
