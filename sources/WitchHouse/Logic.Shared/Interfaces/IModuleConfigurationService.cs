using Data.Shared.Enums;
using Data.Shared.Models.Export;
using Data.Shared.Models.Import;
using Data.Shared.Models.Response;

namespace Logic.Shared.Interfaces
{
    public interface IModuleConfigurationService: IDisposable
    {
        Task<ModuleConfiguration> LoadUserModuleConfiguration(UserModuleRequestModel requestModel);
        Task<List<UserModule>> GetUserModules(Guid accountGuid, ModuleTypeEnum moduleType);
        Task UpdateModule(UserModule module);
        Task<ResponseMessage<SchoolModule>> LoadSchoolModule(Guid accountGuid);
        Task UpdateSchoolModuleSettings(ModuleSettings settings);
    }
}
