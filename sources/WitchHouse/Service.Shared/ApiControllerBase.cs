using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Service.Shared
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ApiControllerBase: ControllerBase
    {
        protected ApiControllerBase()
        {
            
        }

        protected CurrentUser GetCurrentUser()
        {
            var claims = HttpContext.User.Claims;

            var userGuid = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value ?? string.Empty;
            var userName = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value ?? string.Empty;
            var userRole = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault()?.Value ?? string.Empty;
            var familyGuid = claims.Where(x => x.Type == "FamilyGuid").FirstOrDefault()?.Value ?? string.Empty;

            return new CurrentUser
            {
                UserGuid = !string.IsNullOrWhiteSpace(userGuid) ? new Guid(userGuid) : null,
                FamilyGuid = !string.IsNullOrWhiteSpace(userGuid) ? new Guid(familyGuid) : null,
                UserName = userName,
                UserRole = !string.IsNullOrWhiteSpace(userRole) ? (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), userRole) : null
            };

        }
    }
}
