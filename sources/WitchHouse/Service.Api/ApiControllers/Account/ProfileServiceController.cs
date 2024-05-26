using Data.Database;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Family;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Account
{

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
            var service = new FamilyAccountService(_databaseContext, _logRepository);

            return await service.GetProfile(userid);
        }

        [HttpPost(Name = "CheckPassword")]
        public async Task<bool> CheckPassword([FromBody]PasswordUpdateModel model)
        {
            var service = new FamilyAccountService(_databaseContext, _logRepository);

            return await service.CheckPassword(model);
        }

        [HttpPost(Name = "UpdatePassword")]
        public async Task<bool> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            var service = new FamilyAccountService(_databaseContext, _logRepository);

            return await service.UpdatePassword(model);
        }

        [HttpPost(Name = "UpdateProfile")]
        public async Task UpdateProfile([FromBody] ProfileImportModel importModel)
        {
            var service = new FamilyAccountService(_databaseContext, _logRepository);

            await service.UpdateProfile(importModel);
        }
    }
}
