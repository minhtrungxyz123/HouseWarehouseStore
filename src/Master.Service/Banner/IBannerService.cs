using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IBannerService
    {
        Task<IEnumerable<Banner>> GetAll();

        Task<ApiResult<Pagination<BannerModel>>> GetAllPaging(BannerSearchContext ctx);

        Task<Banner> GetById(string? id);

        Task<RepositoryResponse> Create(BannerModel model);

        Task<RepositoryResponse> Update(string? id, BannerModel model);

        Task<int> Delete(string? id);

        Task<ApiResult<Banner>> GetByIdAsyn(string? id);

        IList<Banner> GetActive(bool showHidden = true);
    }
}