using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface ITagProductApiClient
    {
        public Task<bool> Create(TagProductModel request);

        Task<ApiResult<Pagination<TagProductModel>>> Get(TagProductSearchModel request);

        Task<ApiResult<TagProductModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}
