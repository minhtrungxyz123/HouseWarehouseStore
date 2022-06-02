using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface ISizeApiClient
    {
        public Task<bool> Create(SizeModel request);

        public Task<bool> Edit(int? id, SizeModel request);

        Task<ApiResult<Pagination<SizeModel>>> Get(SizeSearchModel request);

        Task<ApiResult<SizeModel>> GetById(int id);

        Task<bool> Delete(int id);
    }
}
