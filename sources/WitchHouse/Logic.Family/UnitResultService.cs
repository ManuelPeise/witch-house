using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Family.Interfaces;
using Logic.Shared;
using Logic.Shared.Interfaces;
using System.Globalization;

namespace Logic.Family
{
    public class UnitResultService : IUnitResultService
    {
        private readonly IApplicationUnitOfWork<DatabaseContext> _applicationUnitOfWork;

        private bool disposedValue;

        public UnitResultService(IApplicationUnitOfWork<DatabaseContext> applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task<List<UnitResultStaticticModel>?> GetLastUnitStatistics(string userId)
        {
            try
            {
                if (!Guid.TryParse(userId, out var userGuid))
                {
                    throw new Exception("Could not parse submitted string to userGuid!");
                }

                var entries = await GetLastUnitResults(userGuid);

                var statistics = (from entry in entries
                                  group entry by new { UnitType = entry.UnitType } into unitTypeStatisticGroup
                                  select new UnitResultStaticticModel
                                  {
                                      UnitType = unitTypeStatisticGroup.Key.UnitType,
                                      Entries = (from entry in unitTypeStatisticGroup
                                                 where entry.TimeStamp != null
                                                 group entry by DateTime.Parse(entry.CreatedAt).Date into unitStatisticGroup
                                                 select new UnitResultStatisticEntry
                                                 {
                                                     TimeStamp = unitStatisticGroup.Key.ToString("dd-MM"),
                                                     Success = unitStatisticGroup.Sum(x => x.Success),
                                                     Failed = unitStatisticGroup.Sum(x => x.Failed)
                                                 }).OrderBy(x => x.TimeStamp).ToList()
                                  }).ToList();

                return statistics;

            }
            catch (Exception exception)
            {


                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(UnitResultService),
                });

                return null;
            }
        }

        public async Task SaveUnitResult(UnitResultImportModel importModel)
        {
            try
            {
                var entity = importModel.ToEntity();

                await _applicationUnitOfWork.UnitResultRepository.AddAsync(entity);

                await _applicationUnitOfWork.SaveChanges();

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(UnitResultService),
                });
            }
        }

        public async Task SaveUnitResults(List<UnitResultImportModel> importModels)
        {
            try
            {
                foreach (var importModel in importModels)
                {
                    var entity = importModel.ToEntity();

                    await _applicationUnitOfWork.UnitResultRepository.AddAsync(entity);
                }

                await _applicationUnitOfWork.SaveChanges();

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _applicationUnitOfWork.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(UnitResultService),
                });
            }
        }

        private async Task<List<UnitResultEntity>> GetUnitResults(Guid userId, DateTime from, DateTime to)
        {
            var results = await _applicationUnitOfWork.UnitResultRepository.GetByAsync(x => x.UserId == userId);

            if (!results.ToList().Any())
            {
                return new List<UnitResultEntity>();
            }

            return results.Where(x => x.TimeStamp != null && DateTime.Parse(x.TimeStamp) >= from && DateTime.Parse(x.TimeStamp) <= to).ToList();
        }

        private async Task<List<UnitResultEntity>> GetLastUnitResults(Guid userId)
        {
            var results = await _applicationUnitOfWork.UnitResultRepository.GetByAsync(x => x.UserId == userId);

            if (!results.ToList().Any())
            {
                return new List<UnitResultEntity>();
            }

            var currentDate = DateTime.UtcNow;
            return results.Where(x => x.TimeStamp != null &&
                GetParsedDate(x.CreatedAt) >= currentDate.AddDays(-15) &&
                GetParsedDate(x.CreatedAt) <= currentDate.AddDays(1)).ToList();
        }

        private DateTime GetParsedDate(string timeStamp)
        {
            var allowedFormats = new List<string>
            {
                "yyyy-MM-dd HH:mm",
                "yyyy-MM-dd",
                "yyyy-M-dd",
                "yyyy-MM-d"
            };

            if (DateTime.TryParseExact(timeStamp, allowedFormats.ToArray(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            return DateTime.MinValue;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _applicationUnitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
