using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;

namespace Logic.Shared.Interfaces
{
    public interface IAccountUnitOfWork : IDisposable
    {
        DatabaseContext DatabaseContext { get; }
        ILogRepository LogRepository { get; }
        IGenericRepository<FamilyEntity> FamilyRepository { get; }
        IGenericRepository<AccountEntity> AccountRepository { get; }
        IGenericRepository<ModuleEntity> ModuleRepository { get; }
        IGenericRepository<UserModuleEntity> UserModuleRepository { get; }
        Task SaveChanges(CurrentUser? currentUser = null);

    }
}
