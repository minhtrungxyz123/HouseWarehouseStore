using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace Master.Webapp.ApiClient
{
    public interface IVoucherApiClient
    {
        public Task<bool> Create(VoucherModel request);

        public Task<bool> Edit(string? id, VoucherModel request);

        Task<ApiResult<Pagination<VoucherModel>>> Get(VoucherSearchModel request);

        Task<ApiResult<VoucherModel>> GetById(string id);

        Task<bool> Delete(string id);

        Task<VoucherModel> GetByCode(string code);
    }
}
