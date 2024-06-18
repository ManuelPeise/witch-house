using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace Data.Database.Configs.MySql
{
    public class CredentialConfiguration : IEntityTypeConfiguration<CredentialEntity>
    {
        private readonly IConfiguration _configuration;

        public CredentialConfiguration(IConfiguration config)
        {
            _configuration = config;
        }

        public void Configure(EntityTypeBuilder<CredentialEntity> builder)
        {
            var salt = Guid.NewGuid();
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");

            builder.HasData(new CredentialEntity
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
