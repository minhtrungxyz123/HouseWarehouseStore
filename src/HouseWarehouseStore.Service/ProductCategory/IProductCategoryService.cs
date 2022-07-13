using HouseWarehouseStore.Common;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface IProductCategoryService
    {
        Task<ApiResult<Pagination<ProductCategoryModel>>> GetAllPaging(ProductCategorySearchContext ctx);
    }
}