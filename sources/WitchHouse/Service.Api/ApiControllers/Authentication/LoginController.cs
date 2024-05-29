using Data.Database;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Logic.Authentication;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Shared;

namespace Service.Api.ApiControllers.Authentication
{
    public class LoginController: ApiControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogRepository _logRepository;
        private readonly IConfiguration _config;

        public LoginController(DatabaseContext databaseContext, IConfiguration config, ILogRepository logRepository)
        {
            _databaseContext = databaseContext;
            _logRepository = logRepository;
            _config = config;
        }

        [HttpPost(Name = "AccountLogin")]
        public async Task<LoginResult> AccountLogin([FromBody] AccountLoginModel model)
        {
            var currentUser = GetCurrentUser();
            var service = new AuthenticationService(_databaseContext, _logRepository, currentUser);

            return await service.LogIn(_config, model);
        }
    }
}
