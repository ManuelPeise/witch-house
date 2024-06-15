using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Administration;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class FamilyAdministrationService: ApiControllerBase
    {

        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleService;
        
        public FamilyAdministrationService(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleService): base()
        {
            _accountUnitOfWork = accountUnitOfWork;
            _moduleService = moduleService;
        }

        [HttpGet(Name = "GetFamilyUsers")]
        public async Task<List<UserDataExportModel>> GetFamilyUsers([FromQuery] Guid familyGuid)
        {
            var service = new FamilyAdministration(_accountUnitOfWork);

            return await service.GetFamilyUsers(familyGuid);
        }

        [HttpPost(Name = "AddFamilyUser")]
        public async Task AddFamilyUser([FromBody] FamilyMemberImportModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleService);

            await service.AssignAccountToFamily(model);
        }

        [HttpPost(Name = "UpdateFamilyMember")]
        public async Task UpdateFamilyMember([FromBody] UserUpdateImportModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleService);
          
            await service.UpdateUser(model);
        }
    }
}
