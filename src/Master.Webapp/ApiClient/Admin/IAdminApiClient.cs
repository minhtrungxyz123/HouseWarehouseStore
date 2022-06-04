using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IAdminApiClient
    {
        public Task<bool> Create(AdminModel request);

        public Task<bool> Edit(string? id, AdminModel request);

        Task<ApiResult<Pagination<AdminModel>>> Get(AdminSearchModel request);

        Task<ApiResult<AdminModel>> GetById(string id);

        Task<bool> Delete(string id);
    }
}
