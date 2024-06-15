using Data.Shared.Enums;
using Data.Shared.Models.Account;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;

namespace Logic.Shared.Interfaces
{
    public interface IModuleConfigurationService: IDisposable
    {
        Task<ModuleConfiguration> LoadUserModuleConfiguration(UserModuleRequestModel requestModel);
        Task<List<UserModule>> GetUserModules(Guid userId, ModuleTypeEnum moduleType);
        Task CreateModules(Guid userId, bool isActive);
        Task UpdateModule(UserModule module);
        Task<List<ModuleSettings>> LoadSchoolModuleSettings(Guid userId, bool? isActive = null);
        Task UpdateSchoolModuleSettings(ModuleSettings settings);
    }
}
