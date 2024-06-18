using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;
using Logic.Authentication.Interfaces;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Logic.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public AuthenticationService(IApplicationUnitOfWork unitOfWork) : base()
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMessage<LoginResult>> LogIn(IConfiguration config, AccountLoginModel loginModel)
        {
            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetByAsync(acc => acc.UserName.ToLower() == loginModel.UserName.ToLower());

                if (accounts.Count() > 1)
                {
                    throw new Exception($"Found multiple accounts with [UserName] {loginModel.UserName}!");
                }

                var accountEntity = accounts.FirstOrDefault();

                if (accountEntity != null && accountEntity.CredentialGuid != null)
                {
                    var credentialsEntity = await _unitOfWork.CredentialsRepository.GetFirstByIdAsync((Guid)accountEntity.CredentialGuid);

                    if (credentialsEntity == null)
                    {
                        throw new Exception("Credentials entity not found!");
                    }

                    var encodedPassword = Helpers.GetEncodedSecret(loginModel.Secret, credentialsEntity.Salt.ToString());

                    if (encodedPassword == credentialsEntity.EncodedPassword)
                    {
                        var tokenGenerator = new JwtTokenGenerator();

                        var (jwt, refreshToken) = tokenGenerator
                            .GenerateToken(config, LoadUserClaims(accountEntity), 100);

                        credentialsEntity.JwtToken = jwt;
                        credentialsEntity.RefreshToken = refreshToken;

                        await _unitOfWork.AccountRepository.Update(accountEntity);

                        await _unitOfWork.SaveChanges();

                        var modules = await _unitOfWork.ModuleRepository
                            .GetByAsync(x => x.AccountGuid == accountEntity.Id);

                        return new ResponseMessage<LoginResult>
                        {
                            Success = true,
                            StatusCode = 200,
                            MessageKey = "",
                            Data = new LoginResult
                            {
                                UserData = new UserData
                                {
                                    UserId = accountEntity.Id,
                                    FamilyGuid = accountEntity.FamilyGuid,
                                    UserName = accountEntity.UserName,
                                    Language = accountEntity.Culture,
                                    UserRoles = (from role in accountEntity.UserRoles
                                                 select role.RoleType).ToList(),
                                    ProfileImage = accountEntity.ProfileImage,
                                },
                                JwtData = new JwtData
                                {
                                    JwtToken = jwt,
                                    RefreshToken = refreshToken,
                                },
                                Modules = (from module in modules
                                           select new UserModule
                                           {
                                               UserId = accountEntity.Id,
                                               ModuleId = module.Id,
                                               ModuleType = module.ModuleType,
                                               ModuleSettings = module.SettingsJson,
                                               IsActive = module.IsActive,
                                           }).ToList()

                            }
                        };
                    }
                }

                return new ResponseMessage<LoginResult>
                {
                    Success = false,
                    StatusCode = 200,
                    MessageKey = "labelLoginFailed",
                    Data = null
                };

            }
            catch (Exception exception)
            {
                await _unitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
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

        public async Task<ResponseMessage<MobileLoginResult>> MobileLoginRequest(IConfiguration config, MobileLoginRequestModel requestModel)
        {
            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetByAsync(acc => acc.UserName.ToLower() == requestModel.UserName.ToLower());

                if (accounts.Count() > 1)
                {
                    throw new Exception($"Found multiple accounts with [UserName] {requestModel.UserName}!");
                }

                var accountEntity = accounts.FirstOrDefault();

                if (accountEntity != null && accountEntity.CredentialGuid != null)
                {
                    var credentialsEntity = await _unitOfWork.CredentialsRepository.GetFirstByIdAsync((Guid)accountEntity.CredentialGuid);

                    if (credentialsEntity == null)
                    {
                        throw new Exception("Credentials entity not found!");
                    }

                    var encodedPassword = Helpers.GetEncodedSecret(requestModel.Password, credentialsEntity.Salt.ToString());

                    if (encodedPassword == credentialsEntity.EncodedPassword)
                    {
                        var tokenGenerator = new JwtTokenGenerator();

                        var (jwt, refreshToken) = tokenGenerator.GenerateToken(config, LoadUserClaims(accountEntity), 100);

                        credentialsEntity.JwtToken = jwt;
                        credentialsEntity.RefreshToken = refreshToken;

                        await _unitOfWork.AccountRepository.Update(accountEntity);

                        await _unitOfWork.SaveChanges();

                        return new ResponseMessage<MobileLoginResult>
                        {

                            Success = true,
                            StatusCode = 200,
                            MessageKey = "common:labelLoginSuccess",
                            Data = new MobileLoginResult
                            {
                                JwtData = new JwtData
                                {
                                    JwtToken = jwt,
                                    RefreshToken = refreshToken,
                                },
                                UserData = new UserData
                                {
                                    UserId = accountEntity.Id,
                                    FamilyGuid = accountEntity.FamilyGuid,
                                    UserName = accountEntity.UserName,
                                    Language = accountEntity.Culture,
                                    UserRoles = (from role in accountEntity.UserRoles
                                                 select role.RoleType).ToList(),
                                    ProfileImage = accountEntity.ProfileImage,
                                },
                            }
                        };
                    }

                }

                return new ResponseMessage<MobileLoginResult>
                {
                    Success = false,
                    StatusCode = 401,
                    MessageKey = "common:labelAccessDenied",
                    Data = null
                };

            }
            catch (Exception exception)
            {
                await _unitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                    Trigger = nameof(AuthenticationService)
                });

                return new ResponseMessage<MobileLoginResult>
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
                    new Claim(UserIdentityClaims.UserId, account.Id.ToString()),
                    new Claim(UserIdentityClaims.FamilyId, account.FamilyGuid.ToString()??""),
                    new Claim(UserIdentityClaims.UserName, account.UserName),
                    new Claim(UserIdentityClaims.FirstName, account.FirstName??""),
                    new Claim(UserIdentityClaims.LastName, account.LastName??""),
                    // new Claim(UserIdentityClaims.UserRole, Enum.GetName(account.Role)??""),
                    // TODO Add Modules
            };
        }
    }
}
