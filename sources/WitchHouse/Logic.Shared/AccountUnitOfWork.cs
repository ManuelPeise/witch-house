using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.Shared
{
    public class AccountUnitOfWork: IAccountUnitOfWork
    {
        private bool disposedValue;
        private readonly DatabaseContext _databaseContext;

        private ILogRepository? _logRepository;
        private IGenericRepository<FamilyEntity>? _familyRepository;
        private IGenericRepository<AccountEntity>? _accountRepository;
        private IGenericRepository<ModuleEntity>? _moduleRepository;
        private IGenericRepository<UserModuleEntity>? _userModuleRepository;
        private readonly IUserDataClaimsAccessor _claimsAccessor;
        public AccountUnitOfWork(DatabaseContext databaseContext, IUserDataClaimsAccessor claimsAccessor)
        {
            _databaseContext = databaseContext;
            _claimsAccessor = claimsAccessor;
        }

        public ILogRepository LogRepository => _logRepository ?? new LogRepository(_databaseContext, _claimsAccessor?? new UserDataClaimsAccessor());
        public IGenericRepository<FamilyEntity> FamilyRepository => _familyRepository ?? new GenericRepository<FamilyEntity>(_databaseContext);
        public IGenericRepository<AccountEntity> AccountRepository => _accountRepository ?? new GenericRepository<AccountEntity>(_databaseContext);
        public IGenericRepository<ModuleEntity> ModuleRepository => _moduleRepository ?? new GenericRepository<ModuleEntity>(_databaseContext);
        public IGenericRepository<UserModuleEntity> UserModuleRepository => _userModuleRepository ?? new GenericRepository<UserModuleEntity>(_databaseContext);
        public DatabaseContext DatabaseContext { get => _databaseContext; }
        public IUserDataClaimsAccessor ClaimsAccessor => _claimsAccessor?? new UserDataClaimsAccessor();

        public async Task SaveChanges()
        {
            var modifiedEntries = DatabaseContext.ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Modified ||
               x.State == EntityState.Added);

            foreach (var entry in modifiedEntries)
            {
                if (entry != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((AEntityBase)entry.Entity).CreatedBy = ClaimsAccessor.GetClaimsValue<string>(UserIdentityClaims.UserName) ?? "System";
                        ((AEntityBase)entry.Entity).CreatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);

                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        ((AEntityBase)entry.Entity).UpdatedBy = ClaimsAccessor.GetClaimsValue<string>(UserIdentityClaims.UserName) ?? "System";
                        ((AEntityBase)entry.Entity).UpdatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);
                    }
                }
            }

            await DatabaseContext.SaveChangesAsync();
        }

        #region dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   _databaseContext.Dispose();
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
