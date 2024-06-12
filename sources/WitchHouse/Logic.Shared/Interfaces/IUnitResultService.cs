using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;

namespace Logic.Shared.Interfaces
{
    public interface IUnitResultService: IDisposable
    {
        Task<List<UnitResultStaticticModel>?> GetUnitStatistic(UnitResultStatisticRequest request, CurrentUser currentUser);
        Task SaveUnitResult(UnitResultImportModel importModel, CurrentUser currentUser);
        Task SaveUnitResults(List<UnitResultImportModel> importModels, CurrentUser currentUser);
    }
}
