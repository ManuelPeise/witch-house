using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;

namespace Logic.Shared.Interfaces
{
    public interface IModuleConfigurationService: IDisposable
    {
        Task<ModuleConfiguration> LoadUserModuleConfiguration(UserModuleRequestModel requestModel, CurrentUser currentUser);
        Task<List<UserModule>> GetUserModules(Guid userId, ModuleTypeEnum moduleType);
        Task CreateModules(CurrentUser currentUser, Guid userId, bool isActive);
        Task UpdateModule(UserModule module, CurrentUser currentUser);
        Task<List<ModuleSettings>> LoadActiveSchoolModuleSettings(Guid userId, CurrentUser currentUser);
        Task UpdateSchoolModuleSettings(ModuleSettings settings, CurrentUser currentUser);
    }
}
