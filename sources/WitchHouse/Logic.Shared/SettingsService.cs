using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Enums;
using Data.Shared.Models.Export;
using Logic.Shared.Interfaces;

namespace Logic.Shared
{
    public class SettingsService : ISettingsService
    {
        private bool disposedValue;

        private IGenericRepository<SettingsEntity> _settingsRepository;

        public SettingsService(DatabaseContext context)
        {
            _settingsRepository = new GenericRepository<SettingsEntity>(context);
        }

        public async Task CreateSchoolTrainingSettings(UserModule module)
        {
            var entities = await _settingsRepository.GetByAsync(x => x.ModuleType == ModuleTypeEnum.SchoolTraining && x.UserId == module.UserId);

            var mathSettingsEntity = entities.FirstOrDefault(x => x.SettingsType == ModuleSettingsTypeEnum.MathUnits);

            if (mathSettingsEntity == null)
            {
                var mathSettigsId = Guid.NewGuid();
                
                mathSettingsEntity = new SettingsEntity
                {
                    Id = mathSettigsId,
                    UserId = module.UserId,
                    SettingsType = ModuleSettingsTypeEnum.MathUnits,
                    ModuleType = module.ModuleType,
                    SettingsJson = ""
                };

                await _settingsRepository.AddAsync(mathSettingsEntity);
            }

            var germanSettingsEntity = entities.FirstOrDefault(x => x.SettingsType == ModuleSettingsTypeEnum.GermanUnits);

            if (germanSettingsEntity == null)
            {
                var germanSettigsId = Guid.NewGuid();

                germanSettingsEntity = new SettingsEntity
                {
                    Id = germanSettigsId,
                    UserId = module.UserId,
                    SettingsType = ModuleSettingsTypeEnum.GermanUnits,
                    ModuleType = module.ModuleType,
                    SettingsJson = ""
                };

                await _settingsRepository.AddAsync(germanSettingsEntity);
            }
        }

        public async Task<SettingsEntity?> GetSettingsBy(Guid userId, ModuleSettingsTypeEnum settingsType)
        {
            var settings = await _settingsRepository.GetByAsync(x => x.UserId == userId && x.SettingsType == settingsType);

            if(settings == null || settings.Count() > 1)
            {
                throw new Exception($"No or more than one setting for [{userId}-{Enum.GetName(typeof(ModuleSettingsTypeEnum), settingsType)}] defined!");
            }

            return settings.FirstOrDefault();
        }

        public async Task<List<SettingsEntity>> GetSettingsByUserId(UserModule module)
        {
            var settings = await _settingsRepository.GetByAsync(x => x.UserId == module.UserId && x.ModuleType == module.ModuleType);

            return settings.ToList();
        }

        public async Task UpdateSettings(SettingsEntity entity)
        {
            await _settingsRepository.Update(entity);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: Verwalteten Zustand (verwaltete Objekte) bereinigen
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
