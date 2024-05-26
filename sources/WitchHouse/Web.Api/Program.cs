using Web.Api.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
WebApp.ConfigureDatabase(builder);
WebApp.ConfigureJwt(builder);
WebApp.ConfigureRepositories(builder);
WebApp.ConfigureServices(builder);

var app = builder.Build();

WebApp.ConfigureApp(app);
DatabaseMigrator.ExecuteDatabaseMigrations(app);

app.Run();
