using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface ITagApiClient
    {
        public Task<bool> Create(TagModel request);

        public Task<bool> Edit(string? id, TagModel request);

        Task<ApiResult<Pagination<TagModel>>> Get(TagSearchModel request);

        Task<ApiResult<TagModel>> GetById(string id);


        Task<bool> Delete(string id);
    }
}
