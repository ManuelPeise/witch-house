using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
using Logic.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Shared;

namespace Service.Api.ApiControllers.Authentication
{
    public class LoginController: ApiControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authService, IConfiguration config) : base()
        {
            _authenticationService = authService;
            _config = config;
        }

        [HttpPost(Name = "AccountLogin")]
        public async Task<ResponseMessage<LoginResult>> AccountLogin([FromBody] AccountLoginModel model)
        {
            return await _authenticationService.LogIn(_config, model);
        }

        [HttpPost(Name = "MobileAccountLogin")]
        public async Task<ResponseMessage<MobileLoginResult>> MobileAccountLogin([FromBody] MobileLoginRequestModel model)
        {
            return await _authenticationService.MobileLoginRequest(_config, model);
        }
    }
}
