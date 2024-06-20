using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.Configs.MySql
{
    public class SyncTableSeed : IEntityTypeConfiguration<DataSyncEntity>
    {
        public void Configure(EntityTypeBuilder<DataSyncEntity> builder)
        {
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new DataSyncEntity
            {
                Id = Guid.NewGuid(),
                UserGuid = new Guid(DefaultEntityGuids.DefaultAdminGuid),
                LastSync = DateTime.UtcNow,
                CreatedAt = createdAt,
                CreatedBy = "System",
                UpdatedAt = createdAt,
                UpdatedBy = "System",
            });
        }
    }
}
