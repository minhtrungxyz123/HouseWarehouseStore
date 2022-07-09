using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesProductService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string productId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string productId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesProduct(int take);

        Task<List<FilesModel>> GetFilesProductDetail(int take, string id);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name);
    }
}