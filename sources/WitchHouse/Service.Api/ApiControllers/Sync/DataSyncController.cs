using Data.Shared.Models.Sync;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Sync
{
    [Authorize]
    public class DataSyncController : ApiControllerBase
    {
        private readonly ISyncHandler _syncHandler;

        public DataSyncController(IHttpContextAccessor contextAccessor, ISyncHandler syncHandler) : base(contextAccessor)
        {
            _syncHandler = syncHandler;
        }

        [HttpPost(Name = "SyncAppData")]
        public async Task<DataSyncExportModel?> SyncAppData([FromBody] DataSyncImportModel model)
        {
            return await _syncHandler.ExecuteSync(model, base.GetCurrentUser());
        }
    }
}
