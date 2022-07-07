using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesCommentService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string commentId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string commentId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesComment(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string name);
    }
}