using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> Add(string brand);
        Task<CatalogBrand> Update(int id, string brand);
        Task<int?> Delete(int id);
    }
}
