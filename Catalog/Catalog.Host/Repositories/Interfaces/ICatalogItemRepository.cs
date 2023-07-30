using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> GetItemByIdAsync(int id);
    Task<List<CatalogItem>> GetItemsByBrandIdAsync(int brandId);
    Task<List<CatalogItem>> GetItemsByTypeIdAsync(int typeId);
    Task<List<CatalogBrand>> GetBrandItemsAsync();
    Task<List<CatalogType>> GetTypeItemsAsync();
    Task<CatalogItem> Update(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName, int id);
    Task<int?> Delete(int itemId);
}