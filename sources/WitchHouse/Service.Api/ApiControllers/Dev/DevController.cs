using Data.Database;
using Data.Shared.Entities;
using Logic.Shared;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace Service.Api.ApiControllers.Dev
{
    public class DevController : ApiControllerBase
    {
        private DatabaseContext _dbContext;
        public DevController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}", Name = "TestUser")]
        public async Task<AccountEntity?> GetTestUser(string id)
        {
            var userGuid = new Guid(id);

            var repo = new GenericRepository<AccountEntity>(_dbContext);

            var model = await repo.GetFirstByIdAsync(new Guid(id));

            return model;
        }
    }
}
