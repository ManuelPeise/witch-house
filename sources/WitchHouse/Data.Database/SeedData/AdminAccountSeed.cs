using Data.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Data.Database.SeedData
{
    public class AdminAccountSeed : IEntityTypeConfiguration<AccountEntity>
    {
        private readonly IConfiguration _configuration;

        public AdminAccountSeed(IConfiguration config)
        {
            _configuration = config;
        }
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            var salt = Guid.NewGuid().ToString();

            builder.HasData(new AccountEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "",
                LastName = "",
                UserName = _configuration["Admin:userName"],
                Secret = GetEncodedSecret(_configuration["Admin:password"], salt),
                Salt = salt,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                CreatedBy = "System",
                Culture = "en",
                Role = Shared.Enums.UserRoleEnum.Admin,
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
