using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
using Logic.Authentication;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Service.Api.ApiControllers.Account
{
    public class AccountServiceController : ApiControllerBase
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleConfigurationService;
        private readonly CurrentUser _currentUser;

        public AccountServiceController(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleConfigurationService, IHttpContextAccessor contextAccessor):base(contextAccessor) 
        {
            _accountUnitOfWork = accountUnitOfWork;
            _moduleConfigurationService = moduleConfigurationService;
            _currentUser = GetCurrentUser();
        }

        [HttpGet(Name = "CheckUserName")]
        public async Task<bool> CheckUserName([FromQuery] string userName)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService, _currentUser);

            return await service.CheckUserName(userName);
        }

        [HttpPost(Name = "RegisterFamily")]
        public async Task<bool> RegisterFamily([FromBody] AccountImportModel importModel)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService, _currentUser);

            return await service.CreateFamilyAccount(importModel);
        }
    }
}
