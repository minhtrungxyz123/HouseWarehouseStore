using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IAdminApiClient
    {
        public Task<bool> Create(AdminModel request);

        public Task<bool> Edit(int? id, AdminModel request);

        Task<ApiResult<Pagination<AdminModel>>> Get(AdminSearchModel request);

        Task<ApiResult<AdminModel>> GetById(int id);

        Task<bool> Delete(int id);
    }
}
