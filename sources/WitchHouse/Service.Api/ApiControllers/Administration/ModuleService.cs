using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
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

        [HttpGet(Name = "LoadSchoolModule")]
        public async Task<ResponseMessage<SchoolModule>> LoadSchoolModule([FromQuery] Guid userGuid)
        {
            var response = await _moduleConfiguration.LoadSchoolModule(userGuid);
            return response;
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
