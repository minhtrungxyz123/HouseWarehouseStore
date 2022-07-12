using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IMemberApiClient
    {
        public Task<bool> Create(MemberModel request);

        public Task<bool> Edit(string? id, MemberModel request);

        Task<ApiResult<Pagination<MemberModel>>> Get(MemberSearchModel request);

        Task<ApiResult<MemberModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}