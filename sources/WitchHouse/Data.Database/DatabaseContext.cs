using Data.Database.SeedData;
using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(DbContextOptions opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }

        public DbSet<LogMessageEntity> MessageLogTable { get; set; }
        public DbSet<FamilyEntity> FamilyTable { get; set; }
        public DbSet<AccountEntity> AccountTable { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<UserModuleEntity> UserModuleSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AdminAccountSeed(_configuration));
            builder.ApplyConfiguration(new ModuleSeed());

        }
    }
}
