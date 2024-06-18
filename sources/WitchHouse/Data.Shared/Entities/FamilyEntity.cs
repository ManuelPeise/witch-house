namespace Data.Shared.Entities
{
    public class FamilyEntity:AEntityBase
    {
        public string FamilyName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public virtual List<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
    }
}
