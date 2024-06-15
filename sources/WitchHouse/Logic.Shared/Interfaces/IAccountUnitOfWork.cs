using Data.Database;
using Data.Shared.Entities;

namespace Logic.Shared.Interfaces
{
    public interface IAccountUnitOfWork : IDisposable
    {
        DatabaseContext DatabaseContext { get; }
        ILogRepository LogRepository { get; }
        IUserDataClaimsAccessor ClaimsAccessor { get; }
        IGenericRepository<FamilyEntity> FamilyRepository { get; }
        IGenericRepository<AccountEntity> AccountRepository { get; }
        IGenericRepository<ModuleEntity> ModuleRepository { get; }
        IGenericRepository<UserModuleEntity> UserModuleRepository { get; }
        Task SaveChanges();

    }
}
