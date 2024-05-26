using Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Config
{
    public static class DatabaseMigrator
    {
        public static void ExecuteDatabaseMigrations(WebApplication app)
        {
            using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();

                if(context != null)
                {
                    // context.Database.EnsureCreated();

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                   
                }
            }
        }
    }
}
