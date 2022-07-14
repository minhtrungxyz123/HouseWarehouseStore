using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace Master.Service
{
    public interface IProductLikeService
    {
        Task<IEnumerable<ProductLike>> GetAll();

        Task<ApiResult<Pagination<ProductLikeModel>>> GetAllPaging(ProductLikeSearchContext ctx);

        Task<ProductLike> GetById(string id);

        Task<ApiResult<ProductLike>> GetByIdAsyn(string id);
    }
}