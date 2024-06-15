using Data.Shared.Models.Account;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Account
{
    public class AccountServiceController : ApiControllerBase
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleConfigurationService;
      

        public AccountServiceController(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleConfigurationService):base() 
        {
            _accountUnitOfWork = accountUnitOfWork;
            _moduleConfigurationService = moduleConfigurationService; 
        }

        [HttpGet(Name = "CheckUserName")]
        public async Task<bool> CheckUserName([FromQuery] string userName)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService);

            return await service.CheckUserName(userName);
        }

        [HttpPost(Name = "RegisterFamily")]
        public async Task<bool> RegisterFamily([FromBody] AccountImportModel importModel)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService);

            return await service.CreateFamilyAccount(importModel);
        }
    }
}
