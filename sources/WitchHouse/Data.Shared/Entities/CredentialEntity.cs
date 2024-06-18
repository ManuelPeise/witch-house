using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Shared.Entities
{
    public class CredentialEntity:AEntityBase
    {
        public Guid Salt { get; set; }
        public int MobilePin { get; set; } = 1234;
        public string? EncodedPassword { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
