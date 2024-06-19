using Data.Database.Configs.MySql;
using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }

        public DbSet<LogMessageEntity> MessageLogTable { get; set; }
        public DbSet<FamilyEntity> FamilyTable { get; set; }
        public DbSet<CredentialEntity> CredentialsTable { get; set; }
        public DbSet<RoleEntity> RolesTable { get; set; }
        public DbSet<AccountEntity> AccountTable { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<UnitResultEntity> UnitResults { get; set; }
        public DbSet<DataSyncEntity> DataSyncTable { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserRoleSeed());
            builder.ApplyConfiguration(new ModuleSeed());
            builder.ApplyConfiguration(new CredentialConfiguration(_configuration));
            builder.ApplyConfiguration(new AccountConfiguration(_configuration));
        }
    }
}
