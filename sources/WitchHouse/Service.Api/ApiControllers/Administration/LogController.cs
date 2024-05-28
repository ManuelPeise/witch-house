using Data.Shared.Entities;
using Logic.Administration;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class LogController : ApiControllerBase
    {
        private readonly ILogRepository _logRepository;

        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [HttpGet(Name = "GetLogmessages")]
        public async Task<IEnumerable<LogMessageEntity>> GetLogmessages()
        {
            var currentUser = GetCurrentUser();

            var service = new LogService(_logRepository, currentUser);

            return await service.LoadLogMessages();
        }
    }
}
