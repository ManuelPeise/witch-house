using Data.Shared.Entities;
using Data.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.Configs.MySql
{
    public class UserRoleSeed : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            var userGuid = new Guid(DefaultEntityGuids.DefaultAdminGuid);
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new RoleEntity
            {
                Id = Guid.NewGuid(),
                AccountGuid = userGuid,
                RoleType = UserRoleEnum.Admin,
                RoleName = Enum.GetName(typeof(UserRoleEnum), UserRoleEnum.Admin),
                CreatedAt = createdAt,
                CreatedBy = "System",
                UpdatedAt = createdAt,
                UpdatedBy = "System",
            });
        }
    }
}
