using Data.Shared.Entities;
using Data.Shared.Models.Export;

namespace Logic.Shared.Interfaces
{
    public interface ISettingsService: IDisposable
    {
        Task CreateSchoolTrainingSettings(UserModule module);
        Task<List<SettingsEntity>> GetSettingsByUserId(UserModule module);
    }
}
