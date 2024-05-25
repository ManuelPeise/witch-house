using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions opt) : base(opt) { }

        public DbSet<LogMessageEntity> MessageLogTable { get; set; }
        public DbSet<FamilyEntity> FamilyTable { get; set; }
        public DbSet<AccountEntity> AccountTable { get; set; }
    }
}
