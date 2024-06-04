using Data.Database;
using Data.Shared.Entities;
using Logic.Administration;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class LogServiceController : ApiControllerBase
    {
        private readonly ILogRepository _logRepository;
        private readonly DatabaseContext _databaseContext;

        public LogServiceController(ILogRepository logRepository, DatabaseContext context, IHttpContextAccessor contextAccessor): base(contextAccessor)
        {
            _logRepository = logRepository;
            _databaseContext = context;
        }

        [HttpGet(Name = "GetLogMessages")]
        public async Task<IEnumerable<LogMessageEntity>> GetLogMessages()
        {
            var currentUser = GetCurrentUser();

            var service = new LogService(_databaseContext, _logRepository, currentUser);

            return await service.LoadLogMessages();
        }

        [HttpPost(Name ="DeleteLogMessages")]
        public async Task DeleteLogMessages([FromBody] int[] messageIds)
        {
            var currentUser = GetCurrentUser();

            var service = new LogService(_databaseContext, _logRepository, currentUser);

            await service.DeleteLogmessages(messageIds);
        }
    }
}
