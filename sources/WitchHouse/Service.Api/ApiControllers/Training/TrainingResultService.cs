using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Training
{
    //[Authorize]
    public class TrainingResultService : ApiControllerBase
    {
        private readonly IUnitResultService _unitResultService;


        public TrainingResultService(IUnitResultService unitResultService, IHttpContextAccessor context) : base(context)
        {
            _unitResultService = unitResultService;
        }

        [HttpGet(Name = "GetUnitResultStatistics")]
        public async Task<List<UnitResultStaticticModel>?> GetUnitResultStatistics([FromQuery] UnitResultStatisticRequest request)
        {
            return await _unitResultService.GetUnitStatistic(request, GetCurrentUser());
        }

        [HttpPost(Name = "SaveUnitResult")]
        public async Task SaveUnitResult([FromBody] UnitResultImportModel model)
        {
            await _unitResultService.SaveUnitResult(model, GetCurrentUser());
        }

        [HttpPost(Name = "SaveUnitResults")]
        public async Task SaveUnitResults([FromBody] List<UnitResultImportModel> models)
        {
            await _unitResultService.SaveUnitResults(models, GetCurrentUser());
        }
    }
}
