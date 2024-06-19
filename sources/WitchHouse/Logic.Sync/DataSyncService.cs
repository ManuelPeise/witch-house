using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Sync;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Logic.Sync.Interfaces;

namespace Logic.Sync
{
    public class DataSyncService : IDataSyncService
    {
        private bool disposedValue;

        private readonly IApplicationUnitOfWork<DatabaseContext> _applicationUnitOfWork;

        public DataSyncService(IApplicationUnitOfWork<DatabaseContext> applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task ExexuteAync(SyncModel syncModel)
        {
            try
            {

            }
            catch (Exception exception)
            {
                await _applicationUnitOfWork.LogRepository.AddLogMessage(new LogMessageEntity
                {
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                    Trigger = nameof(DataSyncService),
                });
            }
        }

        private async Task LoadDataFromMysql(SyncModel syncModel)
        {




        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _applicationUnitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
