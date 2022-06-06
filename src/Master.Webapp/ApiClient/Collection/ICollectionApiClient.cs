using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface ICollectionApiClient
    {
        public Task<bool> Create(CollectionModel request);

        public Task<bool> CreateImage(FilesModel request, string collectionId);

        public Task<bool> Edit(string? id, CollectionModel request);

        Task<ApiResult<Pagination<CollectionModel>>> Get(CollectionSearchModel request);

        Task<ApiResult<CollectionModel>> GetById(string id);

        Task<bool> Delete(string id);

        public Task<ApiResult<FilesModel>> GetByIdImage(string id);
    }
}