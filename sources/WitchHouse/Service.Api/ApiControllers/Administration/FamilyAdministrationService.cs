using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
using Logic.Administration.Interfaces;
using Logic.Family.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
   // [Authorize]
    public class FamilyAdministrationService : ApiControllerBase
    {

        private readonly IFamilyAccountService _familyAccountService;
        private readonly IFamilyAdministrationService _familyAdministrationService;


        public FamilyAdministrationService(IFamilyAccountService familyAccountService, IFamilyAdministrationService familyAdministrationService) : base()
        {
            _familyAccountService = familyAccountService;
            _familyAdministrationService = familyAdministrationService;
        }

        [HttpGet(Name = "GetFamilyUsers")]
        public async Task<List<UserDataExportModel>> GetFamilyUsers([FromQuery] Guid familyGuid)
        {
            return await _familyAdministrationService.GetFamilyUsers(familyGuid);
        }

        [HttpGet(Name = "LoadFamilyUserData")]
        public async Task<ResponseMessage<UserDataModel>> LoadFamilyUserData([FromQuery] Guid accountGuid)
        {
            return await _familyAdministrationService.GetUserData(accountGuid);
        }

        [HttpPost(Name = "AddFamilyUser")]
        public async Task AddFamilyUser([FromBody] AccountImportModel model)
        {
            await _familyAccountService.AssignAccountToFamily(model);
        }

        [HttpPost(Name = "UpdateFamilyMember")]
        public async Task UpdateFamilyMember([FromBody] UserUpdateImportModel model)
        {
            await _familyAccountService.UpdateUser(model);
        }
    }
}
