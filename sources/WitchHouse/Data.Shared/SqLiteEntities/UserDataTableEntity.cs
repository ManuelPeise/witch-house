using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace Data.Shared.SqLiteEntities
{
    public class UserDataTableEntity: ASqliteEntityBase
    {
        public Guid? FamilyId { get; set; }
        [ForeignKey(nameof(FamilyId))]
        public FamilyEntity? Family { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Language { get; set; }
        public bool IsActive { get; set; }
        public string? ProfileImage { get; set; }
        public Guid UserCredentialsId { get; set; }
        [ForeignKey(nameof(UserCredentialsId))]
        public UserCredentialTableEntity UserCredentials { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public RoleTableEntity Role { get; set; } = new RoleTableEntity();
        public List<ModuleTableEntity> Modules { get; set; } = new List<ModuleTableEntity>();
    }
}
