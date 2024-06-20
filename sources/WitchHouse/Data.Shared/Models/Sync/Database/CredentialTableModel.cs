namespace Data.Shared.Models.Sync.Database
{
    public class CredentialTableModel
    {
        public string? CredentialsId { get; set; }
        public int? MobilePin { get; set; } = 1234;
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
