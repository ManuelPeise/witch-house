using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Account
{
    [Authorize]
    public class ProfileServiceController : ApiControllerBase
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleConfigurationService;
       
        public ProfileServiceController(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleConfigurationService): base() 
        {
            _accountUnitOfWork = accountUnitOfWork;
            _moduleConfigurationService = moduleConfigurationService;
        }

        [HttpGet("{userid}", Name = "GetProfile")]
        public async Task<ProfileExportModel?> GetProfile(string userid)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService);

            return await service.GetProfile(userid);
        }

        [HttpPost(Name = "CheckPassword")]
        public async Task<bool> CheckPassword([FromBody]PasswordUpdateModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService);

            return await service.CheckPassword(model);
        }

        [HttpPost(Name = "UpdatePassword")]
        public async Task<bool> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService);

            return await service.UpdatePassword(model);
        }

        [HttpPost(Name = "UpdateProfile")]
        public async Task UpdateProfile([FromBody] ProfileImportModel importModel)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService);

            await service.UpdateProfile(importModel);
        }

        [HttpPost(Name = "UploadProfileImage")]
        public async Task UploadProfileImage([FromBody] ProfileImageUploadModel importModel)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleConfigurationService);

            await service.UploadProfileImage(importModel);
        }
    }
}
