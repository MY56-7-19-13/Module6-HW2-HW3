using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<CatalogItemDto> GetCatalogItemByIdAsync(int id)
    {
        var result = await _catalogItemRepository.GetItemByIdAsync(id);
        return new CatalogItemDto();
    }

    public async Task<List<CatalogItemDto>> GetCatalogItemsByBrandIdAsync(int brandId)
    {
        var result = await _catalogItemRepository.GetItemsByBrandIdAsync(brandId);
        return new List<CatalogItemDto>();
    }

    public async Task<List<CatalogItemDto>> GetCatalogItemsByTypeIdAsync(int typeId)
    {
        var result = await _catalogItemRepository.GetItemsByTypeIdAsync(typeId);
        return new List<CatalogItemDto>();
    }

    public async Task<List<CatalogBrandDto>> GetCatalogBrandItemsAsync()
    {
        var result = await _catalogItemRepository.GetBrandItemsAsync();
        return new List<CatalogBrandDto>();
    }

    public async Task<List<CatalogTypeDto>> GetCatalogTypeItemsAsync()
    {
        var result = await _catalogItemRepository.GetTypeItemsAsync();
        return new List<CatalogTypeDto>();
    }
}