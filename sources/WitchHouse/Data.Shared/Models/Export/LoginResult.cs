namespace Data.Shared.Models.Export
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public Guid UserId { get; set; }
        public string Jwt { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
