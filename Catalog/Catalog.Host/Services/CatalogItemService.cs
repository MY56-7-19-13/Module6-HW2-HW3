using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
    }

    public Task<List<CatalogItem>> GetItemsByBrandIdAsync(int brandId)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemsByBrandIdAsync(brandId));
    }

    public Task<CatalogItem?> GetItemByIdAsync(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemByIdAsync(id));
    }

    public Task<List<CatalogItem>> GetItemsByTypeIdAsync(int typeId)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetItemsByTypeIdAsync(typeId));
    }

    public Task<List<CatalogBrand>> GetBrandItemsAsync()
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetBrandItemsAsync());
    }

    public Task<List<CatalogType>> GetTypeItemsAsync()
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.GetTypeItemsAsync());
    }

    public Task<CatalogItem> Update(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName, int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Update(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName, id));
    }

    public Task<int?> Delete(int itemId)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Delete(itemId));
    }
}