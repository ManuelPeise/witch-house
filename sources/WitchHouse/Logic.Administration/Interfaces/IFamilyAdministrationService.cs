using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Response;

namespace Logic.Administration.Interfaces
{
    public interface IFamilyAdministrationService
    {
        Task<List<UserDataExportModel>> GetFamilyUsers(Guid? familyId);
        Task<ResponseMessage<UserDataModel>> GetUserData(Guid accountGuid);
    }
}
