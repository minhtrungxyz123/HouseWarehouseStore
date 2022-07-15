using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface ICartService
    {
        Task<ApiResult<Pagination<CartModel>>> GetAllPaging(CartSearchContext ctx);
    }
}