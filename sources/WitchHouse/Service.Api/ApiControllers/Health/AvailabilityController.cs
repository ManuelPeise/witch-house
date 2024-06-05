using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Health
{
    public class AvailabilityController : ApiControllerBase
    {
        public AvailabilityController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        [HttpGet(Name = "ApiIsAvailable")]
        public async Task<bool> ApiIsAvailable()
        {
            return await Task.FromResult(true);
        }
    }
}
