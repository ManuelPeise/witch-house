using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
using Microsoft.Extensions.Configuration;

namespace Logic.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResponseMessage<LoginResult>> LogIn(IConfiguration config, AccountLoginModel loginModel);
        Task<ResponseMessage<MobileLoginResult>> MobileLoginRequest(IConfiguration config, MobileLoginRequestModel requestModel);
    }
}
