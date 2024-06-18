using Data.Shared.Models.Export;
using Data.Shared.Models.Import;

namespace Logic.Family.Interfaces
{
    public interface IUnitResultService: IDisposable
    {
        Task<List<UnitResultStaticticModel>?> GetLastUnitStatistics(string userId);
        Task SaveUnitResult(UnitResultImportModel importModel);
        Task SaveUnitResults(List<UnitResultImportModel> importModels);
    }
}
