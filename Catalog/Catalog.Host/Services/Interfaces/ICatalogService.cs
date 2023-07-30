using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<CatalogItemDto> GetCatalogItemByIdAsync(int id);
    Task<List<CatalogItemDto>> GetCatalogItemsByBrandIdAsync(int brandId);
    Task<List<CatalogItemDto>> GetCatalogItemsByTypeIdAsync(int typeId);
    Task<List<CatalogBrandDto>> GetCatalogBrandItemsAsync();
    Task<List<CatalogTypeDto>> GetCatalogTypeItemsAsync();
}