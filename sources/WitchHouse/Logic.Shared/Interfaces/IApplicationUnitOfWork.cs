using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logic.Shared.Interfaces
{
    public interface IApplicationUnitOfWork<TContext>: IDisposable where TContext: DbContext
    {
        public IGenericRepository<FamilyEntity> FamilyRepository { get; }
        public IGenericRepository<AccountEntity> AccountRepository { get; }
        public IGenericRepository<CredentialEntity> CredentialsRepository { get; }
        public IGenericRepository<RoleEntity> RoleRepository { get; }
        public IGenericRepository<ModuleEntity> ModuleRepository { get; }
        public IGenericRepository<UnitResultEntity> UnitResultRepository { get; }
        public IGenericRepository<DataSyncEntity> SyncRepository { get; }
        public IUserDataClaimsAccessor ClaimsAccessor { get; }
        public ILogRepository LogRepository { get; }
        Task SaveChanges();
    }
}
