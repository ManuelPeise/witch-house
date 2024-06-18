using Data.Shared.Entities;

namespace Logic.Shared.Interfaces
{
    public interface IApplicationUnitOfWork: IDisposable
    {
        public IGenericRepository<FamilyEntity> FamilyRepository { get; }
        public IGenericRepository<AccountEntity> AccountRepository { get; }
        public IGenericRepository<CredentialEntity> CredentialsRepository { get; }
        public IGenericRepository<RoleEntity> RoleRepository { get; }
        public IGenericRepository<ModuleEntity> ModuleRepository { get; }
        public IGenericRepository<UnitResultEntity> UnitResultRepository { get; }
        public IUserDataClaimsAccessor ClaimsAccessor { get; }
        public ILogRepository LogRepository { get; }
        Task SaveChanges();
    }
}
