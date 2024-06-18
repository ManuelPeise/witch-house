using Data.Shared.Entities;
using Data.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.Configs.MySql
{
    public class ModuleSeed : IEntityTypeConfiguration<ModuleEntity>
    {
        public void Configure(EntityTypeBuilder<ModuleEntity> builder)
        {
            var userGuid = new Guid(DefaultEntityGuids.DefaultAdminGuid);
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");


            builder.HasData(new List<ModuleEntity>
            {
                new ModuleEntity
                {
                    Id = Guid.NewGuid(),
                    ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTraining),
                    ModuleType = ModuleTypeEnum.SchoolTraining,
                    AccountGuid = userGuid,
                    IsActive = true,
                    SettingsJson = null,
                    CreatedAt = createdAt,
                    CreatedBy = "System",
                    UpdatedAt = createdAt,
                    UpdatedBy = "System",
                },
                new ModuleEntity
                {
                    Id = Guid.NewGuid(),
                    ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTrainingStatistics),
                    ModuleType=ModuleTypeEnum.SchoolTrainingStatistics,
                    AccountGuid = userGuid,
                    IsActive = true,
                    SettingsJson = null,
                    CreatedAt = createdAt,
                    CreatedBy = "System",
                    UpdatedAt = createdAt,
                    UpdatedBy = "System",
                }
            });
        }
    }
}
