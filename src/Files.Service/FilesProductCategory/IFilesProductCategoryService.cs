using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesProductCategoryService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string productCategoryId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string productCategoryId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesProductCategory(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name);
    }
}