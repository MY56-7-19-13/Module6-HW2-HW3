using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItem?> GetItemByIdAsync(int id)
    {
        var item = await _dbContext.CatalogItems.
            Include(i => i.CatalogBrand).
            Include(i => i.CatalogType).
            FirstOrDefaultAsync(i => i.Id == id);

        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<List<CatalogItem>> GetItemsByBrandIdAsync(int id)
    {
        var items = await _dbContext.CatalogItems.
            Include(i => i.CatalogBrand).
            Include(i => i.CatalogType).
            Where(i => i.CatalogBrandId == id).
            ToListAsync();

        await _dbContext.SaveChangesAsync();
        return items.ToList();
    }

    public async Task<List<CatalogItem>> GetItemsByTypeIdAsync(int id)
    {
        var items = await _dbContext.CatalogItems.
            Include(i => i.CatalogBrand).
            Include(i => i.CatalogType).
            Where(i => i.CatalogTypeId == id).
            ToListAsync();

        await _dbContext.SaveChangesAsync();
        return items.ToList();
    }

    public async Task<List<CatalogBrand>> GetBrandItemsAsync()
    {
        var brands = await _dbContext.CatalogBrands.
            ToListAsync();

        await _dbContext.SaveChangesAsync();
        return brands.ToList();
    }

    public async Task<List<CatalogType>> GetTypeItemsAsync()
    {
        var types = await _dbContext.CatalogTypes.
            ToListAsync();

        await _dbContext.SaveChangesAsync();
        return types.ToList();
    }

    public async Task<CatalogItem> Update(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName, int id)
    {
        var updateItem = new CatalogItem
        {
            Name = name,
            Description = description,
            Price = price,
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            PictureFileName = pictureFileName,
            Id = id
        };

        _dbContext.CatalogItems.Update(updateItem);
        await _dbContext.SaveChangesAsync();
        return updateItem;
    }

    public async Task<int?> Delete(int itemId)
    {
        var deleteItem = new CatalogItem { Id = itemId };

        _dbContext.CatalogItems.Remove(deleteItem);
        await _dbContext.SaveChangesAsync();
        return deleteItem.Id;
    }
}