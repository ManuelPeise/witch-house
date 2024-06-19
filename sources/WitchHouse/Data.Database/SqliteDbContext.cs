using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Database
{
    public class SqliteDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SqliteDbContext(DbContextOptions<SqliteDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
        }

        public DbSet<LogMessageEntity> MessageLogTable { get; set; }
        public DbSet<FamilyEntity> FamilyTable { get; set; }
        public DbSet<CredentialEntity> CredentialsTable { get; set; }
        public DbSet<RoleEntity> RolesTable { get; set; }
        public DbSet<AccountEntity> AccountTable { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<UnitResultEntity> UnitResults { get; set; }
        public DbSet<DataSyncEntity> DataSyncTable { get; set; }

    }
}
