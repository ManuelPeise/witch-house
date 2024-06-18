using Data.Shared.Models.Account;
using Logic.Family.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Account
{
    public class AccountServiceController : ApiControllerBase
    {
        private readonly IFamilyAccountService _familyAccountService;

        public AccountServiceController(IFamilyAccountService familyAccountService) : base()
        {
            _familyAccountService = familyAccountService;
        }

        [HttpGet(Name = "CheckUserName")]
        public async Task<bool> CheckUserName([FromQuery] string userName)
        {
            return await _familyAccountService.CheckUserName(userName);
        }

        [HttpPost(Name = "RegisterFamily")]
        public async Task<bool> RegisterFamily([FromBody] AccountImportModel importModel)
        {
            return await _familyAccountService.CreateFamilyAccount(importModel);
        }
    }
}
