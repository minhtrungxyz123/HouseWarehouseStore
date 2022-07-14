using HouseWarehouseStore.Common;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Models;

namespace HouseWarehouseStore.Service
{
    public interface ICategoryService
    {
        Task<List<ProductCategory>> GetAll(bool showHidden = true);

        Task<ApiResult<Pagination<ProductCategoryModel>>> GetAllPaging(CategorySearchContext ctx);
    }
}