using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface ICollectionApiClient
    {
        public Task<bool> Create(CollectionModel request);

        public Task<bool> CreateImage(FilesModel request, string collectionId);

        public Task<bool> UpdateImage(FilesModel request, string collectionId);

        public Task<bool> Edit(string? id, CollectionModel request);

        Task<ApiResult<Pagination<CollectionModel>>> Get(CollectionSearchModel request);

        Task<ApiResult<CollectionModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<bool> DeleteDataFiles(string id);

        Task<List<FilesModel>> GetFilesCollection(int take);

        Task<bool> DeleteFiles(string id);

        Task<AdminModel> GetByUserId(string id);

        Task<List<CollectionModel>> GetAll();

        Task<IList<CollectionModel>> GetActive(bool showHidden = true);
    }
}