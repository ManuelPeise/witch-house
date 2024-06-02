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
                    Id = Guid.NewGuid(),
                    ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.MathUnits),
                    ModuleType = ModuleTypeEnum.MathUnits,
                    CreatedAt = DateTime.Now.ToString("yyyy.MM.dd"),
                    CreatedBy = "System"
                },
                new ModuleEntity
                {
                    Id= Guid.NewGuid(),
                    ModuleName = Enum.GetName(typeof(ModuleTypeEnum), ModuleTypeEnum.GermanUnits),
                    ModuleType=ModuleTypeEnum.GermanUnits,
                    CreatedAt = DateTime.Now.ToString("yyyy.MM.dd"),
                    CreatedBy = "System"
                }
            });
        }
    }
}
