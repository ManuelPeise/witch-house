using Data.Shared.SqLiteEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Data.Database.SeedData
{
    public class AdminCredentialsSeed : IEntityTypeConfiguration<UserCredentialTableEntity>
    {
        private readonly IConfiguration _configuration;

        public AdminCredentialsSeed(IConfiguration config)
        {
            _configuration = config;
        }

        public void Configure(EntityTypeBuilder<UserCredentialTableEntity> builder)
        {
            var salt = Guid.NewGuid();
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new UserCredentialTableEntity
            {
                Id = new Guid(DefaultEntityGuids.AdminCredentialsGuid),
                Salt = salt,
                EncodedPassword = GetEncodedSecret(_configuration["Admin:password"], salt.ToString()),
                MobilePin = 1234,
                JwtToken = null,
                RefreshToken = null,
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
