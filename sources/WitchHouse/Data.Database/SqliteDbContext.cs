using Data.Database.SeedData;
using Data.Shared.SqLiteEntities;
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
            builder.Entity<UserDataTableEntity>()
                .HasMany(x => x.Modules)
                .WithOne(x => x.UserData)
                .HasForeignKey(e => e.UserId);


            builder.ApplyConfiguration(new RoleSeed());
            builder.ApplyConfiguration(new ModuleDataSeed());
            builder.ApplyConfiguration(new AdminCredentialsSeed(_configuration));
            builder.ApplyConfiguration(new UserSeed(_configuration));
        }

        public DbSet<FamilyEntity> Family { get; set; }
        public DbSet<UserDataTableEntity> UserData { get; set; }
        public DbSet<UserCredentialTableEntity> UserCredentials { get; set; }
        public DbSet<RoleTableEntity> Roles { get; set; }
        public DbSet<LogMessageTableEntity> Log { get; set; }
        public DbSet<UnitResultEntity> UnitResults { get; set; }
        public DbSet<ModuleTableEntity> Modules { get; set; }

    }
}
