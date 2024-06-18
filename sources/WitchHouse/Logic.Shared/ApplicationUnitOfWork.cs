using Data.Database;
using Data.Shared.Entities;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.Shared
{
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private bool disposedValue;

        private readonly DatabaseContext _databaseContext;

        private IGenericRepository<FamilyEntity>? _familyRepository;
        private IGenericRepository<AccountEntity>? _accountRepository;
        private IGenericRepository<CredentialEntity>? _credentialsRepository;
        private IGenericRepository<RoleEntity>? _roleRepository;
        private IGenericRepository<ModuleEntity>? _moduleRepository;
        private IGenericRepository<UnitResultEntity>? _unitResultRepository;
        private readonly IUserDataClaimsAccessor _claimsAccessor;
        private ILogRepository _logRepository;

        public ApplicationUnitOfWork(DatabaseContext databaseContext, ILogRepository logRepository, IUserDataClaimsAccessor claimsAccessor)
        {
            _databaseContext = databaseContext;
            _logRepository = logRepository;
            _claimsAccessor = claimsAccessor;

        }
        public IGenericRepository<FamilyEntity> FamilyRepository => _familyRepository?? new GenericRepository<FamilyEntity>(_databaseContext);
        public IGenericRepository<AccountEntity> AccountRepository => _accountRepository ?? new GenericRepository<AccountEntity>(_databaseContext);
        public IGenericRepository<CredentialEntity> CredentialsRepository => _credentialsRepository ?? new GenericRepository<CredentialEntity>(_databaseContext);
        public IGenericRepository<RoleEntity> RoleRepository => _roleRepository ?? new GenericRepository<RoleEntity>(_databaseContext);
        public IGenericRepository<ModuleEntity> ModuleRepository => _moduleRepository ?? new GenericRepository<ModuleEntity>(_databaseContext);
        public IGenericRepository<UnitResultEntity> UnitResultRepository => _unitResultRepository ?? new GenericRepository<UnitResultEntity>(_databaseContext);
        public ILogRepository LogRepository => _logRepository;
        public IUserDataClaimsAccessor ClaimsAccessor => _claimsAccessor;
        public async Task SaveChanges()
        {
            var modifiedEntries = _databaseContext.ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Modified ||
               x.State == EntityState.Added);

            foreach (var entry in modifiedEntries)
            {
                if (entry != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((AEntityBase)entry.Entity).CreatedBy = _claimsAccessor.GetClaimsValue<string>(UserIdentityClaims.UserName) ?? "System";
                        ((AEntityBase)entry.Entity).CreatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);

                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        ((AEntityBase)entry.Entity).UpdatedBy = _claimsAccessor.GetClaimsValue<string>(UserIdentityClaims.UserName) ?? "System";
                        ((AEntityBase)entry.Entity).UpdatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);
                    }
                }
            }

            await _databaseContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _databaseContext.Dispose();
                    _logRepository.Dispose();
                    _familyRepository?.Dispose();
                    _accountRepository?.Dispose();
                    _credentialsRepository?.Dispose();
                    _roleRepository?.Dispose();
                    _moduleRepository?.Dispose();
                    _unitResultRepository?.Dispose();
                    
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
