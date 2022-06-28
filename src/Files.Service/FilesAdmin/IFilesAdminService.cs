using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesAdminService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string adminId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string adminId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesAdmin(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name);
    }
}