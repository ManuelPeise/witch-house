using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Authentication;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Shared;

namespace Service.Api.ApiControllers.Authentication
{
    public class LoginController: ApiControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountUnitOfWork _unitOfWork;
        public LoginController(IAccountUnitOfWork accountUnitOfWork, IConfiguration config, IHttpContextAccessor contextAccessor): base(contextAccessor)
        {
            _unitOfWork = accountUnitOfWork;
            _config = config;
        }

        [HttpPost(Name = "AccountLogin")]
        public async Task<LoginResult> AccountLogin([FromBody] AccountLoginModel model)
        {
            var service = new AuthenticationService(_unitOfWork);

            return await service.LogIn(_config, model);
        }

        [HttpPost(Name = "MobileAccountLogin")]
        public async Task<LoginResult> MobileAccountLogin([FromBody] MobileLoginRequestModel model)
        {
            var service = new AuthenticationService(_unitOfWork);

            return await service.MobileLoginRequest(_config, model);
        }
    }
}
