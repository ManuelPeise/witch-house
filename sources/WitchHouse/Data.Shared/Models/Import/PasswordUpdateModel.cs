namespace Data.Shared.Models.Import
{
    public class PasswordUpdateModel
    {
        public Guid UserId { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
