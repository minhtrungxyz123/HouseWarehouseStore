using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesConfigsiteService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string configsiteId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string configsiteId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesConfigsite(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name);
    }
}