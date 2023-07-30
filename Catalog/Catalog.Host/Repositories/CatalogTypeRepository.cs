using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string type)
        {
            var typeAdd = await _dbContext.AddAsync(new CatalogType
            {
                Type = type,
            });

            await _dbContext.SaveChangesAsync();
            return typeAdd.Entity.Id;
        }

        public async Task<CatalogType> Update(int id, string type)
        {
            var typeUpdate = new CatalogType
            {
                Type = type,
                Id = id
            };

            _dbContext.CatalogTypes.Update(typeUpdate);
            await _dbContext.SaveChangesAsync();
            return typeUpdate;
        }

        public async Task<int?> Delete(int id)
        {
            var typeDelete = new CatalogType
            {
                Id = id
            };

            _dbContext.CatalogTypes.Remove(typeDelete);
            await _dbContext.SaveChangesAsync();
            return typeDelete.Id;
        }
    }
}
