using Data.Database;
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
        private readonly DatabaseContext _databaseContext;
        private readonly ILogRepository _logRepository;

        public ProfileServiceController(DatabaseContext databaseContext, ILogRepository logRepository)
        {
            _databaseContext = databaseContext;
            _logRepository = logRepository;
        }

        [HttpGet("{userid}", Name = "GetProfile")]
        public async Task<ProfileExportModel?> GetProfile(string userid)
        {
            var currentUser = GetCurrentUser();
            var service = new FamilyAccountService(_databaseContext, currentUser);

            return await service.GetProfile(userid);
        }

        [HttpPost(Name = "CheckPassword")]
        public async Task<bool> CheckPassword([FromBody]PasswordUpdateModel model)
        {
            var currentUser = GetCurrentUser();
            var service = new FamilyAccountService(_databaseContext, currentUser);

            return await service.CheckPassword(model);
        }

        [HttpPost(Name = "UpdatePassword")]
        public async Task<bool> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            var currentUser = GetCurrentUser();
            var service = new FamilyAccountService(_databaseContext, currentUser);

            return await service.UpdatePassword(model);
        }

        [HttpPost(Name = "UpdateProfile")]
        public async Task UpdateProfile([FromBody] ProfileImportModel importModel)
        {
            var currentUser = GetCurrentUser();
            var service = new FamilyAccountService(_databaseContext, currentUser);

            await service.UpdateProfile(importModel);
        }
    }
}
