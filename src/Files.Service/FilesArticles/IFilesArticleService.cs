using HouseWarehouseStore.Models;

namespace Files.Service
{
    public interface IFilesArticleService
    {
        Task<long> InsertAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string articleId);

        Task<HouseWarehouseStore.Data.Entities.File> GetByIdAsync(string? id);

        Task<long> UpdateAsync(IList<HouseWarehouseStore.Data.Entities.File> entities, string articleId);

        Task<int> Delete(string id);

        Task<List<FilesModel>> GetFilesArticles(int take);

        Task<HouseWarehouseStore.Data.Entities.File> GetByNameAsync(string id);
    }
}