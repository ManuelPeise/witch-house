using Data.Database;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Account
{
    public class AccountServiceController : ApiControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogRepository _logRepository;

        public AccountServiceController(DatabaseContext databaseContext, ILogRepository logRepository)
        {
            _databaseContext = databaseContext;
            _logRepository = logRepository;
        }

        [HttpGet(Name = "CheckUserName")]
        public async Task<bool> CheckUserName([FromQuery] string userName)
        {
            var service = new FamilyAccountService(_databaseContext, _logRepository);

            return await service.CheckUserName(userName);
        }


        [HttpPost(Name = "RegisterFamily")]
        public async Task<bool> RegisterFamily([FromBody] AccountImportModel importModel)
        {
            var service = new FamilyAccountService(_databaseContext, _logRepository);

            return await service.CreateFamilyAccount(importModel);
        }

        [Authorize]
        [HttpPost(Name = "AssignAccountToFamily")]
        public async Task<bool> AssignAccountToFamily([FromBody] AccountImportModel importModel)
        {
            var service = new FamilyAccountService(_databaseContext, _logRepository);

            return await service.AssignAccountToFamily(importModel);
        }
    }
}
