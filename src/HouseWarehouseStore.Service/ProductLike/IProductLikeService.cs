using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface IProductLikeService
    {
        Task<List<ProductLikeModel>> GetAll(string idProd, string idMember);
        Task<ApiResult<Pagination<ProductLikeModel>>> GetAllPaging(ProductLikeSearchContext ctx);
    }
}