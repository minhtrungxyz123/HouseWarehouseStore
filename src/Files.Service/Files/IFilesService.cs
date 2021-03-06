using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string collectionId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string collectionId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesCollection(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name);

        Task<ApiResult<Pagination<FilesModel>>> GetAllPaging(FileSearchContext ctx);
    }
}