using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
using Logic.Authentication;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Shared;

namespace Service.Api.ApiControllers.Authentication
{
    public class LoginController: ApiControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly IModuleConfigurationService _moduleService;
        public LoginController(IAccountUnitOfWork accountUnitOfWork, IModuleConfigurationService moduleService, IConfiguration config): base()
        {
            _unitOfWork = accountUnitOfWork;
            _config = config;
            _moduleService = moduleService;
        }

        [HttpPost(Name = "AccountLogin")]
        public async Task<LoginResult> AccountLogin([FromBody] AccountLoginModel model)
        {
            var service = new AuthenticationService(_unitOfWork, _moduleService);

            return await service.LogIn(_config, model);
        }

        [HttpPost(Name = "MobileAccountLogin")]
        public async Task<ResponseMessage<MobileLoginResult>> MobileAccountLogin([FromBody] MobileLoginRequestModel model)
        {
            var service = new AuthenticationService(_unitOfWork, _moduleService);

            return await service.MobileLoginRequest(_config, model);
        }
    }
}
