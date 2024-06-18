using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class ModuleService : ApiControllerBase
    {
        private readonly IModuleConfigurationService _moduleConfiguration;
       
        public ModuleService(IModuleConfigurationService moduleConfiguration) : base()
        {
            _moduleConfiguration = moduleConfiguration;
          
        }

        [HttpPost(Name = "LoadModuleConfiguration")]
        public async Task<ModuleConfiguration> LoadModuleConfiguration([FromBody] UserModuleRequestModel requestModel)
        {
            return await _moduleConfiguration.LoadUserModuleConfiguration(requestModel);
        }

        [HttpGet(Name = "LoadModuleSchoolSettings")]
        public async Task<List<UserModule>> LoadModuleSchoolSettings([FromQuery] Guid userGuid)
        {
            return await _moduleConfiguration.LoadSchoolModuleSettings(userGuid);
        }

        [HttpPost(Name = "UpdateModuleConfiguration")]
        public async Task UpdateModuleConfiguration([FromBody] UserModule module)
        {
            await _moduleConfiguration.UpdateModule(module);
        }

        [HttpPost(Name = "UpdateSchoolSettings")]
        public async Task UpdateSchoolSettings([FromBody] ModuleSettings settings)
        {
           await _moduleConfiguration.UpdateSchoolModuleSettings(settings);
        }
    }
}
