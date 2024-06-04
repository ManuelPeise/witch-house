using Data.Database;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Account
{
    [Authorize]
    public class ProfileServiceController : ApiControllerBase
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleConfigurationService;
        private readonly CurrentUser _currentUser;

        public ProfileServiceController(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleConfigurationService, IHttpContextAccessor contextAccessor): base(contextAccessor) 
        {
            _accountUnitOfWork = accountUnitOfWork;
            _moduleConfigurationService = moduleConfigurationService;
            _currentUser = GetCurrentUser();
        }

        [HttpGet("{userid}", Name = "GetProfile")]
        public async Task<ProfileExportModel?> GetProfile(string userid)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService, _currentUser);

            return await service.GetProfile(userid);
        }

        [HttpPost(Name = "CheckPassword")]
        public async Task<bool> CheckPassword([FromBody]PasswordUpdateModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService, _currentUser);

            return await service.CheckPassword(model);
        }

        [HttpPost(Name = "UpdatePassword")]
        public async Task<bool> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService, _currentUser);

            return await service.UpdatePassword(model);
        }

        [HttpPost(Name = "UpdateProfile")]
        public async Task UpdateProfile([FromBody] ProfileImportModel importModel)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService, _currentUser);

            await service.UpdateProfile(importModel);
        }
    }
}
