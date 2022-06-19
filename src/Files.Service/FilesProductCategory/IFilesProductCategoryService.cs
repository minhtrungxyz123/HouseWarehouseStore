using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesProductCategoryService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string productCategoryId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameImageCoverAsync(string name);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string productCategoryId);

        Task<int> Delete(string id);

        Task<int> DeleteCover(string id);

        Task<List<FilesModel>> GetFilesProductCategory(int take);

        Task<List<FilesModel>> GetFilesCoverProductCategory(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name);

        Task<HouseWarehouseStore.Data.Entities.File> GetByFilesConverAsync(string name);
    }
}