using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;

namespace Logic.Family.Interfaces
{
    public interface IFamilyAccountService
    {
        Task<bool> CreateFamilyAccount(AccountImportModel accountImportModel);
        Task AssignAccountToFamily(AccountImportModel accountImportModel);
        Task UpdateProfile(ProfileImportModel importModel);
        Task<bool> CheckPassword(PasswordUpdateModel model);
        Task<bool> UpdatePassword(PasswordUpdateModel model);
        Task UpdateUser(UserUpdateImportModel model);
        Task UploadProfileImage(ProfileImageUploadModel model);
        Task<ProfileExportModel?> GetProfile(string accountId);
        Task<bool> CheckUserName(string userName);
    }
}
