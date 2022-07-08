using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IFollowApiClient
    {
        public Task<bool> Create(FollowModel request);

        public Task<bool> Edit(string id, FollowModel request);

        Task<ApiResult<Pagination<FollowModel>>> Get(FollowSearchModel request);

        Task<ApiResult<FollowModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}