using Data.Database;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Administration;
using Logic.Family;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class FamilyAdministrationService: ApiControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public FamilyAdministrationService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet(Name = "GetFamilyUsers")]
        public async Task<List<UserDataExportModel>> GetFamilyUsers([FromQuery] Guid familyGuid)
        {
            var currentUser = GetCurrentUser();

            var service = new FamilyAdministration(_databaseContext, currentUser);

            return await service.GetFamilyUsers(familyGuid);
        }

        [HttpPost(Name = "AddFamilyUser")]
        public async Task AddFamilyUser([FromBody] FamilyMemberImportModel model)
        {
            var currentUser = GetCurrentUser();

            var service = new FamilyAccountService(_databaseContext, currentUser);

            await service.AssignAccountToFamily(model);
        }

        [HttpPost(Name = "UpdateFamilyMember")]
        public async Task UpdateFamilyMember([FromBody] UserUpdateImportModel model)
        {
            var currentUser = GetCurrentUser();

            var service = new FamilyAccountService(_databaseContext, currentUser);
          
            await service.UpdateUser(model);
        }
    }
}
