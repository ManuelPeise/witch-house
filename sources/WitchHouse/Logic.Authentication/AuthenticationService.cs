using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Logic.Authentication
{
    public class AuthenticationService: LogicBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogRepository _logRepository;

        public AuthenticationService(DatabaseContext databaseContext, ILogRepository logRepository, CurrentUser currentUser):base(databaseContext, currentUser)
        {
            _databaseContext = databaseContext;
            _logRepository = logRepository;
        }

        public async Task<LoginResult> LogIn(IConfiguration config, AccountLoginModel loginModel)
        {
            try
            {
                using (var unitOfWork = new AccountUnitOfWork(_databaseContext))
                {
                    var accounts = await unitOfWork.AccountRepository.GetByAsync(acc => acc.UserName.ToLower() == loginModel.UserName.ToLower());

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

                            await unitOfWork.AccountRepository.Update(accountEntity);

                            await SaveChanges();

                            return new LoginResult
                            {
                                Success = true,
                                UserId = accountEntity.Id,
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
            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                    Trigger = nameof(AuthenticationService)
                });

                await SaveChanges();

                return new LoginResult
                {
                    Success = false,
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
