using Data.Shared.Entities;
using Data.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace Data.Database.Configs.MySql
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        private readonly IConfiguration _configuration;
        public AccountConfiguration(IConfiguration config)
        {
            _configuration = config;
        }

        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            var userGuid = new Guid(DefaultEntityGuids.DefaultAdminGuid);
            var salt = Guid.NewGuid();
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new AccountEntity
            {
                Id = userGuid,
                FamilyGuid = null,
                FirstName = "",
                LastName = "",
                UserName = _configuration["Admin:userName"],
                DateOfBirth = null,
                IsActive = true,
                Culture = "en",
                ProfileImage = null,
                CredentialGuid= new Guid(DefaultEntityGuids.AdminCredentialsGuid),
                Modules = null,
                UserRoles = null,
                UnitResults = null,
                CreatedAt = createdAt,
                CreatedBy = "System",
                UpdatedAt = createdAt,
                UpdatedBy = "System",
            });
        }

        private string GetEncodedSecret(string secret, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(secret).ToList();
            bytes.AddRange(Encoding.UTF8.GetBytes(salt));

            return Convert.ToBase64String(bytes.ToArray());
        }
    }
}
