using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesBannerService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string bannerId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string bannerId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesBanner(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string id);
    }
}