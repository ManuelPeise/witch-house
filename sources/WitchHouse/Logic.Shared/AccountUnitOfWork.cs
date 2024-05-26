using Data.Database;
using Data.Shared.Entities;
using Logic.Shared.Interfaces;

namespace Logic.Shared
{
    public class AccountUnitOfWork: IDisposable
    {
        private bool disposedValue;
        private readonly DatabaseContext _databaseContext;

        private ILogRepository? _logRepository;
        private IGenericRepository<FamilyEntity>? _familyRepository;
        private IGenericRepository<AccountEntity>? _accountRepository;

        public AccountUnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ILogRepository LogRepository => _logRepository ?? new LogRepository(_databaseContext);
        public IGenericRepository<FamilyEntity> FamilyRepository => _familyRepository ?? new GenericRepository<FamilyEntity>(_databaseContext);
        public IGenericRepository<AccountEntity> AccountRepository => _accountRepository ?? new GenericRepository<AccountEntity>(_databaseContext);

        public async Task SaveChanges()
        {
            await _databaseContext.SaveChangesAsync();
        }

        #region dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   // _databaseContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
