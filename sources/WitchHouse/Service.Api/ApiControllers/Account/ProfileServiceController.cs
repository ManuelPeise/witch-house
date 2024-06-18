using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Family;
using Logic.Family.Interfaces;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Account
{
    [Authorize]
    public class ProfileServiceController : ApiControllerBase
    {
       private readonly IFamilyAccountService _familyAccountService;
       
        public ProfileServiceController(IFamilyAccountService familyAccountService) : base() 
        {
            _familyAccountService = familyAccountService;
        }

        [HttpGet("{userid}", Name = "GetProfile")]
        public async Task<ProfileExportModel?> GetProfile(string userid)
        {
            return await _familyAccountService.GetProfile(userid);
        }

        [HttpPost(Name = "CheckPassword")]
        public async Task<bool> CheckPassword([FromBody]PasswordUpdateModel model)
        {
            return await _familyAccountService.CheckPassword(model);
        }

        [HttpPost(Name = "UpdatePassword")]
        public async Task<bool> UpdatePassword([FromBody] PasswordUpdateModel model)
        {
            return await _familyAccountService.UpdatePassword(model);
        }

        [HttpPost(Name = "UpdateProfile")]
        public async Task UpdateProfile([FromBody] ProfileImportModel importModel)
        {
            await _familyAccountService.UpdateProfile(importModel);
        }

        [HttpPost(Name = "UploadProfileImage")]
        public async Task UploadProfileImage([FromBody] ProfileImageUploadModel importModel)
        {
            await _familyAccountService.UploadProfileImage(importModel);
        }
    }
}
