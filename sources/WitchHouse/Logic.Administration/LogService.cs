﻿using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Logic.Administration
{
    public class LogService : LogicBase
    {
        private readonly ILogRepository _logRepository;
        private readonly DatabaseContext _databaseContext;
        private readonly CurrentUser _currentUser;

        public LogService(DatabaseContext context, ILogRepository logRepository, CurrentUser currentUser) : base()
        {
            _databaseContext = context;
            _logRepository = logRepository;
            _currentUser = currentUser;
        }

        public async Task<List<LogMessageEntity>> LoadLogMessages()
        {
            try
            {
                var messages = await _logRepository.GetLogMessages(null, null);

                return messages.ToList();
            }
            catch (Exception exception)
            {

                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _currentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });

                return new List<LogMessageEntity>();

            }
        }
        public async Task DeleteLogmessages(int[] ids)
        {
            try
            {
                await _logRepository.DeleteMessages(ids);

                await SaveChanges();

            }
            catch (Exception exception)
            {
                await _logRepository.AddLogMessage(new LogMessageEntity
                {
                    FamilyGuid = _currentUser.FamilyGuid,
                    Message = exception.Message,
                    Stacktrace = exception.StackTrace ?? "",
                    Trigger = nameof(LogService),
                    TimeStamp = DateTime.Now.ToString(Constants.LogMessageDateFormat),
                });
            }
        }

        private async Task SaveChanges()
        {
            var modifiedEntries = _databaseContext.ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Modified ||
               x.State == EntityState.Added);

            foreach (var entry in modifiedEntries)
            {
                if (entry != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((AEntityBase)entry.Entity).CreatedBy = _currentUser.UserName ?? "System";
                        ((AEntityBase)entry.Entity).CreatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);

                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        ((AEntityBase)entry.Entity).UpdatedBy = _currentUser.UserName ?? "System";
                        ((AEntityBase)entry.Entity).UpdatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);
                    }
                }
            }

            await _databaseContext.SaveChangesAsync();
        }
    }
}
