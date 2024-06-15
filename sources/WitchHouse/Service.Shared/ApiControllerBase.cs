using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Shared
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ApiControllerBase : ControllerBase
    {
       
        protected ApiControllerBase()
        {
            
        }

    }
}
