using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Training
{
    [Authorize]
    public class TrainingResultService : ApiControllerBase
    {
        private readonly IUnitResultService _unitResultService;


        public TrainingResultService(IUnitResultService unitResultService) : base()
        {
            _unitResultService = unitResultService;
        }

        [HttpGet("{userId}",Name = "GetLastUnitResultStatistics")]
        public async Task<List<UnitResultStaticticModel>?> GetLastUnitResultStatistics(string userId)
        {
            return await _unitResultService.GetLastUnitStatistics(userId);
        }

        [HttpPost(Name = "SaveUnitResult")]
        public async Task SaveUnitResult([FromBody] UnitResultImportModel model)
        {
            await _unitResultService.SaveUnitResult(model);
        }

        [HttpPost(Name = "SaveUnitResults")]
        public async Task SaveUnitResults([FromBody] List<UnitResultImportModel> models)
        {
            await _unitResultService.SaveUnitResults(models);
        }
    }
}
