using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IVoucherService
    {
        Task<IEnumerable<Voucher>> GetAll();

        Task<ApiResult<Pagination<Voucher>>> GetAllPaging(VoucherSearchContext ctx);

        Task<Voucher> GetById(string? id);

        Task<RepositoryResponse> Create(VoucherModel model);

        Task<RepositoryResponse> Update(string? id, VoucherModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Voucher>> GetByIdAsyn(string? id);

        IList<Voucher> GetActive(bool showHidden = true);

        IList<Voucher> GetType(bool showHidden = true);

        IList<Voucher> GetCondition(bool showHidden = true);
    }
}