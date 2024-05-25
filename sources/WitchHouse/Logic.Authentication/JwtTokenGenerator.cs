using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Logic.Authentication
{
    public class JwtTokenGenerator
    {
        public JwtTokenGenerator()
        {

        }

        public (string, string) GenerateToken(IConfiguration config, List<Claim> claims, int expireInDays)
        {
            return (GenerateJwtToken(config, claims, expireInDays), GenerateRefreshToken());
        }

        private string GenerateJwtToken(IConfiguration config, List<Claim> claims, int expireInDays)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(config["Jwt:Issuer"],
             config["Jwt:Issuer"],
             claims,
             expires: DateTime.Now.AddDays(expireInDays),
             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
