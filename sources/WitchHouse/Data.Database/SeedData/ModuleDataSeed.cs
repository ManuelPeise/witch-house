using Data.Shared.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.Shared.SqLiteEntities;

namespace Data.Database.SeedData
{
    public class ModuleDataSeed : IEntityTypeConfiguration<ModuleTableEntity>
    {
        public void Configure(EntityTypeBuilder<ModuleTableEntity> builder)
        {
            var userGuid = new Guid(DefaultEntityGuids.DefaultAdminGuid);
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new List<ModuleTableEntity>
            {
                    new ModuleTableEntity
                    {
                         Id = Guid.NewGuid(),
                         ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTraining),
                         ModuleType = ModuleTypeEnum.SchoolTraining,
                         UserId = userGuid,
                         CreatedAt = createdAt,
                         CreatedBy = "System",
                         UpdatedAt = createdAt,
                         UpdatedBy = "System",
                    },
                    new ModuleTableEntity
                    {
                         Id = Guid.NewGuid(),
                         ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTrainingStatistics),
                         ModuleType=ModuleTypeEnum.SchoolTrainingStatistics,
                         UserId = userGuid,
                         CreatedAt = createdAt,
                         CreatedBy = "System",
                         UpdatedAt = createdAt,
                         UpdatedBy = "System",
                    }
            });
        }
    }
}
