using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Administration;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class FamilyAdministrationService: ApiControllerBase
    {

        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IModuleConfigurationService _moduleService;
        private readonly CurrentUser _currentUser;
        public FamilyAdministrationService(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleService, IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
        {
            _accountUnitOfWork = accountUnitOfWork;
            _moduleService = moduleService;
            _currentUser = GetCurrentUser();
        }

        [HttpGet(Name = "GetFamilyUsers")]
        public async Task<List<UserDataExportModel>> GetFamilyUsers([FromQuery] Guid familyGuid)
        {
            var service = new FamilyAdministration(_accountUnitOfWork, _currentUser);

            return await service.GetFamilyUsers(familyGuid);
        }

        [HttpPost(Name = "AddFamilyUser")]
        public async Task AddFamilyUser([FromBody] FamilyMemberImportModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleService, _currentUser);

            await service.AssignAccountToFamily(model);
        }

        [HttpPost(Name = "UpdateFamilyMember")]
        public async Task UpdateFamilyMember([FromBody] UserUpdateImportModel model)
        {
            var service = new FamilyAccountService(_accountUnitOfWork, _moduleService, _currentUser);
          
            await service.UpdateUser(model);
        }
    }
}
