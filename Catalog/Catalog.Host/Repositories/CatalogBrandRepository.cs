using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string brand)
        {
            var brandItemAdd = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = brand,
            });

            await _dbContext.SaveChangesAsync();
            return brandItemAdd.Entity.Id;
        }

        public async Task<CatalogBrand> Update(int id, string brand)
        {
            var brandItemUpdate = new CatalogBrand
            {
                Brand = brand,
                Id = id
            };

            _dbContext.CatalogBrands.Update(brandItemUpdate);
            await _dbContext.SaveChangesAsync();
            return brandItemUpdate;
        }

        public async Task<int?> Delete(int id)
        {
            var brandItemDelete = new CatalogBrand { Id = id };

            _dbContext.CatalogBrands.Remove(brandItemDelete);
            await _dbContext.SaveChangesAsync();
            return brandItemDelete.Id;
        }
    }
}
