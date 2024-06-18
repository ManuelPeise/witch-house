using Data.Shared.Enums;
using Data.Shared.SqLiteEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace Data.Database.SeedData
{
    public class UserSeed : IEntityTypeConfiguration<UserDataTableEntity>
    {
        private readonly IConfiguration _configuration;

        public UserSeed(IConfiguration config)
        {
            _configuration = config;
        }

        public void Configure(EntityTypeBuilder<UserDataTableEntity> builder)
        {
            var userGuid = new Guid(DefaultEntityGuids.DefaultAdminGuid);
            var salt = Guid.NewGuid();
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new UserDataTableEntity
            {
                Id = userGuid,
                FamilyId = null,
                FirstName = "",
                LastName = "",
                UserName = _configuration["Admin:userName"],
                DateOfBirth = null,
                UserCredentialsId = new Guid(DefaultEntityGuids.AdminCredentialsGuid),
                UserCredentials = null,
                IsActive = true,
                Language = "en",
                ProfileImage = null,
                Modules = null,
                RoleId = new Guid(DefaultEntityGuids.AdminRoleGuid),
                Role = null,
                CreatedAt = createdAt,
                CreatedBy = "System",
                UpdatedAt = createdAt,
                UpdatedBy = "System",
            });
        }
    }
}
