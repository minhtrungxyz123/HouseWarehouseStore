using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface ICollectionApiClient
    {
        public Task<bool> Create(CollectionModel request);

        public Task<bool> Edit(int? id, CollectionModel request);

        Task<ApiResult<Pagination<CollectionModel>>> Get(CollectionSearchModel request);

        Task<ApiResult<CollectionModel>> GetById(int id);

        Task<bool> Delete(int id);
    }
}