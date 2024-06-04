using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class ModuleService : ApiControllerBase
    {
        private readonly IModuleConfigurationService _moduleConfiguration;
        private readonly CurrentUser _currentUser;
        public ModuleService(IModuleConfigurationService moduleConfiguration, IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _moduleConfiguration = moduleConfiguration;
            _currentUser = GetCurrentUser();
        }

        [HttpPost(Name = "LoadModuleConfiguration")]
        public async Task<ModuleConfiguration> LoadModuleConfiguration([FromBody] UserModuleRequestModel requestModel)
        {
            return await _moduleConfiguration.LoadUserModuleConfiguration(requestModel, _currentUser);
        }

        [HttpGet(Name = "LoadModuleSettings")]
        public async Task<List<ModuleSettings>> LoadModuleSettings([FromQuery] Guid userGuid)
        {
            return await _moduleConfiguration.LoadActiveModuleSettings(userGuid, _currentUser);
        }

        [HttpPost(Name = "UpdateModuleConfiguration")]
        public async Task UpdateModuleConfiguration([FromBody] UserModule module)
        {
            await _moduleConfiguration.UpdateModule(module, _currentUser);
        }
    }
}
