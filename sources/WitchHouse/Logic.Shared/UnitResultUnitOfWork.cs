using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logic.Shared
{
    public class UnitResultUnitOfWork : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly ILogRepository _logRepository;

        private IGenericRepository<UnitResultEntity> _unitResultRepo;
        private bool disposedValue;


        public UnitResultUnitOfWork(ILogRepository logRepository, DatabaseContext context)
        {
            _logRepository = logRepository;
            _context = context;
            _unitResultRepo = new GenericRepository<UnitResultEntity>(context);
        }

        public async Task<List<UnitResultEntity>> GetUnitResults(Guid userId, DateTime from, DateTime to)
        {
           var results =  await _unitResultRepo.GetByAsync(x => x.UserId == userId);

            if(!results.ToList().Any())
            {
                return new List<UnitResultEntity>();
            }

            return results.Where(x => x.TimeStamp != null && DateTime.Parse(x.TimeStamp) >= from && DateTime.Parse(x.TimeStamp) <= to).ToList();
        }

        public async Task SaveUnitResults(List<UnitResultImportModel> importModels, CurrentUser? currentUser = null)
        {
            foreach (var importModel in importModels)
            {
                var entity = importModel.ToEntity();

                await _unitResultRepo.AddAsync(entity);
            }

            await SaveChanges(currentUser);
        }

        public async Task SaveUnitResult(UnitResultImportModel importModel, CurrentUser? currentUser = null)
        {
            var entity = importModel.ToEntity();

            await _unitResultRepo.AddAsync(entity);
        }

        public async Task SaveChanges(CurrentUser? currentUser = null)
        {
            var modifiedEntries = _context.ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Modified ||
               x.State == EntityState.Added);

            foreach (var entry in modifiedEntries)
            {
                if (entry != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((AEntityBase)entry.Entity).CreatedBy = currentUser?.UserName ?? "System";
                        ((AEntityBase)entry.Entity).CreatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);

                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        ((AEntityBase)entry.Entity).UpdatedBy = currentUser?.UserName ?? "System";
                        ((AEntityBase)entry.Entity).UpdatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _logRepository.Dispose();
                    _unitResultRepo.Dispose();
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
