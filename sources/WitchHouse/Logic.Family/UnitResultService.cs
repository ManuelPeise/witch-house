using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared;
using Logic.Shared.Interfaces;

namespace Logic.Family
{
    public class UnitResultService : IUnitResultService
    {
        private readonly DatabaseContext _context;
        private readonly ILogRepository _logRepository;
        
        private bool disposedValue;

        public UnitResultService(ILogRepository logRepository, DatabaseContext context)
        {
            _context = context;
            _logRepository = logRepository;
        }

        public async Task<List<UnitResultStaticticModel>?> GetLastUnitStatistics(string userId)
        {
            try
            {
                if(!Guid.TryParse(userId, out var userGuid))
                {
                    throw new Exception("Could not parse submitted string to userGuid!");
                }

                using (var unitOfWork = new UnitResultUnitOfWork(_logRepository, _context))
                {
                    var entries = await unitOfWork.GetLastUnitResults(userGuid);

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
            }
            catch (Exception exception)
            {
               

                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _logRepository.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
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
                using (var unitOfWork = new UnitResultUnitOfWork(_logRepository, _context))
                {
                    await unitOfWork.SaveUnitResult(importModel);

                    await unitOfWork.SaveChanges();
                }

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _logRepository.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
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
                using (var unitOfWork = new UnitResultUnitOfWork(_logRepository, _context))
                {
                    await unitOfWork.SaveUnitResults(importModels);

                    await unitOfWork.SaveChanges();
                }

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _logRepository.ClaimsAccessor.GetClaimsValue<Guid>(UserIdentityClaims.FamilyId),
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.UtcNow.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(UnitResultService),
                });
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _logRepository.Dispose();
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
