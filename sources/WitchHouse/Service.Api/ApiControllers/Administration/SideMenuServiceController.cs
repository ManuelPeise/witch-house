using Data.Shared.Models.Export.SideMenu;
using Logic.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Administration
{
    [Authorize]
    public class SideMenuServiceController: ApiControllerBase
    {
        [HttpGet(Name ="GetSideMenu")]
        public async Task<SideMenuExportModel> GetSideMenu()
        {
            var menu = new SideMenu();

            return await Task.FromResult(menu.GetSideMenu());
        }
    }
}
