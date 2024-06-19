using Data.Database;
using Data.Shared.Entities;
using Logic.Shared;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Dev
{
    public class Ids
    {
        public string? First { get; set; }
        public string? Second { get; set; }
    }
    public class DevController : ApiControllerBase
    {
        private DatabaseContext _dbContext;
        private SqliteDbContext _sqLiteContext;
        public DevController(DatabaseContext dbContext, SqliteDbContext sqLiteContext)
        {
            _dbContext = dbContext;
            _sqLiteContext = sqLiteContext;
        }

        [HttpGet("{id}", Name = "TestUser")]
        public async Task<Ids> GetTestUser(string id)
        {
            var userGuid = new Guid(id);

            var mysqlRepo = new GenericRepository<DatabaseContext, AccountEntity>(_dbContext);
            var sqliteRepo = new GenericRepository<SqliteDbContext, AccountEntity>(_sqLiteContext);

            var mysqlAccount = await mysqlRepo.GetFirstByIdAsync(new Guid(id));
            var sqliteAccount = await sqliteRepo.GetFirstByIdAsync(new Guid(id));


            return new Ids
            {
                First = mysqlAccount?.Id.ToString() ?? "Failed",
                Second = sqliteAccount?.Id.ToString() ?? "Failed sqlite"
            };
        }
    }
}
