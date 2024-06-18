using Data.Shared.Models.Export;

namespace Logic.Administration.Interfaces
{
    public interface IFamilyAdministrationService
    {
        Task<List<UserDataExportModel>> GetFamilyUsers(Guid? familyId);
    }
}
