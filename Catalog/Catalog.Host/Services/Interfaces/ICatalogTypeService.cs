using Catalog.Host.Data.Entities;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Add(string type);
        Task<CatalogType> Update(int id, string type);
        Task<int?> Delete(int id);
    }
}
