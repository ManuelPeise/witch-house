using Data.Shared.Enums;
using Data.Shared.SqLiteEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.SeedData
{
    public class RoleSeed : IEntityTypeConfiguration<RoleTableEntity>
    {
        public void Configure(EntityTypeBuilder<RoleTableEntity> builder)
        {
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new List<RoleTableEntity>
            {
               new RoleTableEntity
               {
                   Id = new Guid(DefaultEntityGuids.UserRoleGuid),
                   RoleType = UserRoleEnum.User,
                   RoleName = Enum.GetName(typeof(UserRoleEnum), UserRoleEnum.User),
                   CreatedAt = createdAt,
                   CreatedBy = "System",
                   UpdatedAt = createdAt,
                   UpdatedBy = "System",
               },
               new RoleTableEntity
               {
                   Id = new Guid(DefaultEntityGuids.LocalAdminRoleGuid),
                   RoleType = UserRoleEnum.LocalAdmin,
                   RoleName = Enum.GetName(typeof(UserRoleEnum), UserRoleEnum.LocalAdmin),
                   CreatedAt = createdAt,
                   CreatedBy = "System",
                   UpdatedAt = createdAt,
               },
               new RoleTableEntity
               {
                   Id = new Guid(DefaultEntityGuids.AdminRoleGuid),
                   RoleType = UserRoleEnum.Admin,
                   RoleName = Enum.GetName(typeof(UserRoleEnum), UserRoleEnum.Admin),
                   CreatedAt = createdAt,
                   CreatedBy = "System",
                   UpdatedAt = createdAt,
                   UpdatedBy = "System",
               },
            });
        }
    }
}
