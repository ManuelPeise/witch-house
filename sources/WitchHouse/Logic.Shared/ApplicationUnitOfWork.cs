using Data.Database;
using Data.Shared.Entities;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.Shared
{
    public class ApplicationUnitOfWork<TContext> : IApplicationUnitOfWork<TContext> where TContext: DbContext
    {
        private bool disposedValue;

        private readonly TContext _context;
        
        private IGenericRepository<FamilyEntity>? _familyRepository;
        private IGenericRepository<AccountEntity>? _accountRepository;
        private IGenericRepository<CredentialEntity>? _credentialsRepository;
        private IGenericRepository<RoleEntity>? _roleRepository;
        private IGenericRepository<ModuleEntity>? _moduleRepository;
        private IGenericRepository<UnitResultEntity>? _unitResultRepository;
        private readonly IUserDataClaimsAccessor _claimsAccessor;
        private ILogRepository _logRepository;

        public ApplicationUnitOfWork(TContext context, ILogRepository logRepository, IUserDataClaimsAccessor claimsAccessor)
        {
            _context = context;
            _logRepository = logRepository;
            _claimsAccessor = claimsAccessor;

        }
        public IGenericRepository<FamilyEntity> FamilyRepository => _familyRepository ?? new GenericRepository<TContext, FamilyEntity>(_context);
        public IGenericRepository<AccountEntity> AccountRepository => _accountRepository ?? new GenericRepository<TContext, AccountEntity>(_context);
        public IGenericRepository<CredentialEntity> CredentialsRepository => _credentialsRepository ?? new GenericRepository<TContext, CredentialEntity>(_context);
        public IGenericRepository<RoleEntity> RoleRepository => _roleRepository ?? new GenericRepository<TContext, RoleEntity>(_context);
        public IGenericRepository<ModuleEntity> ModuleRepository => _moduleRepository ?? new GenericRepository<TContext, ModuleEntity>(_context);
        public IGenericRepository<UnitResultEntity> UnitResultRepository => _unitResultRepository ?? new GenericRepository<TContext, UnitResultEntity>(_context);
        public ILogRepository LogRepository => _logRepository;
        public IUserDataClaimsAccessor ClaimsAccessor => _claimsAccessor;
        
        public async Task SaveChanges()
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
