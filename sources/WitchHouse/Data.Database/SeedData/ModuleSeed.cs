using Data.Shared.Entities;
using Data.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.SeedData
{
    public class ModuleSeed : IEntityTypeConfiguration<ModuleEntity>
    {
        public void Configure(EntityTypeBuilder<ModuleEntity> builder)
        {

            builder.HasData(new List<ModuleEntity>
            {
                new ModuleEntity
                {
                    Id = new Guid("c62daaff-4f9b-4283-a1ea-1a9f42df2d99"),
                    ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTraining),
                    ModuleType = ModuleTypeEnum.SchoolTraining,
                    CreatedAt = DateTime.Now.ToString("yyyy.MM.dd"),
                    CreatedBy = "System"
                },
                new ModuleEntity
                {
                    Id= new Guid("cd472d2e-28c9-42b0-9cba-536b0ddb923b"),
                    ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.SchoolTrainingStatistics),
                    ModuleType=ModuleTypeEnum.SchoolTrainingStatistics,
                    CreatedAt = DateTime.Now.ToString("yyyy.MM.dd"),
                    CreatedBy = "System"
                }
            });
        }
    }
}
