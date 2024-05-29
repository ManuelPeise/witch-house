using Data.Database;
using Data.Shared.Entities;
using Data.Shared.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace Logic.Shared
{
    public abstract class LogicBase
    {
        private readonly DatabaseContext _context;
        private readonly CurrentUser _currentUser;

        public CurrentUser CurrentUser { get { return _currentUser; } }
        
        public LogicBase(DatabaseContext context, CurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task SaveChanges()
        {
            var modifiedEntries = _context.ChangeTracker.Entries()
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

                    }else if(entry.State == EntityState.Modified)
                    {
                        ((AEntityBase)entry.Entity).UpdatedBy = _currentUser.UserName ?? "System";
                        ((AEntityBase)entry.Entity).UpdatedAt = DateTime.Now.ToString(Constants.LogMessageDateFormat);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
