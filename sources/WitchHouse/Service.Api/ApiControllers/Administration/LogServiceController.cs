using Data.Database;
using Data.Shared.Entities;
using Logic.Administration;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class LogServiceController : ApiControllerBase
    {
        private readonly IApplicationUnitOfWork<DatabaseContext> _applucationUnitOfWork;

        public LogServiceController(IApplicationUnitOfWork<DatabaseContext> applucationUnitOfWork) : base()
        {
            _applucationUnitOfWork = applucationUnitOfWork;
        }

        [HttpGet(Name = "GetLogMessages")]
        public async Task<IEnumerable<LogMessageEntity>> GetLogMessages(Guid? familyGuid)
        {
            var service = new LogService(_applucationUnitOfWork);

            return await service.LoadLogMessages(familyGuid);
        }

        [HttpPost(Name ="DeleteLogMessages")]
        public async Task DeleteLogMessages([FromBody] int[] messageIds)
        {
            var service = new LogService(_applucationUnitOfWork);

            await service.DeleteLogmessages(messageIds);
        }
    }
}
