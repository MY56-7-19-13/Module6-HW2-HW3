using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(string type);
        Task<CatalogType> Update(int id, string type);
        Task<int?> Delete(int id);
    }
}
