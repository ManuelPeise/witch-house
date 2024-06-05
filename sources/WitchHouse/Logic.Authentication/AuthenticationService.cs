using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Logic.Authentication
{
    public class AuthenticationService : LogicBase
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        public AuthenticationService(IAccountUnitOfWork accountUnitOfWork) : base()
        {
            _accountUnitOfWork = accountUnitOfWork;
        }

        public async Task<LoginResult> LogIn(IConfiguration config, AccountLoginModel loginModel)
        {
            try
            {
                var accounts = await _accountUnitOfWork.AccountRepository.GetByAsync(acc => acc.UserName.ToLower() == loginModel.UserName.ToLower());

                if (accounts.Count() > 1)
                {
                    throw new Exception($"Found multiple accounts with [UserName] {loginModel.UserName}!");
                }

                var accountEntity = accounts.FirstOrDefault();

                if (accountEntity != null)
                {
                    var encodedPassword = Helpers.GetEncodedSecret(loginModel.Secret, accountEntity.Salt);

                    if (encodedPassword == accountEntity.Secret)
                    {
                        var tokenGenerator = new JwtTokenGenerator();

                        var (jwt, refreshToken) = tokenGenerator.GenerateToken(config, LoadUserClaims(accountEntity), 100);

                        accountEntity.Token = jwt;
                        accountEntity.RefreshToken = refreshToken;

                        await _accountUnitOfWork.AccountRepository.Update(accountEntity);

                        await _accountUnitOfWork.SaveChanges();

                        return new LoginResult
                        {
                            Success = true,
                            UserId = accountEntity.Id,
                            FamilyGuid = accountEntity.FamilyGuid,
                            UserName = accountEntity.UserName,
                            Language = accountEntity.Culture,
                            Jwt = jwt,
                            RefreshToken = refreshToken,
                            UserRole = accountEntity.Role,
                        };
                    }
                }

                return new LoginResult
                {
                    Success = false,
                };

            }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                    Trigger = nameof(AuthenticationService)
                });

                return new LoginResult
                {
                    Success = false,
                };
            }
        }

        public async Task<ResponseMessage<LoginResult>> MobileLoginRequest(IConfiguration config, MobileLoginRequestModel requestModel)
        {
            try
            {
                var accounts = await _accountUnitOfWork.AccountRepository.GetByAsync(acc => acc.UserName.ToLower() == requestModel.UserName.ToLower());

                if (accounts.Count() > 1)
                {
                    throw new Exception($"Found multiple accounts with [UserName] {requestModel.UserName}!");
                }

                var accountEntity = accounts.FirstOrDefault();

                if (accountEntity != null)
                {
                    var appModules = await _accountUnitOfWork.UserModuleRepository.GetByAsync(x => x.UserId == accountEntity.Id && x.ModuleType == ModuleTypeEnum.MobileApp);

                    if (appModules.FirstOrDefault() == null || !appModules.First().IsActive)
                    {
                        return new ResponseMessage<LoginResult>
                        {
                            Success = false,
                            StatusCode = 401,
                            MessageKey = "common:labelAccessDenied",
                            Data = null
                        };
                    }

                    var encodedPin = Helpers.GetEncodedSecret(requestModel.Pin, accountEntity.Salt);

                    if (string.IsNullOrWhiteSpace(accountEntity.Pin))
                    {
                        accountEntity.Pin = encodedPin;

                        await _accountUnitOfWork.SaveChanges();
                    }

                   

                    if (encodedPin == accountEntity.Pin)
                    {
                        var tokenGenerator = new JwtTokenGenerator();

                        var (jwt, refreshToken) = tokenGenerator.GenerateToken(config, LoadUserClaims(accountEntity), 100);

                        accountEntity.Token = jwt;
                        accountEntity.RefreshToken = refreshToken;

                        await _accountUnitOfWork.AccountRepository.Update(accountEntity);

                        await _accountUnitOfWork.SaveChanges();

                        return new ResponseMessage<LoginResult>
                        {

                            Success = true,
                            StatusCode = 200,
                            MessageKey = "common:labelLoginSuccess",
                            Data = new LoginResult
                            {
                                UserId = accountEntity.Id,
                                FamilyGuid = accountEntity.FamilyGuid,
                                UserName = accountEntity.UserName,
                                Language = accountEntity.Culture,
                                Jwt = jwt,
                                RefreshToken = refreshToken,
                                UserRole = accountEntity.Role,
                            }
                        };
                }
            }

                return new ResponseMessage<LoginResult>
                {
                    Success = false,
                    StatusCode = 401,
                    MessageKey = "common:labelAccessDenied",
                    Data = null
                };

        }
            catch (Exception exception)
            {
                await _accountUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                    Trigger = nameof(AuthenticationService)
                });

                return new ResponseMessage<LoginResult>
                {
                    Success = false,
                    StatusCode = 401,
                    MessageKey = "common:labelCheckLoginData",
                    Data = null
                };
            }
        }

        private List<Claim> LoadUserClaims(AccountEntity account)
{
    return new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.UserName),
                    new Claim(ClaimTypes.Role, Enum.GetName(account.Role)),
                    new Claim("FamilyGuid", account.FamilyGuid.ToString()),
                    // new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(account)),
            };
}
    }
}
