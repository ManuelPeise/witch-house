using Data.Database;
using Logic.Administration;
using Logic.Administration.Interfaces;
using Logic.Authentication;
using Logic.Authentication.Interfaces;
using Logic.Family;
using Logic.Family.Interfaces;
using Logic.Shared;
using Logic.Shared.Interfaces;
using Logic.Sync;
using Logic.Sync.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web.Api.Config
{

    public static class WebApp
    {
        private static string CorsPolicy = "Policy";

        public static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DatabaseContext>(opt =>
            {
                var connection = builder.Configuration.GetConnectionString("WitchHouseContext") ?? null;

                if (connection == null)
                {
                    throw new Exception("Could not connect to database!");
                }

                opt.UseMySQL(connection);

            });

            builder.Services.AddDbContext<SqliteDbContext>(opt =>
            {
                var connection = builder.Configuration.GetConnectionString("WitchHouseSqLiteContext");

                if (connection == null)
                {
                    throw new Exception("Could not connect to database!");
                }

                opt.UseSqlite(connection);
            });
        }

        public static void ConfigureJwt(WebApplicationBuilder builder)
        {
            var (issuer, key) = GetJwtDataFromConfig(builder);

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        public static void ConfigureRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILogRepository, LogRepository>();
            builder.Services.AddScoped<IUserDataClaimsAccessor, UserDataClaimsAccessor>();
            builder.Services.AddScoped(typeof(IApplicationUnitOfWork<>), typeof(ApplicationUnitOfWork<>));
            builder.Services.AddScoped<IFamilyAccountService, FamilyAccountService>();
            builder.Services.AddScoped<IFamilyAdministrationService, FamilyAdministrationService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IModuleConfigurationService, ModuleConfigurationService>();
            builder.Services.AddScoped<IUnitResultService, UnitResultService>();
            builder.Services.AddScoped<IDataSyncService, DataSyncService>();

        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(CorsPolicy, opt =>
                {
                    opt.AllowAnyHeader();
                    opt.AllowAnyMethod();
                    opt.AllowAnyOrigin();
                    // opt.AllowCredentials();
                });
            });

        }

        public static void ConfigureApp(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                Thread.CurrentPrincipal = context.User;
                await next(context);
            });

            app.MapControllers();

            app.UseCors(CorsPolicy);
        }

        private static (string? jwtIssuer, string? jwtKey) GetJwtDataFromConfig(WebApplicationBuilder builder)
        {
            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

            return (jwtIssuer, jwtKey);
        }
    }
}
